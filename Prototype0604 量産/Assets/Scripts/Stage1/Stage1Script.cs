﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Script : MonoBehaviour {
    GameObject FallenTrap;
    GameObject FallenTrap2;

    bool fallenEnable;
    bool fallenEnable2;

    public float fallenSpeed=0.5f;
    public float raiseSpeed=0.1f;

    public float fallenSpeed2 = 0.5f;
    public float raiseSpeed2 = 0.5f;
    // Use this for initialization
    void Start () {
        FallenTrap = GameObject.Find("FallenTrapSet");
        fallenEnable = true;

        FallenTrap2 = GameObject.Find("FallenTrapSet2");
        fallenEnable2 = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fallenEnable)
        {
            if (FallenTrap.transform.localPosition.y > 1)
            {
                FallenTrap.transform.position -= new Vector3(0, fallenSpeed, 0);
            }
            else
                fallenEnable = !fallenEnable;
        }
        else
        {
            if (FallenTrap.transform.localPosition.y < 5.5)
            {
                FallenTrap.transform.position += new Vector3(0, raiseSpeed, 0);
            }
            else
                fallenEnable = !fallenEnable;
        }

        if (fallenEnable2)
        {
            if (FallenTrap2.transform.localPosition.y > 1.0)
            {
                FallenTrap2.transform.position -= new Vector3(0, fallenSpeed2, 0);
            }
            else
                fallenEnable2 = !fallenEnable2;
        }
        else
        {
            if (FallenTrap2.transform.localPosition.y < 5)
            {
                FallenTrap2.transform.position += new Vector3(0, raiseSpeed2, 0);
            }
            else
                fallenEnable2 = !fallenEnable2;
        }
    }
}
