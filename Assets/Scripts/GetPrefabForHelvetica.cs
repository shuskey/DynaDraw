using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrefabForHelvetica : MonoBehaviour
{
    public float helveticaZoomFactor = 0.3f;

    public GameObject GetPrefab(char charLetter)
    {
        GameObject LetterToShow;

        if (charLetter == '/')
        {
            LetterToShow = transform.Find("_Alphabets/" + "slash").gameObject; //special case for "/" since it cannot be used for obj name in fbx					
        }
        else if (charLetter == '.')
        {
            LetterToShow = transform.Find("_Alphabets/" + "period").gameObject; //special case for "." - naming issue	
        }
        else
        {
            LetterToShow = transform.Find("_Alphabets/" + charLetter).gameObject;
        }

        return LetterToShow;
    }
}
