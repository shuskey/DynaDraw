using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextEditHandler : MonoBehaviour
{
    public GameObject DrawStringObject;
    // Start is called before the first frame update
    void Start()
    {
        var textEdit = transform.GetComponent<InputField>();        
        textEdit.onValueChanged.AddListener(delegate { TextEditValueChanged(textEdit); });
    }

    void TextEditValueChanged(InputField textEdit)
    {
        var text = textEdit.text;
        var drawStingScript = DrawStringObject.GetComponentInChildren<DrawStringScript>();
        var caretPosition = textEdit.caretPosition;
        drawStingScript.Restart(text, caretPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
