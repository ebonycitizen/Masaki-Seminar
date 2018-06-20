using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これはステージ6全般をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class Stage6Script : MonoBehaviour
{
    //参照
    const int mshinIto_Count = 5;
    GameObject[] mshinIto = new GameObject[mshinIto_Count];

    GameObject pressMachine;
    GameObject pressMachine2;

    GameObject switch1;
    GameObject switch2;
    //パラメータ
    public static string mshinItoName = "ミシン糸１";
    public float fallenVeloc = 3;
    public float conflictForce = 5;

    public static string mshinItoName2 = "ミシン糸２";
    public float fallenVeloc2 = 3;
    public float conflictForce2 = 5;

    public static string mshinItoName3 = "ミシン糸３";
    public float fallenVeloc3 = 3;
    public float conflictForce3 = 5;

    public static string mshinItoName4 = "ミシン糸４";
    public float fallenVeloc4 = 3;
    public float conflictForce4 = 5;

    public static string mshinItoName5 = "ミシン糸５";
    public float fallenVeloc5 = 3;
    public float conflictForce5 = 5;

    public static string pressMachineName = "プレス機１";
    public float fallenSpeed = -0.1f;
    public float raiseSpeed = 0.1f;
    public float distanceToTop = 4f;
    public float distanceToBottom = -4;

    public static string pressMachineName2 = "プレス機２";
    public float fallenSpeed2 = -0.1f;
    public float raiseSpeed2 = 0.1f;
    public float distanceToTop2 = 5.5f;
    public float distanceToBottom2 = 1;

    public void ResetStage()
    {
        for (int i = 0; i < mshinIto_Count; i++)
        {
            mshinIto[i].GetComponent<MshinItoScript>().ResetFallenStone();
        }

        pressMachine.GetComponent<PressMachineTrapScript>().ResetPressMachine();
        pressMachine2.GetComponent<PressMachineTrapScript>().ResetPressMachine();

        switch1.GetComponent<SwitchScript>().ResetSwitch();
        switch2.GetComponent<SwitchScript>().ResetSwitch();
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < mshinIto_Count; i++)
        {
            mshinIto[i] = GameObject.Find("ito" + (i + 1));
        }

        pressMachine = GameObject.Find("PressMachineSet1");
        pressMachine2 = GameObject.Find("PressMachineSet2");

        switch1 = GameObject.Find("Switch1");
        switch2 = GameObject.Find("Switch2");

        mshinIto[0].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc, conflictForce);
        mshinIto[1].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc2, conflictForce2);
        mshinIto[2].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc3, conflictForce3);
        mshinIto[3].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc4, conflictForce4);
        mshinIto[4].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc5, conflictForce5);


        pressMachine.GetComponent<PressMachineTrapScript>().InitializeParameter
            (distanceToTop, distanceToBottom, fallenSpeed, raiseSpeed);
        pressMachine2.GetComponent<PressMachineTrapScript>().InitializeParameter
            (distanceToTop2, distanceToBottom2, fallenSpeed2, raiseSpeed2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mshinIto[0].GetComponent<MshinItoScript>().SetFallenStoneRigid();
        mshinIto[0].GetComponent<MshinItoScript>().SetFallenStone();

        if (switch1.GetComponent<SwitchScript>().trapEnable)
        {
            mshinIto[1].GetComponent<MshinItoScript>().SetFallenStoneRigid();
            mshinIto[2].GetComponent<MshinItoScript>().SetFallenStoneRigid();
            mshinIto[1].GetComponent<MshinItoScript>().SetFallenStone();
            mshinIto[2].GetComponent<MshinItoScript>().SetFallenStone();

        }

        if (switch2.GetComponent<SwitchScript>().trapEnable)
        {
            mshinIto[3].GetComponent<MshinItoScript>().SetFallenStoneRigid();
            mshinIto[4].GetComponent<MshinItoScript>().SetFallenStoneRigid();
            mshinIto[3].GetComponent<MshinItoScript>().SetFallenStone();
            mshinIto[4].GetComponent<MshinItoScript>().SetFallenStone();
        }

        pressMachine.GetComponent<PressMachineTrapScript>().
            SetTwoWaysTrap();

        pressMachine2.GetComponent<PressMachineTrapScript>().
    SetTwoWaysTrap();

        if (UIScript.parameter_ChangeEnable)
            SetParameterInRealTime();
    }
    /// <summary>
    /// これはリアルタイムでパラメータを反映させる関数
    /// </summary>
    void SetParameterInRealTime()
    {
        mshinIto[0].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc, conflictForce);
        mshinIto[1].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc2, conflictForce2);
        mshinIto[2].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc3, conflictForce3);
        mshinIto[3].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc4, conflictForce4);
        mshinIto[4].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc5, conflictForce5);

        pressMachine.GetComponent<PressMachineTrapScript>().InitializeParameter
    (distanceToTop, distanceToBottom, fallenSpeed, raiseSpeed);
        pressMachine2.GetComponent<PressMachineTrapScript>().InitializeParameter
            (distanceToTop2, distanceToBottom2, fallenSpeed2, raiseSpeed2);
    }
}
