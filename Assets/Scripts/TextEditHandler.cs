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
        if (textEdit.caretPosition != currentCursorPosition)
        {
            currentCursorPosition = textEdit.caretPosition;
            drawStringScript.Restart(textEdit.text, currentCursorPosition);
        }
    }
}
