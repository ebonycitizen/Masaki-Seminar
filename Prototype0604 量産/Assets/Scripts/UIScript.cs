﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// これはUI全般をコントロールするクラスです
/// 作成者:huyp
/// </summary>
public class UIScript : MonoBehaviour
{
    //参照
    GameObject lightOn_Button;
    GameObject retry_Button;

    GameObject worldLight;
    GameObject player;

    //コンポーネント
    public Text lightSwitch_Text;
    public Text playerlife_Text;

    public bool turnLightOn;

    public static bool parameter_ChangeEnable = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        lightOn_Button = GameObject.Find("LightOn_Button");
        retry_Button = GameObject.Find("Retry_Button");

        worldLight = GameObject.FindGameObjectWithTag("MainLight");


        turnLightOn = false;

        lightOn_Button.SetActive(false);
        retry_Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //turnlightonフラグを頼って、ワールドライトの輝度を調整する
        if (!turnLightOn)
        {
            lightSwitch_Text.text = "LightON(Num1)";
            worldLight.GetComponent<Light>().intensity = 0.01f;
            worldLight.GetComponent<Light>().color = Color.black;
        }
        else
        {
            lightSwitch_Text.text = "LightOFF(Num1)";
            worldLight.GetComponent<Light>().intensity = 2f;
            worldLight.GetComponent<Light>().color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("LightOn"))
        {
            turnLightOn = !turnLightOn;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("Retry"))
        {
            player.GetComponent<PlayerLifeControl>().lifeCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            parameter_ChangeEnable = !parameter_ChangeEnable;
        }
    }

    /// <summary>
    /// これはLIGHT ONボタンでコントロールする関数です
    /// 呼び出したら、輝度を反転させる；
    /// </summary>
    public void ChangeTheLightIndensity()
    {
        turnLightOn = !turnLightOn;
    }

    /// <summary>
    /// これはリトライボタンでコントロールする関数です
    /// 呼び出したら、プレイヤーのライフ数を減らすをゼロまで減らす；
    /// </summary>
    public void RetryFunction()
    {
        player.GetComponent<PlayerLifeControl>().lifeCount = 0;
    }
}
