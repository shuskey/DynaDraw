using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextEditHandler : MonoBehaviour
{
    public GameObject DrawStringObject;
    private InputField textEdit;
    private DrawStringScript drawStringScript;
    private int currentCursorPosition = 0;
    private bool lastTimeICheckedFocusWasTrue = false;
    
    // Start is called before the first frame update
    void Start()
    {
        textEdit = transform.GetComponent<InputField>();        
        textEdit.onValueChanged.AddListener(delegate { TextEditValueChanged(textEdit); });

        drawStringScript = DrawStringObject.GetComponentInChildren<DrawStringScript>();
    }

    void TextEditValueChanged(InputField textEdit)
    {
        var text = textEdit.text;        
        currentCursorPosition = textEdit.caretPosition;
        drawStringScript.Restart(text, currentCursorPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!textEdit.isFocused) // does not have focus
        {
            if (lastTimeICheckedFocusWasTrue) // just lost focus
            {
                drawStringScript.Redraw(-1);  // turn off cursor caret thing
                lastTimeICheckedFocusWasTrue = false;
                return;
            }
        }
        else  // does have focus
        {
            lastTimeICheckedFocusWasTrue = true;
            if (textEdit.caretPosition != currentCursorPosition)
            {
                currentCursorPosition = textEdit.caretPosition;
                drawStringScript.Restart(textEdit.text, currentCursorPosition);
            }
            return;
        }
    }
}
