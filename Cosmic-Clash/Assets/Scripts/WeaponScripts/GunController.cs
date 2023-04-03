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

    float rifleTimeInterval = 0.07f;
    int bulletSpeed = 1000;

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
        currentGunIndex = 0;
    }

    void Update()
    {
        // Setting target direction
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gunHolder.transform.position;

        // Swap 
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGunIndex < totalGuns - 1)
            {
                guns[currentGunIndex].SetActive(false);
                currentGunIndex++;
                guns[currentGunIndex].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGunIndex > 0)
            {
                guns[currentGunIndex].SetActive(false);
                currentGunIndex--;
                guns[currentGunIndex].SetActive(true);
            }
        }
        
        // Calling Current Gun's Shoot Method
        if (Input.GetMouseButton(0))
        {
            gunScripts[currentGunIndex].Shoot(direction);
        }
    }

}
