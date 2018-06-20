using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStage2 : MonoBehaviour
{
    SetResetStage2 s2;

    // Use this for initialization
    void Start()
    {
        s2 = new SetResetStage2();
        s2.InitPlayer();
        s2.SetNowStage();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        s2.UpdateReset();
    }
}
