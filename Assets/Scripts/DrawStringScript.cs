using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class DrawStringScript : MonoBehaviour
{
    [SerializeField] [Tooltip("These characters define whats drawn")] private string dynaDrawCommands = "frfrf";
    public GameObject hinge_prefab;
    public GameObject arm_prefab;
    public GameObject cursor_prefab;
    public GameObject trail_prefab;
    public GameObject pointlight_prefab;
    public GameObject spotlight_prefab;
    public GameObject tilter_prefab;
    public GameObject shooter_prefab;
    public GameObject letterOpener_prefab;
    public GameObject instructionalsObject;
    public GameObject[] UserSelectable;
    public float zoomFactor = 0.2f;  //20%

    private int maxMultiplyCount = 25;
    private GameObject parentObject;
    private GameObject headObject;
    private Stack<GameObject>headObjectStack = new Stack<GameObject>();
    private GameObject go;
    private ColorAndLengthScript colorAndLengthScript;
    private LightColor lightColotScript;
    private RotateHorizontally rotateScript;
    private PixelColor pixelColorScript;
    private Tilter tilterScript;
    private Color[] colors = { Color.black, Color.white, Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan, Color.gray, Color.clear };
    private int cursorPosition = 0;
    private bool hideCursorPosition = true;

    private IDictionary<string, string> userFuntions = new Dictionary<string, string>();

    struct copySubString 
    { 
        public int startIndex; 
        public int endIndex;
        public copySubString(int s, int e)
        {
            this.startIndex = s;
            this.endIndex = e;
        }
    };
    private Stack<copySubString>copySubStringStack = new Stack<copySubString>();
    private Stack<copySubString>userFunctionSubStringStack = new Stack<copySubString>();

    Color currentColor;
    bool usingShooter = false;
    bool usingTitler = false;
    bool usingDynamicColor = false;
    bool skippingStuffInsideAngleBrackets = false;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = Color.white;
        usingDynamicColor = false;
        usingShooter = usingTitler = false;
        skippingStuffInsideAngleBrackets = false;
        headObject = parentObject = new GameObject();
        parentObject.transform.SetParent(this.transform);
        ProcessDynaDrawCommand(dynaDrawCommands, startIndex: 0, endIndex: dynaDrawCommands.Length - 1);        
    }

    public void Restart(string newCommands, int caretPosition)
    {
        dynaDrawCommands = newCommands;
        Redraw(caretPosition);
    }
    public void SetDynaString(string newCommands)
    {
        dynaDrawCommands = newCommands;
        Redraw(-1);  // will hide 3D Cursor
    }

    public string GetDynaString()
    {
        return dynaDrawCommands;
    }

    public string SendKey(byte byteKeyValue)  // onscreen keyboard interfaces here
    {
        // private byte[] lineOneBytes = new byte[] { 0x1b, 0x1a, 0x20, 0x7f, 0x08 };  // left arrow, right arrow, space, Del, Backspace

        switch (byteKeyValue)
        {
            case 0x1b: //Left Arrow
                var newCursorPositon = cursorPosition - 1;
                if (newCursorPositon < 0)
                    newCursorPositon = 0;
                Redraw(newCursorPositon);
                break;
            case 0x1a: //Right Arrow
                newCursorPositon = cursorPosition + 1;
                if (newCursorPositon > dynaDrawCommands.Length)
                    newCursorPositon = dynaDrawCommands.Length;
                Redraw(newCursorPositon);
                break;
            case 0x7f: //Delete
                if (cursorPosition >= 0)
                {
                    newCursorPositon = cursorPosition;
                    dynaDrawCommands = dynaDrawCommands.Remove(cursorPosition,1);
                    Redraw(newCursorPositon);
                }
                break;
            case 0x08: //Backspace
                if (cursorPosition >= 1)
                {
                    newCursorPositon = cursorPosition - 1;
                    dynaDrawCommands = dynaDrawCommands.Remove(cursorPosition - 1, 1);
                    Redraw(newCursorPositon);
                }
                break;
            default:
                newCursorPositon = cursorPosition + 1;
                dynaDrawCommands = dynaDrawCommands.Insert(cursorPosition, System.Text.Encoding.ASCII.GetString(new[] { byteKeyValue }));
                Redraw(newCursorPositon);
                break;
        }
        return dynaDrawCommands;
    }

    public void Redraw(int caretPosition)
    {
        if (caretPosition == -1)
            hideCursorPosition = true;
        else
        {
            hideCursorPosition = false;
            cursorPosition = caretPosition;
        }
        GameObject.Destroy(parentObject);
        userFuntions.Clear();
        Start();
    }

    public int GetCursorPosition()
    {
        return cursorPosition;
    }
    public void SetCursorPosition(int newCursorPosition)
    {
        cursorPosition = newCursorPosition;
    }

    void ProcessDynaDrawCommand(string dynaDrawString, int startIndex, int endIndex)
    {
        copySubString currentCopySubString = new copySubString(0,0);
        Stack<GameObject> headObjectStack = new Stack<GameObject>();
        copySubString lettersSubstring = new copySubString(-1, -1);
        bool insideQuotes = false;
        copySubString userSelectedOperation = new copySubString(-1, -1);
        int repeatCounter = 0;
        bool userSetRepeatCounterInPlay = false;

        if (cursorPosition == 0 && !hideCursorPosition)
        {
            go = Instantiate(cursor_prefab, headObject.transform);
            go.transform.SetParent(headObject.transform);
        }

        //foreach (char dynaDrawCommand in dynaDrawString)
        for (int index = startIndex; index <= endIndex; index++)
        {
            char dynaDrawCommand = dynaDrawString[index];

            if (dynaDrawCommand == '>')
            {
                string functionDefinition = null;
                userSelectedOperation = userFunctionSubStringStack.Pop();
                userSelectedOperation.endIndex = index;
                
                var lengthOfString = userSelectedOperation.endIndex - userSelectedOperation.startIndex - 1;
                if (lengthOfString > 0 &&  userFunctionSubStringStack.Count == 0)
                {
                    functionDefinition = DoUserCommand(dynaDrawString.Substring(userSelectedOperation.startIndex + 1, lengthOfString));
                }
                
                //Only at top level
                if (userFunctionSubStringStack.Count == 0)
                {
                    skippingStuffInsideAngleBrackets = false;
                    userSelectedOperation.startIndex = userSelectedOperation.endIndex = -1;
                    if (functionDefinition != null)
                        ProcessDynaDrawCommand(functionDefinition, startIndex: 0, endIndex: functionDefinition.Length - 1);
                }
            }
        
            if (dynaDrawCommand == '<')
            {
                userSelectedOperation.startIndex = userSelectedOperation.endIndex = index;
                userFunctionSubStringStack.Push(new copySubString(index, index));
                skippingStuffInsideAngleBrackets = true;
            }

            if (skippingStuffInsideAngleBrackets)
                continue;  // ignore everything inside < >

            if (dynaDrawCommand == '"')  // remember this group of Characters, so it can be Displayed as letter gameobjects
            {
                if (!insideQuotes)
                {
                    lettersSubstring.startIndex = lettersSubstring.endIndex = index;
                    insideQuotes = true;
                }
                else
                {
                    lettersSubstring.endIndex = index;
                    var lengthOfString = lettersSubstring.endIndex - lettersSubstring.startIndex - 1;
                    if (lengthOfString > 0)
                        DisplayTheseLetters(dynaDrawString.Substring(lettersSubstring.startIndex + 1, lengthOfString));
                    lettersSubstring.startIndex = lettersSubstring.endIndex = -1;
                    insideQuotes = false;
                }
            }
            if (insideQuotes)
                continue;  // everything inside " " gets saved for a DisplayLetter call        

            if (char.IsDigit(dynaDrawCommand))
            {
                if (userSetRepeatCounterInPlay)
                    repeatCounter = repeatCounter * 10 + dynaDrawCommand - '0';
                else
                {
                    repeatCounter = dynaDrawCommand - '0';
                    userSetRepeatCounterInPlay = true;
                }
            }

            int doThisManyTimes = 1;
            if (userSetRepeatCounterInPlay)
                doThisManyTimes = Math.Min(repeatCounter, maxMultiplyCount);            
            while (doThisManyTimes > 0)
            {
                switch (dynaDrawCommand)
                {
                    case 'Z':   //Zoom in
                        go = new GameObject("ZoomIn");
                        go.transform.SetParent(headObject.transform);
                        go.transform.rotation = headObject.transform.rotation;
                        go.transform.localPosition = new Vector3(0, 0, 0);
                        go.transform.localScale = new Vector3(1f + zoomFactor, 1f + zoomFactor, 1f + zoomFactor);
                        headObject = go;
                        break;
                    case 'z':   //Zoom in
                        go = new GameObject("ZoomOut");
                        go.transform.SetParent(headObject.transform);
                        go.transform.rotation = headObject.transform.rotation;
                        go.transform.localPosition = new Vector3(0, 0, 0);
                        go.transform.localScale = new Vector3(1f - zoomFactor, 1f - zoomFactor, 1f - zoomFactor);
                        headObject = go;
                        break;
                    case 'c':   // Copy the recorded commands   
                        if (startIndex != endIndex)  // Someone may have typed a copy without any brackets, or there is nothing to draw
                            ProcessDynaDrawCommand(dynaDrawString, startIndex: currentCopySubString.startIndex, endIndex: currentCopySubString.endIndex);
                        break;
                    case 'f':   // Forward static
                    case 'F':   // Forward dynamic
                    case 'b':   // backwards static - no drawing
                    case 'B':   // backwards dynamic - no drawing
                    case 'm':   // move static - dont draw arm
                    case 'M':   // move dynamic - dont draw arm
                    case 'x':   // was an XOR draw of the arm - will map to f
                    case 'X':   // was an XOR draw of the arm - will map to F
                        go = Instantiate(arm_prefab, headObject.transform);
                        colorAndLengthScript = go.GetComponentInChildren<ColorAndLengthScript>();
                        colorAndLengthScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                        colorAndLengthScript.SetStatic(char.IsLower(dynaDrawCommand));
                        if (char.ToLower(dynaDrawCommand) == 'm' || char.ToLower(dynaDrawCommand) == 'b')
                            colorAndLengthScript.SetVisibility(false);
                        if (char.ToLower(dynaDrawCommand) == 'b')
                            colorAndLengthScript.SetBackwards();
                        go.transform.SetParent(headObject.transform);
                        headObject = go;
                        break;
                    case 'l':   // turn left   around the Z axis
                    case 'L':   // continuous turn left
                    case 'r':   // turn right
                    case 'R':   // continuous turn right
                        go = Instantiate(hinge_prefab, headObject.transform);
                        go.transform.SetParent(headObject.transform);
                        rotateScript = go.GetComponentInChildren<RotateHorizontally>();
                        rotateScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                        if (char.ToLower(dynaDrawCommand) == 'r')
                            rotateScript.SetDirection(0, 0, -1);
                        else
                            rotateScript.SetDirection(0, 0, 1);
                        if (char.IsLower(dynaDrawCommand))
                            rotateScript.SetStatic(true);
                        else
                            rotateScript.SetStatic(false);
                        headObject = go;
                        break;
                    case 'u':   // up  - around the X axis
                    case 'U':
                    case 'd':   // down
                    case 'D':
                        go = Instantiate(hinge_prefab, headObject.transform);
                        go.transform.SetParent(headObject.transform);
                        rotateScript = go.GetComponentInChildren<RotateHorizontally>();
                        rotateScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                        if (char.ToLower(dynaDrawCommand) == 'd')
                            rotateScript.SetDirection(1, 0, 0);
                        else
                            rotateScript.SetDirection(-1, 0, 0);
                        if (char.IsLower(dynaDrawCommand))
                            rotateScript.SetStatic(true);
                        else
                            rotateScript.SetStatic(false);
                        headObject = go;
                        break;
                    case 'i':   // in - around the Y axis
                    case 'I':
                    case 'o':   // out
                    case 'O':
                        go = Instantiate(hinge_prefab, headObject.transform);
                        go.transform.SetParent(headObject.transform);
                        rotateScript = go.GetComponentInChildren<RotateHorizontally>();
                        rotateScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                        if (char.ToLower(dynaDrawCommand) == 'i')
                            rotateScript.SetDirection(0, -1, 0);
                        else
                            rotateScript.SetDirection(0, 1, 0);
                        if (char.IsLower(dynaDrawCommand))
                            rotateScript.SetStatic(true);
                        else
                            rotateScript.SetStatic(false);
                        headObject = go;
                        break;
                    default:
                        break;
                }
                doThisManyTimes--;
            }

            // non-repeatables
            switch (dynaDrawCommand)
            {                
                case 'C':  // Taste the rainbow
                    usingDynamicColor = true;
                    break;
                case '[':  // remember origin, so it can be reset after this group
                    headObjectStack.Push(headObject);
                    break;
                case ']':   // reset origin
                    if (headObjectStack.Count > 0)
                        headObject = headObjectStack.Pop();
                    break;
                case '(':   // remember this group of commands, so it can be copied with the C command                                       
                    copySubStringStack.Push(new copySubString(index, index));
                    break;
                case ')':   // stop recording the commands that will be available for Copy
                    if (copySubStringStack.Count > 0)
                    {
                        currentCopySubString = copySubStringStack.Pop();
                        currentCopySubString.endIndex = index;
                    }
                    break;                          
                default:
                    break;                
            }

            if (index == (cursorPosition - 1) && !hideCursorPosition)
            {
                go = Instantiate(cursor_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
            }
            if (!char.IsDigit(dynaDrawCommand))
                userSetRepeatCounterInPlay = false;

        }
        //copySubStringStack.Clear();
        userFunctionSubStringStack.Clear();
        var instructionalScript = instructionalsObject.GetComponentInChildren<InputInstructionalScript>();
        instructionalScript.SetInstructionVisibility(showShoot: usingShooter, showTilt: usingTitler);
    }

    // UserSelectable object
    // <Thing>: Find this Prefab
    // <Color(1)> 0-9, or C
    // <NewFunction(3)>
    // <NewFunction(X)=mmmmm[Xr<Disk>]>
    private string DoUserCommand(string userCommand)
    {
        // <Thing> Should be a user selectable Prefab Object 
        // OR parameterless function <Circle>
        if (Regex.IsMatch(userCommand, @"^[a-zA-Z]+$"))
        {
            if (userCommand.ToLower() == "tilter")
            {
                go = Instantiate(tilter_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
                tilterScript = go.GetComponentInChildren<Tilter>();
                tilterScript.direction = 1;
                headObject = go;
                usingTitler = true;
                return null;
            }
            if (userCommand.ToLower() == "atilter")
            {
                go = Instantiate(tilter_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
                tilterScript = go.GetComponentInChildren<Tilter>();
                tilterScript.direction = -1;
                headObject = go;
                usingTitler = true;
                return null;
            }
            if (userCommand.ToLower() == "shoot")
            {
                go = Instantiate(shooter_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
                usingShooter = true;
                return null;
            }
            if (userCommand.ToLower() == "point")
            { go = Instantiate(pointlight_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
                lightColotScript = go.GetComponentInChildren<LightColor>();
                lightColotScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                return null;
            }
            if (userCommand.ToLower() == "spot")
            { 
            go = Instantiate(spotlight_prefab, headObject.transform);
            go.transform.SetParent(headObject.transform);
            lightColotScript = go.GetComponentInChildren<LightColor>();
            lightColotScript.SetColor(currentColor, useDynamic: usingDynamicColor);         
            return null;
            }
            if (userCommand.ToLower() == "smoke" || userCommand.ToLower() == "draw")
            {
                go = Instantiate(trail_prefab, headObject.transform);
                pixelColorScript = go.GetComponentInChildren<PixelColor>();
                pixelColorScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                go.transform.SetParent(headObject.transform);
                return null;
            }
            var userSelectedPrefab = UserSelectable.Where(item => item.name == userCommand).FirstOrDefault();
            if (userSelectedPrefab != null)
            {
                go = Instantiate(userSelectedPrefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
                colorAndLengthScript = go.GetComponentInChildren<ColorAndLengthScript>();
                if (colorAndLengthScript != null)
                    colorAndLengthScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                return null;
            }
            string functionDefinition;
            if (userFuntions.TryGetValue(userCommand, out functionDefinition))
            {
                // NO Parameters here, is that bad?
                return functionDefinition;
            }
            return null;
        }

        // <Function(X)=asd>  Function Assign
        var functionAssignMatch = Regex.Match(userCommand, @"([A-Za-z][a-z0-9_]*)\((.*)\)=(..*)");
        if (functionAssignMatch.Success)  
        {
            userFuntions[functionAssignMatch.Groups[1].ToString()] = functionAssignMatch.Groups[3].ToString();
            return null;
        }

        // <Function=asd>  Function Assign No Parameter parenthases
        functionAssignMatch = Regex.Match(userCommand, @"([A-Za-z][a-z0-9_]*)=(..*)");
        if (functionAssignMatch.Success)
        {
            userFuntions[functionAssignMatch.Groups[1].ToString()] = functionAssignMatch.Groups[2].ToString();
            return null;
        }

        // <Function(1)>  Function Call
        var functionCallMatch = Regex.Match(userCommand, @"([A-Za-z][a-z0-9_]*)\((.*)\)");
        if (functionCallMatch.Success)  
        {
            var functionName = functionCallMatch.Groups[1].ToString();

            if (functionName.ToLower() == "color")
            {
                if (System.Int32.TryParse(functionCallMatch.Groups[2].ToString(), out int color))
                {
                    color = Mathf.Min(color, 9);
                    color = Mathf.Max(color, 0);
                    currentColor = colors[color];
                    usingDynamicColor = false;
                }
                else
                {
                    if (functionCallMatch.Groups[2].ToString().ToLower() == "c")
                        usingDynamicColor = true;
                }
                return null;
            }
            // function(1)="Xf6r" - all occurances of X are replaced with 1
            string functionDefinition;
            if (userFuntions.TryGetValue(functionName, out functionDefinition))
            {                
                return functionDefinition.Replace("X", functionCallMatch.Groups[2].ToString());
            }
        }

        return null;
    }

    private void DisplayTheseLetters(string lettersToDisplay)
    {
        var getPrefabForLetterScript = letterOpener_prefab.GetComponentInChildren<GetPrefabForLetter>();
        
        var arrayOfChars = lettersToDisplay.ToCharArray();
        bool skipAngleBracket = false;
        foreach (var nextCharater in arrayOfChars)
        {
            if (nextCharater == '<')
                skipAngleBracket = true;
            if (nextCharater == '>')
                skipAngleBracket = false;
            if (skipAngleBracket)
                continue;           
            var prefab = getPrefabForLetterScript.GetPrefab(nextCharater);
            go = Instantiate(prefab, headObject.transform);
            go.transform.SetParent(headObject.transform);
            headObject = go;
            var letterColoeScript = go.GetComponentInChildren<LetterColor>();
            letterColoeScript.SetColor(currentColor, useDynamic: usingDynamicColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
