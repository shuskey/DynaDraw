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

    // Start is called before the first frame update
    void Start()
    {

    }

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
            
            if (showShoot && !showTilt && instructional.tag == TagsICareAbout.Shoot.ToString())
                newActiveState = true;
            if (!showShoot && showTilt && instructional.tag == TagsICareAbout.Tilt.ToString())
                newActiveState = true;
            if (showShoot && showTilt && instructional.tag == TagsICareAbout.ShootTilt.ToString())
                newActiveState = true;

            if (showShoot && instructional.tag == TagsICareAbout.MobileButton.ToString())
                newActiveState = true;
            if (showTilt && instructional.tag == TagsICareAbout.MobileJoystick.ToString())
                newActiveState = true;

            instructional.SetActive(newActiveState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
