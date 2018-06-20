using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは装置：床の転送をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class TranslateMachinceScript : MonoBehaviour
{

    GameObject floor;
    GameObject kassa;
    GameObject kassa2;
    
    float TranslateVeloc { get; set; }
    float TopPosY { get; set; }
    float BottomPosY { get; set; }

    bool setTranslateEnable = false;
    float deltaTime_Rigidbody;
    float total_Veloc;

    Rigidbody rb;

    /// <summary>
    /// これは移動するときの初期位置の変数
    /// </summary>
    Vector3 translateInitPos;

    /// <summary>
    /// これは初期位置を記録するための変数
    /// 読み込み専用
    /// </summary>
    Vector3 defaultPos;

    // Use this for initialization
    void Start()
    {
        kassa = transform.Find("kassya").gameObject;
        kassa2 = transform.Find("kassya2").gameObject;
        floor = transform.Find("TranslateFloor").gameObject;

        translateInitPos = floor.transform.position;
        defaultPos = floor.transform.position;
        rb = floor.GetComponent<Rigidbody>();
    }

    public void ResetTranslateMachine()
    {
        deltaTime_Rigidbody=0;
        total_Veloc=0;
        setTranslateEnable = false;
        translateInitPos = defaultPos;
        rb.velocity =Vector3.zero;
        rb.MovePosition(new Vector3(defaultPos.x,
defaultPos.y,
defaultPos.z));
    }

    public void InitializeParameter(float _TranslateVeloc, float _TopPosY, float _BottomPosY)
    {

        TranslateVeloc = _TranslateVeloc;
        TopPosY = _TopPosY;
        BottomPosY = _BottomPosY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //滑車を回す
        kassa.transform.Rotate(0, 0, 2);
        kassa2.transform.Rotate(0, 0, 2);
    }
    /// <summary>
    /// これは床を上下運動させるメソッド
    /// </summary>
    public void MoveFloorVertical()
    {
        if (setTranslateEnable)
            total_Veloc = deltaTime_Rigidbody * TranslateVeloc;
        else
            total_Veloc = deltaTime_Rigidbody * TranslateVeloc * -1;

        if (setTranslateEnable)
        {
            if (floor.transform.localPosition.x < TopPosY)
            {
                rb.MovePosition(new Vector3(translateInitPos.x,
            translateInitPos.y + total_Veloc,
            translateInitPos.z));
            }
            else
            {
                deltaTime_Rigidbody = 0;
                translateInitPos = floor.transform.position;
                setTranslateEnable = false;
            }
        }
        else
        {
            if (floor.transform.localPosition.x > BottomPosY)
            {
                rb.MovePosition(new Vector3(translateInitPos.x,
            translateInitPos.y + total_Veloc,
            translateInitPos.z));
            }
            else
            {
                translateInitPos = floor.transform.position;
                deltaTime_Rigidbody = 0;
                setTranslateEnable = true;
            }
        }

        deltaTime_Rigidbody += Time.deltaTime;
    }

    /// <summary>
    /// これは床を左右運動させるメソッド
    /// </summary>
    public void MoveFloorHorizontal()
    {
        if (setTranslateEnable)
            total_Veloc = deltaTime_Rigidbody * TranslateVeloc;
        else
            total_Veloc = deltaTime_Rigidbody * TranslateVeloc * -1;

        if (setTranslateEnable)
        {
            if (floor.transform.localPosition.x < TopPosY)
            {
                rb.MovePosition(new Vector3(translateInitPos.x + total_Veloc,
            translateInitPos.y,
            translateInitPos.z));
            }
            else
            {
                deltaTime_Rigidbody = 0;
                translateInitPos = floor.transform.position;
                setTranslateEnable = false;
            }
        }
        else
        {
            if (floor.transform.localPosition.x > BottomPosY)
            {
                rb.MovePosition(new Vector3(translateInitPos.x + total_Veloc,
            translateInitPos.y,
            translateInitPos.z));
            }
            else
            {
                translateInitPos = floor.transform.position;
                deltaTime_Rigidbody = 0;
                setTranslateEnable = true;
            }
        }

        deltaTime_Rigidbody += Time.deltaTime;
    }
}
