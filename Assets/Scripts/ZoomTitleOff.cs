using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTitleOff : MonoBehaviour
{
    public float timeToLive = 10;
    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }
}
