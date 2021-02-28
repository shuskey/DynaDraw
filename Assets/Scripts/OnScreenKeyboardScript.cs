using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenKeyboardScript : MonoBehaviour
{
    public GameObject drawStringObject;
    public InputField inputFieldCommands;
    public Button keyboardKeyPrefab;
    public string[] keysForThisLine;

    // Start is called before the first frame update
    void Start()
    {
        PopulateOnScreenKeyboard();
    }

    static System.Tuple<byte, string> convertToKeyDefinitionPair(char keyValue)
    {
        System.Tuple<byte, string> returnThis = new System.Tuple<byte, string>((byte)keyValue, keyValue.ToString());
        return returnThis;
    }
    static System.Tuple<byte, string>[] convertStringToKeyDefinition(string sixChars)
    {
        var returnThis = new System.Tuple<byte, string>[6];
        for (var i = 0; i < 6; i++)
        {
            if (sixChars.Substring(i, 1) == " ")  // Special Case skip this button
                returnThis[i] = new System.Tuple<byte, string>(0x00, "");
            else
                returnThis[i] = convertToKeyDefinitionPair(sixChars.Substring(i, 1).ToCharArray()[0]);
        }
        return returnThis;
    }
    
    void PopulateOnScreenKeyboard()
    {
        var specialControls = new System.Tuple<byte, string>[] { new System.Tuple<byte, string>(0x1b, "<"), new System.Tuple<byte, string>(0x1a, ">"), new System.Tuple<byte, string>(0x20, "Sp"), new System.Tuple<byte, string>(0x7f, "Del"), new System.Tuple<byte, string>(0x08, "Bksp"), new System.Tuple<byte, string>(0x22, "\"") };

        var offset = new Vector3(0, 0, 0);
        int line = 0;
        foreach (var thisLinesString in keysForThisLine)            
        {
            var lineOfKeys = convertStringToKeyDefinition(thisLinesString);
            if (thisLinesString == "<<<>>>")
                lineOfKeys = specialControls;

            for (var row = 0; row < lineOfKeys.Length; row++)
            {
                if (lineOfKeys[row].Item1 != 0x00)
                {
                    var keyButtonGameObject = Instantiate(keyboardKeyPrefab, gameObject.transform.position + offset, gameObject.transform.rotation);
                    var textElement = keyButtonGameObject.GetComponentInChildren<Text>();
                    textElement.fontSize = 12;
                    textElement.text = lineOfKeys[row].Item2;

                    keyButtonGameObject.transform.SetParent(gameObject.transform);

                    var passThisToDelegateKeyValue = lineOfKeys[row].Item1;                     
                    keyButtonGameObject.onClick.AddListener(delegate { KeyBoardButtonClicked(passThisToDelegateKeyValue); });
                }

                offset.x += 25.0f;
            }
            offset.x = 0.0f;
            offset.y -= 25.0f;
            line++;
        }
    }

    void KeyBoardButtonClicked(byte bytaKeyValue)
    {
        //Debug.Log("On Screen Key Pressed for " + allKeyDefinitionLines[line][row].Item2);
        inputFieldCommands.SetTextWithoutNotify(drawStringObject.GetComponentInChildren<DrawStringScript>().SendKey(bytaKeyValue));
    }
}
