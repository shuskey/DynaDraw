using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStringScript : MonoBehaviour
{
    [SerializeField] [Tooltip("These characters define whats drawn")] private string dynaDrawCommands = "frfrf";
    public GameObject hinge_prefab;
    public GameObject arm_prefab;
    public GameObject disk_prefab;
    public GameObject cursor_prefab;
    public GameObject trail_prefab;
    public GameObject rocket_prefab;
    public GameObject pointlight_prefab;
    public GameObject spotlight_prefab;
    public GameObject world_prefab;
    public GameObject astiroid_prefab;
    public GameObject sword_prefab;
    public GameObject tilter_prefab;
    public GameObject shooter_prefab;
    public GameObject letterOpener_prefab;
    public GameObject instructionalsObject;
    public float zoomFactor = 0.2f;  //20%

    private GameObject parentObject;
    private GameObject headObject;
    private Stack<GameObject>headObjectStack = new Stack<GameObject>();
    private GameObject go;
    private ArmLength armLengthScript;
    private LightColor lightColotScript;
    private RotateHorizontally rotateScript;
    private PixelColor pixelColorScript;
    private Tilter tilterScript;
    private Color[] colors = { Color.black, Color.white, Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan, Color.gray, Color.clear };
    private int cursorPosition = 0;
    private bool hideCursorPosition = true;
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
                skippingStuffInsideAngleBrackets = false;
            if (skippingStuffInsideAngleBrackets)
                continue;  // ignore everything inside < >
            if (dynaDrawCommand == '<')
                skippingStuffInsideAngleBrackets = true;

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
                currentColor = colors[dynaDrawCommand - '0'];
                usingDynamicColor = false;
            }
            
            switch (dynaDrawCommand)
            {
                case 's':   // shooter
                    go = Instantiate(shooter_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    usingShooter = true;
                    break;
                case 't':   //Tilter
                    go = Instantiate(tilter_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    tilterScript = go.GetComponentInChildren<Tilter>();
                    tilterScript.direction = 1;
                    headObject = go;
                    usingTitler = true;
                    break;
                case 'T':   //Tilter opposite direction
                    go = Instantiate(tilter_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    tilterScript = go.GetComponentInChildren<Tilter>();
                    tilterScript.direction = -1;
                    headObject = go;
                    usingTitler = true;
                    break;
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
                case 'S':   // Sword
                    go = Instantiate(sword_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    break;
                case 'Q':   // Quarter - Disk
                    go = Instantiate(disk_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    armLengthScript = go.GetComponentInChildren<ArmLength>();
                    armLengthScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                    break;
                case 'W':   // World
                    go = Instantiate(world_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    break;
                case 'A':   // Astiroid
                    go = Instantiate(astiroid_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    break;
                case 'J':   // Jet Rocket
                    go = Instantiate(rocket_prefab, headObject.transform);                    
                    go.transform.SetParent(headObject.transform);
                    break;
                case 'K':   // 5KW point light
                    go = Instantiate(pointlight_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    lightColotScript = go.GetComponentInChildren<LightColor>();
                    lightColotScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                    break;
                case 'k':   // 5kW spot light
                    go = Instantiate(spotlight_prefab, headObject.transform);
                    go.transform.SetParent(headObject.transform);
                    lightColotScript = go.GetComponentInChildren<LightColor>();
                    lightColotScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                    break;
                case 'P':   // Dropping a Pixel - or a trail in this case
                    go = Instantiate(trail_prefab, headObject.transform);
                    pixelColorScript = go.GetComponentInChildren<PixelColor>();
                    pixelColorScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                    go.transform.SetParent(headObject.transform);
                    break;
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
                    armLengthScript = go.GetComponentInChildren<ArmLength>();
                    armLengthScript.SetColor(currentColor, useDynamic: usingDynamicColor);
                    armLengthScript.SetStatic(char.IsLower(dynaDrawCommand));
                    if (char.ToLower(dynaDrawCommand) == 'm' || char.ToLower(dynaDrawCommand) == 'b')
                        armLengthScript.SetVisibility(false);
                    if (char.ToLower(dynaDrawCommand) == 'b')
                        armLengthScript.SetBackwards();
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
                    if (char.ToLower(dynaDrawCommand) == 'u')
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

            if (index == (cursorPosition - 1) && !hideCursorPosition)
            {
                go = Instantiate(cursor_prefab, headObject.transform);
                go.transform.SetParent(headObject.transform);
            }
        }
        var instructionalScript = instructionalsObject.GetComponentInChildren<InputInstructionalScript>();
        instructionalScript.SetInstructionVisibility(showShoot: usingShooter, showTilt: usingTitler);
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
