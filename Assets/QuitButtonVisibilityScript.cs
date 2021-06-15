using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonVisibilityScript : MonoBehaviour
{

    void Awake()
    {     
#if UNITY_WEBGL
        gameObject.SetActive(false);
#else
        gameObject.SetActive(true);
#endif
    }

}
