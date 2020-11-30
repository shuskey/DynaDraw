using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Tilter : MonoBehaviour
{    
    public float Strength = 1.0f;
    public int direction = 1;
    TilterControls tiltControl;
    Vector2 tilt;
    
    void Start()
    {
        
    }
    private void Awake()
    {
        tiltControl = new TilterControls();
        tiltControl.Tilter.Tilt.performed += cntxt => tilt = cntxt.ReadValue<Vector2>();
        tiltControl.Tilter.Tilt.canceled += cntxt => tilt = Vector2.zero;
    }

    void Update()
    {
        //   this.transform.rotation = Quaternion.Euler(tilt.x * 90 * Strength, tilt.y * 90 * Strength, 0);
        Vector3 newRotation = new Vector3(tilt.y * 90 * Strength * direction, 0, tilt.x * -90 * Strength * direction);
        //GetComponent<Rigidbody>().velocity = m;
        GetComponent<Transform>().localRotation = Quaternion.Euler(newRotation);
    }

    void OnEnable()
    {
        tiltControl.Tilter.Enable();
    }

    void OnDisable()
    {
        tiltControl.Tilter.Disable();
    }
}
