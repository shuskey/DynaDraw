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
            //Vector3 rotation = Quaternion.LookRotation(transform.position - prevPosition).eulerAngles;            
            //transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
            //var deltaX = transform.position.x - prevPosition.x;
            //var deltaY = transform.position.y - prevPosition.y;
            //var deltaZ = transform.position.z - prevPosition.z;
            //var xRotation = Mathf.Rad2Deg * Mathf.Atan2(deltaY, deltaZ);
            ////var yRotation = Mathf.Rad2Deg * Mathf.Atan2(deltaX, deltaZ);
            ////var zRotation = Mathf.Rad2Deg * Mathf.Atan2(deltaY, deltaX);
            //transform.eulerAngles = new Vector3(xRotation, 0, 0);
        }
        prevPosition = transform.position;
    }
}
