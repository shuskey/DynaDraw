using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBuildVersion : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {        
        gameObject.GetComponent<Text>().text = $"Version {Application.version}";
    }
}
