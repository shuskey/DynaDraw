using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRocketScript : MonoBehaviour
{
    Vector3 prevPosition = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (prevPosition != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - prevPosition, Vector3.forward);
        }
        prevPosition = transform.position;
    }
}
