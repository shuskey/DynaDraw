using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputInstructionalScript : MonoBehaviour
{
    private GameObject[] allShootInstructionals;
    private GameObject[] allTiltInstructionals;
    private GameObject[] allShootTiltInstructionals;
    private GameObject[] allMobileOnScreenButtons;
    private GameObject[] allInstructionals;
    enum TagsICareAbout { Shoot, Tilt, ShootTilt, MobileJoystick, MobileButton }

    private void Awake()
    {
        GetAllInstructionalsAndMakeNotVisible();
    }

    private void GetAllInstructionalsAndMakeNotVisible()
    {
        allShootInstructionals = GameObject.FindGameObjectsWithTag(TagsICareAbout.Shoot.ToString());
        allTiltInstructionals = GameObject.FindGameObjectsWithTag(TagsICareAbout.Tilt.ToString()); ;
        allShootTiltInstructionals = GameObject.FindGameObjectsWithTag(TagsICareAbout.ShootTilt.ToString());
        allMobileOnScreenButtons = GameObject.FindGameObjectsWithTag(TagsICareAbout.MobileJoystick.ToString())
            .Concat(GameObject.FindGameObjectsWithTag(TagsICareAbout.MobileButton.ToString())).ToArray();
        allInstructionals = allShootInstructionals.Concat(allTiltInstructionals).ToArray()
            .Concat(allShootTiltInstructionals).ToArray()
            .Concat(allMobileOnScreenButtons).ToArray();

        foreach (var instructional in allInstructionals)
            instructional.SetActive(false);
    }

    public void SetInstructionVisibility(bool showShoot, bool showTilt)
    {
        foreach (var instructional in allInstructionals)
        {
            bool newActiveState = false;
            
            if (showShoot && !showTilt && instructional.CompareTag(TagsICareAbout.Shoot.ToString()))
                newActiveState = true;
            if (!showShoot && showTilt && instructional.CompareTag(TagsICareAbout.Tilt.ToString()))
                newActiveState = true;
            if (showShoot && showTilt && instructional.CompareTag(TagsICareAbout.ShootTilt.ToString()))
                newActiveState = true;

            if (showShoot && instructional.CompareTag(TagsICareAbout.MobileButton.ToString()))
                newActiveState = true;
            if (showTilt && instructional.CompareTag(TagsICareAbout.MobileJoystick.ToString()))
                newActiveState = true;

            instructional.SetActive(newActiveState);
        }
    }

}
