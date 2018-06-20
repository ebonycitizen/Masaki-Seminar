using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStage3 : MonoBehaviour
{
    SetResetStage3 s3;

    // Use this for initialization
    void Start()
    {
        s3 = new SetResetStage3();
        s3.InitPlayer();
        s3.SetNowStage();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        s3.UpdateReset();
    }
}
