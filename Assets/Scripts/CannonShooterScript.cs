using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooterScript : MonoBehaviour
{

    TilterControls tiltControl;
    public GameObject cannonBallPrefab;
    public float shootForce = 2f;
    // Start is called before the first frame update
    private void Awake()
    {
        tiltControl = new TilterControls();
        tiltControl.Cannon.Shoot.performed += cntxt => Shoot();        
    }
    void Start()
    {
        
    }

    void Shoot()
    {
        GameObject projectile = (GameObject)Instantiate(cannonBallPrefab, transform);
        projectile.transform.parent = null; // Set it Free
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.up * shootForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnEnable()
    {
        tiltControl.Cannon.Enable();
    }

    void OnDisable()
    {
        tiltControl.Cannon.Disable();
    }
}
