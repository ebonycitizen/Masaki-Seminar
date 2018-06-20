using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// これは死亡時の演出をコントロールするクラス
/// 作成者:huyp
/// </summary>
public class DeadPerformanceScript : MonoBehaviour
{
    /// <summary>
    /// 参照
    /// </summary>
    GameObject startTarget;
    GameObject player;
    /// <summary>
    /// コンポーネント
    /// </summary>
    ParticleSystem soulEff;

    static public bool moveEnable = false;
    /// <summary>
    /// 円運動のためのプロパティ
    /// </summary>
    float Radius { set; get; }
    Vector3 centerPos { set; get; }

    public const float MoveCircleSpeed = 1.5f;
    float moveCircleSpeed = MoveCircleSpeed;

    float timeForCircle = 0;
    bool moveCircle = false;

    /// <summary>
    /// 正方形運動のためのプロパティ
    /// </summary>
    public const float MoveSquareSpeed = 0.1f;
    float moveSqureSpeed = MoveSquareSpeed;
    public bool moveSquare = false;

    bool raiseEnable = false;
    bool moveSoulToLeft = false;
    bool moveSoulToRight = false;

    /// <summary>
    /// ゴーストが拡大する演出
    /// </summary>
    const float InitScaleValue = 0f;
    Vector3 localScale = new Vector3(InitScaleValue, InitScaleValue, InitScaleValue);

    public const float ScaleVeloc = 0.02f;
    float scaleVeloc = ScaleVeloc;

    void Start()
    {
        //参照
        player = GameObject.Find("Player");
        startTarget = GameObject.Find("StartArea");

        //コンポーネント
        soulEff = transform.Find("Particle System").GetComponent<ParticleSystem>();

        //初期設定
        moveEnable = false;
        soulEff.Stop();
        GetComponent<Renderer>().enabled = false;
    }

    void FixedUpdate()
    {
        //円運動
        timeForCircle += Time.deltaTime;

        if (moveEnable && moveCircle)
            MoveAsCircle();

        //正方形運動
        if (moveEnable && moveSquare)
            MoveAsSquare();

        //Scale
        ScaleFunction();

        //演出中は、モデルを隠す
        if (moveEnable)
        {
            Transform body = player.transform.Find("character_motion2").Find("root");
            foreach (Transform child in body)
            {
                if (child.GetComponent<Renderer>())
                    child.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    public void SetCircleParameter(Vector3 deathPos)
    {
        //スケール
        localScale = new Vector3(InitScaleValue, InitScaleValue, InitScaleValue);

        transform.localScale = localScale;

        //円
        centerPos = new Vector3((deathPos.x + startTarget.transform.position.x) / 2,
    (deathPos.y + startTarget.transform.position.y) / 2 + 0.1f, transform.position.z);

        Radius = Mathf.Abs((deathPos.x - startTarget.transform.position.x) / 2);

        moveCircleSpeed = MoveCircleSpeed;

        timeForCircle = 0;

        moveCircle = true;
        moveEnable = true;
        //モデルを出現させる
        if (!soulEff.isPlaying)
            soulEff.Play();

        GetComponent<Renderer>().enabled = true;
    }

    public void SetSquareParameter(Vector3 deathPos)
    {
        //スケール
        localScale = new Vector3(InitScaleValue, InitScaleValue, InitScaleValue);
        transform.localScale = localScale;

        //正方形
        moveSquare = true;
        raiseEnable = true;
        moveEnable = true;

        transform.position = new Vector3(deathPos.x,deathPos.y,transform.position.z);

        if (deathPos.x > startTarget.transform.position.x)
            moveSoulToLeft = true;
        if (deathPos.x < startTarget.transform.position.x)
            moveSoulToRight = true;

        //モデルを出現させる
        if (!soulEff.isPlaying)
            soulEff.Play();

        GetComponent<Renderer>().enabled = true;

    }
    public void MoveAsCircle()
    {

        // 円運動の座標演算
        float x = Mathf.Sin(-timeForCircle * moveCircleSpeed + (Mathf.PI / 2)) * Radius + centerPos.x;
        float y = Mathf.Cos(-timeForCircle * moveCircleSpeed + (Mathf.PI / 2)) * Radius + centerPos.y;
        float z = centerPos.z;

        // オブジェクトに座標を代入
        transform.position = new Vector3(x, y, z);
    }
    public void ScaleFunction()
    {
        if(transform.localScale.x<1)
        {
            transform.localScale += new Vector3(scaleVeloc, scaleVeloc, scaleVeloc);
        }
    }
    public void MoveAsSquare()
    {
        if (raiseEnable)
        {
            // 正方形運動演算
            if (transform.position.y < startTarget.transform.position.y)
            {
                transform.position += new Vector3(0, moveSqureSpeed, 0);
            }
            else
            {
                raiseEnable = false;
            }
        }
        else
        {
            if (moveSoulToLeft)
            {
                if (transform.position.x > startTarget.transform.position.x)
                    transform.position -= new Vector3(moveSqureSpeed, 0, 0);
            }
            if (moveSoulToRight)
            {
                if (transform.position.x < startTarget.transform.position.x)
                    transform.position += new Vector3(moveSqureSpeed, 0, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StartArea")
        {
            if (!soulEff.isStopped)
                soulEff.Stop();

            GetComponent<Renderer>().enabled = false;
            moveEnable = false;
            moveCircle = false;
            moveSquare = false;

            moveSoulToLeft = false;
            moveSoulToRight = false;

            Transform body = player.transform.Find("character_motion2").Find("root");
            foreach (Transform child in body)
            {
                if (child.GetComponent<Renderer>())
                    child.GetComponent<Renderer>().enabled = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "StartArea")
        {
            if (!soulEff.isStopped)
                soulEff.Stop();

            GetComponent<Renderer>().enabled = false;
            moveEnable = false;
            moveCircle = false;
            moveSquare = false;

            moveSoulToLeft = false;
            moveSoulToRight = false;

            Transform body = player.transform.Find("character_motion2").Find("root");
            foreach (Transform child in body)
            {
                if (child.GetComponent<Renderer>())
                    child.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
