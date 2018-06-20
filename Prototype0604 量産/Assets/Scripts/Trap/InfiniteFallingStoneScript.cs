using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// これは罠：無限に落ちる岩をコントロールするクラス
/// 作成者:huyup
/// </summary>
public class InfiniteFallingStoneScript : MonoBehaviour
{
    float FallenVeloc { get; set; }
    Vector3 stoneInitPos;

    public Vector3 P_stoneInitPos
    {
        get { return stoneInitPos; }
    }

    // Use this for initialization
    void Start()
    {
        stoneInitPos = transform.position;
    }

    public void ResetInfiniteFallingStone()
    {
        transform.position = stoneInitPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "OutArea")
        {
            transform.position = stoneInitPos;
        }
    }
}
