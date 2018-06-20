using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResetStage2 : ResetStage
{
    Stage2Script stage2Script;
    public override void SetNowStage()
    {
        stage2Script = GameObject.Find("Stage2").GetComponent<Stage2Script>();
    }
    public override void ResetThisStage()
    {
        stage2Script.ResetStage();
    }
}
