using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// これは罠：落石をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class MshinItoScript : MonoBehaviour
{
    int hitFloorCount;

    bool addForceEnable;
    public float FallenVeloc { get; set; }

    float HitFloor_AddForce { get; set; }

    Vector3 mshinIto_InitPos;
    Quaternion mshinIton_InitRotation;

    // Use this for initialization
    void Start()
    {
        mshinIto_InitPos = transform.position;
        mshinIton_InitRotation = transform.rotation;
    }
    public void InitializeParameter(float _FallenVeloc,float _HitFloor_AddForce)
    {
        FallenVeloc = _FallenVeloc;

        HitFloor_AddForce = _HitFloor_AddForce;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void ResetFallenStone()
    {
        hitFloorCount = 0;
        transform.rotation = mshinIton_InitRotation;
        addForceEnable = false;
        GetComponent<Rigidbody>().isKinematic = true ;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = mshinIto_InitPos;
    }

    /// <summary>
    /// これはミシン糸を落下させるメソッド
    /// 床に落ちたら、力を与える
    /// </summary>
    public void SetFallenStoneRigid()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().AddForce(0, FallenVeloc, 0);
    }
    public void SetFallenStone()
    {
        //GetComponent<Rigidbody>().isKinematic = false;
        //transform.GetComponent<Rigidbody>().AddForce(0, FallenVeloc, 0);

        if (hitFloorCount == 1 && addForceEnable)
        {
            transform.GetComponent<Rigidbody>().AddForce(HitFloor_AddForce, 0, 0, ForceMode.VelocityChange);
            addForceEnable = false;
        }
        if (hitFloorCount == 2 && addForceEnable)
        {
            transform.GetComponent<Rigidbody>().AddForce(HitFloor_AddForce+5, 0, 0, ForceMode.VelocityChange);
            addForceEnable = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            addForceEnable = true;
            hitFloorCount++;
        }
    }
}
