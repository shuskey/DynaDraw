using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLength : MonoBehaviour
{
    [SerializeField] [Tooltip("Overall Speed when dynamic")] private float speed = 10;
    [SerializeField] [Tooltip("Dyanmic or Static")] private bool isStatic = true;
    [SerializeField] [Tooltip("Static Length")] private int length = 10;
    [SerializeField] [Tooltip("Static Direction, -1 if backwards")] private int direction = 1;
    [SerializeField] [Tooltip("Color")] private Color color = Color.white;
    [SerializeField] [Tooltip("Taste the rainbow!")] private bool useDynamicColor = false;

    float dynamicLength = 0;
    int dynamicDirection = -1;    

    // Start is called before the first frame update
    void Start()
    {
        if (isStatic)
        {            
            var currentPosition = this.transform.position;
            var currentLocalPosition = this.transform.localPosition;
            this.transform.localPosition = new Vector3(0, length * direction, 0);      
        }
    }

    public void SetColor(Color color, bool useDynamic)
    {
        var children = GetComponentsInChildren<Renderer>();
        var trailrenderer = GetComponentsInChildren<TrailRenderer>();
        children[0].material.color = color;
        useDynamicColor = useDynamic;
        
    }

    public void SetVisibility(bool isVisible)
    {
        var children = GetComponentsInChildren<Renderer>();

        children[0].forceRenderingOff = !isVisible;
    }

    public void SetStatic(bool newStaticValue)
    {
        isStatic = newStaticValue;
    }

    public void SetBackwards()
    {
        direction = -direction;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
        {
            dynamicLength += Time.deltaTime * speed * dynamicDirection;

            if (dynamicLength > length)
            {
                dynamicLength = length;
                dynamicDirection = -1;
            }
            if (dynamicLength < -length)
            {
                dynamicLength = -length;
                dynamicDirection = 1;
            }
            this.transform.localPosition = new Vector3(0, dynamicLength * direction, 0);            
        }
        if (useDynamicColor)
            SetColor(HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 0.1f, 1), 1, 1)), true);
    }
}
