using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    public GameObject firstLineOfText;
    public GameObject logoToZoom;
    public GameObject starWarsTextPrefab;
    
    float loopCreditsTimer = 0.0f;
    float phaseTimer = 0.0f;
    float phase0Time = 5f;
    float phase1Time = 10;
    float phase2repeatTime = 75;
    int logoPhase = 0;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (logoPhase == 0)
        {
            if (phaseTimer == 0f)
                Instantiate(firstLineOfText, gameObject.transform);
            if (phaseTimer >= phase0Time)
            { 
                logoPhase = 1;
                phaseTimer = 0f;
            }
        }
        if (logoPhase == 1)
        {
            if (phaseTimer == 0f)
                Instantiate(logoToZoom, gameObject.transform);
            if (phaseTimer >= phase1Time)
            {
                logoPhase = 2;
                phaseTimer = 0f;
            }
        }
        if (logoPhase == 2)
        {
            if (loopCreditsTimer <= 0)
            {
                var movingTitle = Instantiate(starWarsTextPrefab, gameObject.transform);
                loopCreditsTimer = phase2repeatTime;
            }
            loopCreditsTimer -= Time.deltaTime;
        }

        phaseTimer += Time.deltaTime;
    }
}
