using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public int currentGunIndex;

    public GameObject[] guns;
    GunScript[] gunScripts;
    public GameObject gunHolder;

    private int totalGunsAvailable;
    private bool[] gunLocks;
    private GameManager gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        totalGunsAvailable = gunHolder.transform.childCount;
        guns = new GameObject[totalGunsAvailable];
        gunScripts = new GunScript[totalGunsAvailable];
        gunLocks = new bool[] { true, false, false };

        for (int i = 0; i < totalGunsAvailable; i++)
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
        for (int i = currentGunIndex - 1; i >= 0; i--) {
            if (gunLocks[i]) {
                guns[currentGunIndex].SetActive(false);
                guns[i].SetActive(true);
                currentGunIndex = i;
                break;
            }
        }
    }

    public void RightGunSwap()
    {
        for (int i = currentGunIndex + 1; i < totalGunsAvailable; i++) {
            if (gunLocks[i]) {
                guns[currentGunIndex].SetActive(false);
                guns[i].SetActive(true);
                currentGunIndex = i;
                break;
            }
        }
    }

    public void Shoot(Vector3 mousePos)
    {
        Vector3 direction = mousePos - gunHolder.transform.position;
        gunScripts[currentGunIndex].Shoot(direction);
    }

    public void UnlockWeapon(string weaponTag)
    {
        if(weaponTag == "rifle") {
            gunLocks[0] = true;
            gameManager.OpenInstruction("RIFLE UNLOCKED!\nQ to Switch to Previous Weapon\nE to Switch to Next Weapon");
        }
        else if(weaponTag == "shotgun") {
            gunLocks[1] = true;
            gameManager.OpenInstruction("SHOTGUN UNLOCKED!\nQ to Switch to Previous Weapon\nE to Switch to Next Weapon");
        }
        else if(weaponTag == "laser") {
            gunLocks[2] = true;
            gameManager.OpenInstruction("LASER UNLOCKED!\nQ to Switch to Previous Weapon\nE to Switch to Next Weapon");
        }
    }

}
