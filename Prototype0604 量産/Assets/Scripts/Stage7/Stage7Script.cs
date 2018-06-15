﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これはステージ7全般をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class Stage7Script : MonoBehaviour
{
    //参照
    const int translateMachineCount = 4;
    GameObject[] translateMachine = new GameObject[translateMachineCount];

    //パラメータ
    public static string mshinItoName = "転送装置";
    public float translateVeloc = 3;
    public float bottomPosy = 5;
    public float topPosy = 5;
    
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < translateMachineCount; i++)
        {
            translateMachine[i] = GameObject.Find("TranslateMachince" + (i + 1));
        }
        for (int i = 0; i < translateMachineCount; i++)
        {
            translateMachine[i].GetComponent<TranslateMachinceScript>().InitializeParameter
                (translateVeloc, topPosy, bottomPosy);
        }
    }

    public void ResetStage()
    {
        for (int i = 0; i < translateMachineCount; i++)
        {
            translateMachine[i].GetComponent<TranslateMachinceScript>().ResetTranslateMachine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UIScript.parameter_ChangeEnable)
            SetParameterInRealTime();
    }
    /// <summary>
    /// これはリアルタイムでパラメータを反映させる関数
    /// </summary>
    void SetParameterInRealTime()
    {
        for (int i = 0; i < translateMachineCount; i++)
        {
            translateMachine[i].GetComponent<TranslateMachinceScript>().InitializeParameter
                (translateVeloc, topPosy, bottomPosy);
        }
    }
}
