﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// これはプレイヤーのライフをコントロールするクラス
/// </summary>
public class PlayerLifeControl : MonoBehaviour
{

    public int lifeCount;
    bool invincble = false;

    GameObject Eff_Invincble;
    GameObject soul;
    GameObject frontWall;


    void Start()
    {
        //ライフ初期化
        lifeCount = 1;

        Eff_Invincble = GameObject.Find("Eff_PowerUp_Fix");

        soul = GameObject.Find("Soul");

        frontWall = GameObject.Find("FrontWall");
    }
    private void ResetLife()
    {
        lifeCount = 1;
        invincble = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (lifeCount <= 0)
        {
            lifeCount = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!invincble)
        {
            //tag名が”トラップ”のオブジェと遭遇したら、ライフ数を減らす
            if (collision.gameObject.tag == "Trap")
            {
                lifeCount--;
                soul.GetComponent<DeadPerformanceScript>().SetCircleParameter(transform.position);
                invincble = true;
                transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
            if (collision.gameObject.name == "OutArea")
            {
                lifeCount--;
                invincble = true;
                soul.GetComponent<DeadPerformanceScript>().SetSquareParameter(transform.position);
                transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
            if (collision.gameObject.tag == "Stone")
            {
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
                {
                    invincble = true;
                    lifeCount--;
                    soul.GetComponent<DeadPerformanceScript>().SetCircleParameter(transform.position);
                    transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!invincble)
        {
            //tag名が”トラップ”のオブジェと遭遇したら、ライフ数を減らす
            if (collision.gameObject.tag == "Trap")
            {
                invincble = true;
                lifeCount--;
                soul.GetComponent<DeadPerformanceScript>().SetCircleParameter(transform.position);
                transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
            if (collision.gameObject.name == "OutArea")
            {
                invincble = true;
                lifeCount--;
                soul.GetComponent<DeadPerformanceScript>().SetSquareParameter(transform.position);
                transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
            if (collision.gameObject.tag == "Stone")
            {
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
                {
                    invincble = true;
                    lifeCount--;
                    soul.GetComponent<DeadPerformanceScript>().SetCircleParameter(transform.position);
                    transform.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                }
            }
        }
    }
}
