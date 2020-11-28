using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHorizontally : MonoBehaviour
{
    [SerializeField] [Tooltip("Overall Speed")] private float speed = 180;
    [SerializeField] [Tooltip("Dyanmic or Static")] private bool isStatic = false;
    [SerializeField] [Tooltip("Set to 1, 0, or -1")] private int xDirection = 0;
    [SerializeField] [Tooltip("Set to 1, 0, or -1")] private int yDirection = 0;
    [SerializeField] [Tooltip("Set to 1, 0, or -1")] private int zDirection = 1;
    [SerializeField] [Tooltip("Static angle")] private float staticAngle = 15;
    [SerializeField] [Tooltip("Color")] private Color color = Color.white;
    [SerializeField] [Tooltip("Taste the rainbow!")] private bool useDynamicColor = false;

    float xRotation = 0;
    float yRotation = 0;
    float zRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetLocalRotation();
    }

    public void SetColor(Color color, bool useDynamic)
    {
        var children = GetComponentsInChildren<Renderer>();

        children[0].material.color = color;
        useDynamicColor = useDynamic;
    }

    public void SetDiection(int setXDirection, int setYDirection, int setZDirection)
    {
        xDirection = setXDirection;
        yDirection = setYDirection;
        zDirection = setZDirection;

        SetLocalRotation();
    }

    public void SetLocalRotation()
    {
        xRotation = xDirection * staticAngle;
        yRotation = yDirection * staticAngle;
        zRotation = zDirection * staticAngle;
        if (isStatic)
        {
            this.transform.localRotation = Quaternion.Euler(
                xRotation,
                yRotation,
                zRotation);
        }
    }

    public void SetStatic(bool newStaticValue)
    {
        isStatic = newStaticValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
        {
            xRotation += Time.deltaTime * speed * xDirection;
            yRotation += Time.deltaTime * speed * yDirection;
            zRotation += Time.deltaTime * speed * zDirection;
        
            this.transform.localRotation = Quaternion.Euler(
                xRotation,
                yRotation,
                zRotation);
        }
        if (useDynamicColor)
            SetColor(HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * 10, 1), 1, 1)), true);

    }
}
