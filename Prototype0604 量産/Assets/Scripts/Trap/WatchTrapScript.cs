using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは罠：時計をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class WatchTrapScript : MonoBehaviour
{
    int changeCount = 0;
    Vector3 initPos;
    Quaternion initRotation;
    float VibrationVeloc { get; set; }

    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
        initRotation = transform.rotation;

    }
    public void InitializeParameter(float _VibrationVeloc)
    {
        VibrationVeloc = _VibrationVeloc;
    }

    public void ResetWatch()
    {
        transform.position = initPos;
        transform.rotation = initRotation;
        changeCount = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //時計の運動
        if (changeCount == 0)
        {
            if (transform.eulerAngles.z >= -5 && transform.eulerAngles.z < 95)
            {
                transform.Rotate(new Vector3(0, 0, -VibrationVeloc));
            }
            else
            {
                changeCount++;
            }
        }
        if (changeCount == 1)
        {
            if (transform.eulerAngles.z >= 265&& transform.eulerAngles.z <360)
            {
                transform.Rotate(new Vector3(0, 0, -VibrationVeloc));
            }
            else
            {
                changeCount++;
                //changeCount=0;
            }
        }
        if (changeCount == 2)
        {
            if (transform.eulerAngles.z >= 250 && transform.eulerAngles.z < 360)
            {
                transform.Rotate(new Vector3(0, 0, VibrationVeloc));
            }
            else
            {
                changeCount++;
                //changeCount=0;
            }
        }
        if (changeCount == 3)
        {
            if (transform.eulerAngles.z >= -5 && transform.eulerAngles.z < 90)
            {
                transform.Rotate(new Vector3(0, 0, VibrationVeloc));
            }
            else
            {
                changeCount=0;
            }
        }
    }
}
