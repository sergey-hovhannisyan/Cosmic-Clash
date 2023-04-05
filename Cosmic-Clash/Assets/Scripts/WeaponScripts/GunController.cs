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

    void Start()
    {
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

    public void LeftGunSwap()
    {
        if (currentGunIndex > 0)
        {
            guns[currentGunIndex].SetActive(false);
            currentGunIndex--;
            guns[currentGunIndex].SetActive(true);
        }
    }

    public void RightGunSwap()
    {
        if (currentGunIndex < totalGuns - 1)
        {
            guns[currentGunIndex].SetActive(false);
            currentGunIndex++;
            guns[currentGunIndex].SetActive(true);
        }
    }

    public void Shoot(Vector3 mousePos)
    {
        Vector3 direction = mousePos - gunHolder.transform.position;
        gunScripts[currentGunIndex].Shoot(direction);
    }

}
