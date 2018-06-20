using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これはステージ3全般をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class Stage3Script : MonoBehaviour
{
    //参照
    const int bedMachine_Count = 2;
    GameObject[] bedMachine = new GameObject[bedMachine_Count];

    GameObject stone1;
    GameObject stone2;
    //パラメータ
    public static string bedMachineName = "ベッド１";
    public float elasticity = 11;

    public static string bedMachineName2 = "ベッド２";
    public float elasticity2 = 11;

    public void ResetStage()
    {
        stone1.GetComponent<InfiniteFallingStoneScript>().ResetInfiniteFallingStone();
        stone2.GetComponent<InfiniteFallingStoneScript>().ResetInfiniteFallingStone();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Trap"))
        {
            obj.GetComponent<InfiniteFallingStoneScript>().ResetInfiniteFallingStone();
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < bedMachine_Count; i++)
        {
            bedMachine[i] = GameObject.Find("bed" + (i + 1));
        }

        bedMachine[0].GetComponent<BedMachineScript>().InitializeParameter(elasticity);
        bedMachine[1].GetComponent<BedMachineScript>().InitializeParameter(elasticity2);


        stone1 = GameObject.Find("Stone1");
        stone2 = GameObject.Find("Stone2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    /// <summary>
    /// これはリアルタイムでパラメータを反映させる関数
    /// </summary>
    void SetParameterInRealTime()
    {
        bedMachine[0].GetComponent<BedMachineScript>().InitializeParameter(elasticity);
        bedMachine[1].GetComponent<BedMachineScript>().InitializeParameter(elasticity2);
    }
}
