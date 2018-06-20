using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResetStage3 : ResetStage
{
    Stage3Script stage3Script;
    public override void SetNowStage()
    {
        stage3Script = GameObject.Find("Stage3").GetComponent<Stage3Script>();
    }
    public override void ResetThisStage()
    {
        stage3Script.ResetStage();
    }
}
