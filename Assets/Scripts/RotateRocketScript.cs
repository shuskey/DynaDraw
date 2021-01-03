using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRocketScript : MonoBehaviour
{
    Vector3 prevPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
