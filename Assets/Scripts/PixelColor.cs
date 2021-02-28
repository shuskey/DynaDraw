using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelColor : MonoBehaviour
{
    [SerializeField] [Tooltip("Color")] private Color color = Color.white;
    [SerializeField] [Tooltip("Taste the rainbow!")] private bool useDynamicColor = false;
    public void SetColor(Color color, bool useDynamic)
    {  
        var particleSystem = GetComponentsInChildren<ParticleSystem>();
        var main = particleSystem[0].main;
        main.startColor = color;
        useDynamicColor = useDynamic;
    }

    // Update is called once per frame
    void Update()
    {
        if (useDynamicColor)
            SetColor(HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.1f, 1), 1, 1)), true);
    }
}
