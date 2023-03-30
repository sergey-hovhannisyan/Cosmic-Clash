using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAiming : MonoBehaviour
{
    public Transform[] armTrans;
    public Transform[] gunHoldingTrans;

    void Start()
    {
        
    }

    void Update()
    {
        armTrans[0].position = gunHoldingTrans[0].position;
        armTrans[1].position = gunHoldingTrans[1].position;
        // Debug.Log(gunHoldingTrans[0].position);
        // Debug.Log(armTrans[0].position);
    }
}
