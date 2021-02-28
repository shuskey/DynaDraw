using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Tilter : MonoBehaviour
{    
    public float Strength = 1.0f;
    public int direction = 1;
    public float speed = 50.0f;
    TilterControls tiltControl;
    Vector2 tilt;
    private void Awake()
    {
        tiltControl = new TilterControls();
        tiltControl.Tilter.Tilt.performed += cntxt => tilt = cntxt.ReadValue<Vector2>();
        tiltControl.Tilter.Tilt.canceled += cntxt => tilt = Vector2.zero;
    }

    void Update()
    {
        if (tilt == Vector2.zero)
            return;

        var destinationRotation = new Vector3(tilt.y * 90 * Strength * direction, 0, tilt.x * -90 * Strength * direction);                
        var currentRoatation = GetComponent<Transform>().localRotation;

        // The step size is equal to speed times frame time.
        var step = speed * Time.deltaTime;

        // Rotate our transform a step closer to the target's.
        GetComponent<Transform>().localRotation = Quaternion.RotateTowards(currentRoatation, Quaternion.Euler(destinationRotation), step);
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
