using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    int totalGuns = 1;
    public int currentGunIndex;

    public GameObject[] guns;
    public GameObject gunHolder;
    public GameObject currentGun; 

    void Start()
    {
        totalGuns = gunHolder.transform.childCount;
        guns = new GameObject[totalGuns];

        for (int i = 0; i < totalGuns; i++)
        {
            guns[i] = gunHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
        currentGun = guns[0];
        currentGunIndex = 0;
    }

    void Update()
    {
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
    }
}
