using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonVisibilityScript : MonoBehaviour
{

    void Awake()
    {
        gameObject.SetActive(true);
#if !UNITY_EDITOR && UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }

}
