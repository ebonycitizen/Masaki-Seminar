﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これはステージ3全般をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class Stage3Script : MonoBehaviour
{
    //参照
    GameObject FallenTrap;
    GameObject FallenTrap2;
    GameObject FallenTrap3;
    //パラメータ
    public static string fallenTrapName = "プレス機１";
    public float fallenSpeed = 0.5f;
    public float raiseSpeed = 0.1f;

    public float distanceToTop = 5.5f;
    public float distanceToBottom = 1;

    public static string fallenTrapName2 = "プレス機2";
    public float fallenSpeed2 = 0.5f;
    public float raiseSpeed2 = 0.5f;

    public float distanceToTop2 = 5.5f;
    public float distanceToBottom2 = 1;

    public static string fallenTrapName3 = "プレス機3";
    public float fallenSpeed3 = 0.1f;
    public float raiseSpeed3 = 0.1f;

    public float distanceToTop3 = 5f;
    public float distanceToBottom3 = -5;
    // Use this for initialization
    void Start()
    {
        FallenTrap = GameObject.Find("FallenTrapSet1");
        FallenTrap2 = GameObject.Find("FallenTrapSet2");
        FallenTrap3 = GameObject.Find("FallenTrapSet3");

        FallenTrap.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop, distanceToBottom, fallenSpeed, raiseSpeed);
        FallenTrap2.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop2, distanceToBottom2, fallenSpeed2, raiseSpeed2);
        FallenTrap3.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop3, distanceToBottom3, fallenSpeed3, raiseSpeed3);

    }

    // Update is called once per frame
    void Update()
    {
        FallenTrap.GetComponent<PressMachineTrapScript>().
            SetTwoWaysTrap();

        FallenTrap2.GetComponent<PressMachineTrapScript>().
            SetTwoWaysTrap();

        FallenTrap3.GetComponent<PressMachineTrapScript>().
           SetTwoWaysTrap();

        if (UIScript.parameter_ChangeEnable)
        {
            SetParameterInRealTime();
        }


    }
    /// <summary>
    /// これはリアルタイムでパラメータを反映させる関数
    /// </summary>
    void SetParameterInRealTime()
    {

        FallenTrap.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop, distanceToBottom, fallenSpeed, raiseSpeed);
        FallenTrap2.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop2, distanceToBottom2, fallenSpeed2, raiseSpeed2);
        FallenTrap3.GetComponent<PressMachineTrapScript>().InitializeParameter(distanceToTop3, distanceToBottom3, fallenSpeed3, raiseSpeed3);

    }
}
