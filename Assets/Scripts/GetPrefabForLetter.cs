using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrefabForLetter : MonoBehaviour
{
    public GameObject[] alphabet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetPrefab(char charLetter)
    {
        if (charLetter == ' ')
            return alphabet[26];

        charLetter = System.Char.ToLower(charLetter);

        var index = (int)charLetter - (int)'a';
        index = (index > 25) ? 25 : index;
        index = (index < 0) ? 0 : index;
        return alphabet[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
