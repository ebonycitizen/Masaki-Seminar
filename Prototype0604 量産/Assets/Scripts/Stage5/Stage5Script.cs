using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これはステージ5全般をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class Stage5Script : MonoBehaviour
{
    //参照
    const int mshinIto_Count = 3;
    GameObject[] mshinIto = new GameObject[mshinIto_Count];

    //パラメータ
    public static string mshinItoName = "ミシン糸１";
    public float fallenVeloc = 3;
    public float conflictForce = 5;

    public static string mshinItoName2 = "ミシン糸２";
    public float fallenVeloc2 = 3;
    public float conflictForce2 = 6;

    public static string mshinItoName3 = "ミシン糸３";
    public float fallenVeloc3 = 3;
    public float conflictForce3 = 6;

    public void ResetStage()
    {
        for (int i = 0; i < mshinIto_Count; i++)
        {
            mshinIto[i].GetComponent<MshinItoScript>().ResetFallenStone();
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < mshinIto_Count; i++)
        {
            mshinIto[i] = GameObject.Find("ito" + (i + 1));
        }
        mshinIto[0].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc, conflictForce);
        mshinIto[1].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc2, conflictForce2);
        mshinIto[2].GetComponent<MshinItoScript>().InitializeParameter(fallenVeloc3, conflictForce3);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i=0;i< mshinIto.Length;i++)
        {
            if (mshinIto[i] == null)
                return;
        }
        mshinIto[0].GetComponent<MshinItoScript>().SetFallenStone();
        mshinIto[1].GetComponent<MshinItoScript>().SetFallenStone();
        mshinIto[2].GetComponent<MshinItoScript>().SetFallenStone();
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
    }
}
