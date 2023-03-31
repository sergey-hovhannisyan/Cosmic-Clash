using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    int totalGuns = 1;
    public int currentGunIndex;

    public GameObject[] guns;
    GunScript[] gunScripts;
    public GameObject gunHolder;

    bool rifleOn = false;
    float rifleTimeInterval = 0.07f;
    int bulletSpeed = 1000;
    public Transform bulletSpawnPos;

    private Vector3 mousePos;
    private Camera mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        totalGuns = gunHolder.transform.childCount;
        guns = new GameObject[totalGuns];
        gunScripts = new GunScript[totalGuns];

        for (int i = 0; i < totalGuns; i++)
        {
            guns[i] = gunHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
            gunScripts[i] = guns[i].GetComponent<GunScript>();
        }
        guns[0].SetActive(true);
        //currentGun = guns[0];
        currentGunIndex = 0;
    }

    void Update()
    {
        
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - bulletSpawnPos.position;

        if (Input.GetKeyDown(KeyCode.E) && !rifleOn)
        {
            if (currentGunIndex < totalGuns - 1)
            {
                guns[currentGunIndex].SetActive(false);
                currentGunIndex++;
                guns[currentGunIndex].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && !rifleOn)
        {
            if (currentGunIndex > 0)
            {
                guns[currentGunIndex].SetActive(false);
                currentGunIndex--;
                guns[currentGunIndex].SetActive(true);
            }
        }

        // Shooting for laser and shotgun. Both shoot only once every click
        // if (Input.GetMouseButtonDown(0)){
        //     gunScripts[currentGunIndex].Shoot();
        // }
           

        //Vector3 fireDirection = gunHolder.transform.rotation * Vector3.forward;
        // Shooting for rifle. Continuous shooting till mouse button up
        if (Input.GetMouseButton(0))
        {
            //gunScripts[currentGunIndex].isShooting = true;
            gunScripts[currentGunIndex].Shoot(direction);
        }

        // Check for rifle stop
        //if (Input.GetMouseButtonUp(0) && gunScripts[currentGunIndex].isShooting)
        //    gunScripts[currentGunIndex].isShooting = false;

    }

}
