using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CenterAndScale(this.transform.parent.localPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        CenterAndScale(this.transform.parent.localPosition.y);
    }

    void CenterAndScale(float parentSize)
    {        
        this.transform.localScale = new Vector3(0.5f, parentSize, 0.5f);
        this.transform.localPosition = new Vector3(0, parentSize / -2, 0);
    }
}
