using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// 必要なコンポーネントの列記
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    #region フィールド
    public const float DistanceToGround = 0.2f;

    /// <summary>
    /// プロパティ
    /// </summary>
    public float animSpeed = 1.5f;
    public float Speed = 7.0f;
    public float jumpPower = 8.0f;

    public float g_VeclocityX;
    bool duringRun = false;

    bool canInput;
    bool canControlPlayer;

    public bool P_CanInput
    {
        get { return canInput; }
    }
    public bool P_CanControlPlayer
    {
        get { return canControlPlayer; }
        set { canControlPlayer = value; }
    }
    //方向変更用
    ChangeRotation changeRotation;

    // ジャンプ
    public bool g_duringJump = false;
    public bool isGround = false;
    int stayCount = 0;

    //変更できるコンポーネント
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 playerInitPos;
    private Vector3 clearPos;
    private Animator anim;

    bool playClearAnimation;

    public bool P_playClearAnimation
    {
        get { return playClearAnimation; }
    }
    Image nextStage;

    /// <summary>
    /// 空中ジャンプ
    /// </summary>
    public bool airJumpEnable = false;
    public const int MAX_AIR_JUMP_COUNT = 30;
    int airJumpEnableCount = MAX_AIR_JUMP_COUNT;

    /// <summary>
    /// 規定の初期
    /// </summary>
    public const int MAX_INIT_RECORD_COUNT = 50;
    public int initCount = MAX_INIT_RECORD_COUNT;

    bool onBed = false;
    public const int MAX_BED_COUNT = 20;
    int onBedCount = 0;

    #endregion

    #region 初期化
    // Use this for initialization
    void Start()
    {
        playerInitPos = this.gameObject.transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        changeRotation = new ChangeRotation();
        stayCount = 0;

        canControlPlayer = true;
        canInput = false;
        playClearAnimation = false;
        clearPos = GameObject.Find("ClearTrigger").transform.position;
        clearPos.y = clearPos.y - GameObject.Find("ClearTrigger").GetComponent<BoxCollider>().bounds.extents.y;

        nextStage = GameObject.Find("nextStage").GetComponent<Image>();
        nextStage.enabled = false;
    }
    #endregion

    public void ResetPlayer()
    {
        if (RecordPlayerPos.playStartEnable)
        {
            this.gameObject.transform.position = playerInitPos;
            this.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;


            this.gameObject.SendMessage("ResetLife");

            changeRotation.ResetRotation();
            rb.velocity = Vector3.zero;
            velocity = Vector3.zero;

            g_VeclocityX = 0;
            rb.isKinematic = false;
            canInput = false;


            initCount = MAX_INIT_RECORD_COUNT;
            airJumpEnableCount = MAX_AIR_JUMP_COUNT;

            onBed = false;
            onBedCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initCount > 0)
        {
            initCount--;
        }

        if (onBed)
        {
            onBedCount++;
            if (onBedCount > MAX_BED_COUNT)
            {
                onBedCount = 0;
                onBed = false;
            }
        }


        CheckisGrounded();
        UpdateAnimator();

        if (!DeadPerformanceScript.moveEnable)
        {
            UpdateInput();
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            g_VeclocityX = 0;
            g_duringJump = false;
        }
    }

    void FixedUpdate()
    {
        CheckPressAnyKey();
        Move();
        Jump();

        changeRotation.ChangeDirection(g_VeclocityX, this.gameObject);
    }

    #region clear時の演出
    bool RecieveInput()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (key == KeyCode.Mouse0 || key == KeyCode.Mouse1 || key == KeyCode.Mouse2 ||
                key == KeyCode.Mouse3 || key == KeyCode.Mouse4 || key == KeyCode.Mouse5 || key == KeyCode.Mouse6)
                    continue;
            if (Input.GetKeyDown(key))
                return true;
        }
        return false;
    }
    void ClearAnimation()
    {
        Animator doorAnimator = GameObject.Find("door").GetComponent<Animator>();
        doorAnimator.SetTrigger("OpenDoor");
        transform.localPosition += Vector3.forward * Time.fixedDeltaTime;
    }
    void GoNextStage()
    {
        //次のステージへ
        NextStage nextStage = GameObject.Find("ClearTrigger").GetComponent<NextStage>();
        nextStage.P_GoNextStage = true;
    }
    void SetPlayerToClearPos()
    {
        velocity = Vector3.zero;
        rb.isKinematic = true;
        nextStage.enabled = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void CheckPressAnyKey()
    {
        if (canControlPlayer)
            return;

        SetPlayerToClearPos();

        if (playClearAnimation)
        {
            ClearAnimation();
            GoNextStage();
            return;
        }
        else
            transform.position = clearPos;

        //入力したらクリアアニメ再生
        if (RecieveInput())
            playClearAnimation = true;
    }
    #endregion

    #region 移動に関連する
    void Move()
    {
        if (!canControlPlayer)
            return;
        velocity = new Vector3(g_VeclocityX, 0, 0);
        velocity *= Speed;

        if (!changeRotation.P_TurnOverEnable && canInput && stayCount < 3)
        {
            transform.position += velocity * Time.fixedDeltaTime;
        }

    }

    void Jump()
    {
        if (g_duringJump)
        {
            rb.AddForce(Vector3.up *
                jumpPower, ForceMode.VelocityChange);
            g_duringJump = false;
        }
        if (!g_duringJump)
        {
            rb.velocity += Physics.gravity * Time.deltaTime;
        }
        //if(!g_duringJump||!airJumpEnable)
        //{
        //    rb.velocity += Physics.gravity;
        //}

        //入れ替えの後の空中ジャンプ
        if (!airJumpEnable)
            rb.useGravity = true;
        else
            rb.useGravity = false;


    }
    #endregion

    #region input animator更新
    void UpdateInput()
    {
        if (!canControlPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
            || (int)Input.GetAxis("Horizontal") == 0)
            canInput = true;

        if (canInput)
            g_VeclocityX = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Jump"))
        {
            if (isGround
                && !airJumpEnable
                && onBedCount == 0
                && !onBed
                /*&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump")*/)
            {
                g_duringJump = true;
                anim.SetBool("Jump", true);
            }
        }
        

        if (g_VeclocityX > 0 || g_VeclocityX < 0)
            duringRun = true;
        else
            duringRun = false;
    }

    void UpdateAnimator()
    {
        anim.SetBool("Jump", false);
        anim.SetBool("Run", duringRun);
        anim.speed = animSpeed;
    }
    #endregion

    void CheckisGrounded()
    {
        RaycastHit hit;
        Ray[] ray = new Ray[20];
        for (int i = 0; i < 20; i++)
        {
            ray[i] = new Ray(transform.position - new Vector3(0.10f, 0, 0) + new Vector3(0.012f * i, 0, 0), Vector3.down);
        }
        int count = 0;
        for (int i = 0; i < 20; i++)
        {
            if (Physics.Raycast(ray[i], out hit, DistanceToGround))
            {

                if (hit.transform.tag != "Bed"&&hit.transform.name!="StartArea")
                {
                    isGround = true;
                    break;
                }
            }
            else
            {
                count++;
            }

        }
        if (count == 20)
        {
            isGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bed")
        {
            onBed = true;
        }
        if (collision.gameObject.tag == "MovingFloor")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor"||
            collision.gameObject.tag == "MovingFloor")
        {
            if (!isGround)
                stayCount++;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" ||
            collision.gameObject.tag == "MovingFloor")
        {
            stayCount = 0;
        }
        transform.parent = null;
    }
}
