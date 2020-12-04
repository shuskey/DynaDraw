using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterColor : MonoBehaviour
{
    [SerializeField] [Tooltip("Color")] private Color color = Color.white;
    [SerializeField] [Tooltip("Taste the rainbow!")] private bool useDynamicColor = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetColor(Color color, bool useDynamic)
    {
        var children = GetComponentsInChildren<Renderer>();
        if (children.Length !=0)
            children[0].material.color = color;
        useDynamicColor = useDynamic;
    }

    // Update is called once per frame
    void Update()
    {
        if (useDynamicColor)
            SetColor(HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.1f, 1), 1, 1)), true);
    }
}
