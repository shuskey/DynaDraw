using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputInstructionalScript : MonoBehaviour
{
    private GameObject[] allShootInstructionals;
    private GameObject[] allTiltInstructionals;
    private GameObject[] allShootTiltInstructionals;
    private GameObject[] allInstructionals;

    // Start is called before the first frame update
    void Start()
    {
        allShootInstructionals = GameObject.FindGameObjectsWithTag("Shoot");
        allTiltInstructionals = GameObject.FindGameObjectsWithTag("Tilt");
        allShootTiltInstructionals = GameObject.FindGameObjectsWithTag("ShootTilt");
        allInstructionals = allShootInstructionals.Concat(allTiltInstructionals).ToArray().Concat(allShootTiltInstructionals).ToArray();

        foreach (var instructional in allInstructionals)        
            instructional.SetActive(false);
    }

    public void SetInstructionVisibility(bool showShoot, bool showTilt)
    {
        foreach (var instructional in allInstructionals)
        {
            bool newActiveState = false;
            
            if (showShoot && !showTilt && instructional.tag == "Shoot")
                newActiveState = true;
            if (!showShoot && showTilt && instructional.tag == "Tilt")
                newActiveState = true;
            if (showShoot && showTilt && instructional.tag == "ShootTilt")
                newActiveState = true;

            instructional.SetActive(newActiveState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
