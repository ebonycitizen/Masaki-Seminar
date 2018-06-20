using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    #region フィールド
    enum PauseState { PauseState_ReturnGame, PauseState_Control, PauseState_ReturnMenu };
    enum ReturnState { ReturnState_Yes, ReturnState_No };

    PauseState pauseState;
    ReturnState returnState;

    #region image
    Image returnGame;
    Image control;
    Image returnMenu;

    Image returnMenuBg;
    Image returnYes;
    Image returnNo;
    #endregion

    #region sprite
    [SerializeField]
    Sprite returnGameNonClick;
    [SerializeField]
    Sprite returnGameClick;
    [SerializeField]
    Sprite controlNonClick;
    [SerializeField]
    Sprite controlClick;
    [SerializeField]
    Sprite returnMenuNonClick;
    [SerializeField]
    Sprite returnMenuClick;

    [SerializeField]
    Sprite yesNonClick;
    [SerializeField]
    Sprite yesClick;
    [SerializeField]
    Sprite noNonClick;
    [SerializeField]
    Sprite noClick;
    #endregion

    float old;
    float now;

    bool isReturnMenuState;
    #endregion

    // Use this for initialization
    void Start()
    {
        InitializeImage();
        InitializeState();
        old = 0;
        now = 0;
        isReturnMenuState = false;
    }

    #region 初期化
    void InitializeImage()
    {
        returnGame = GameObject.Find("returnGame").GetComponent<Image>();
        control = GameObject.Find("control").GetComponent<Image>();
        returnMenu = GameObject.Find("returnMenu").GetComponent<Image>();
        returnMenuBg = GameObject.Find("returnMenuBg").GetComponent<Image>();
        returnYes = GameObject.Find("returnMenuYes").GetComponent<Image>();
        returnNo = GameObject.Find("returnMenuNo").GetComponent<Image>();
    }
    void InitializeState()
    {
        pauseState = PauseState.PauseState_ReturnGame;
        returnState = ReturnState.ReturnState_Yes;
    }
    #endregion
    private void Update()
    {
        if (!isReturnMenuState)
        {
            UpdateSelectPause();
            UpdateButtonPause();
        }
        else
        {
            UpdateSelectMenu();
            UpdateButtonReturnMenu();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isReturnMenuState)
        {
            ShowPauseState();
            SetPauseSpriteEnable();
        }
        else
        {
            ShowReturnState();
            SetReturnMenuSpriteEnable();
        }
    }
    #region 描画
    void ShowPauseState()
    {
        switch (pauseState)
        {
            case PauseState.PauseState_ReturnGame:
                ShowReturnGame();
                break;
            case PauseState.PauseState_Control:
                ShowControl();
                break;
            case PauseState.PauseState_ReturnMenu:
                ShowReturnMenu();
                break;
        }
    }
    void ShowReturnState()
    {
        switch (returnState)
        {
            case ReturnState.ReturnState_Yes:
                ShowReturnYes();
                break;
            case ReturnState.ReturnState_No:
                ShowReturnNo();
                break;
        }
    }

    void SetPauseSpriteEnable()
    {
        returnGame.enabled = true;
        control.enabled = true;
        returnMenu.enabled = true;
        returnMenuBg.enabled = false;
        returnYes.enabled = false;
        returnNo.enabled = false;
    }
    void SetReturnMenuSpriteEnable()
    {
        returnGame.enabled = false;
        control.enabled = false;
        returnMenu.enabled = false;
        returnMenuBg.enabled = true;
        returnYes.enabled = true;
        returnNo.enabled = true;
    }

    void ChangeSprite(Image img, Sprite sprite)
    {
        img.sprite = sprite;
    }

    void ShowReturnGame()
    {
        ChangeSprite(returnGame, returnGameClick);
        ChangeSprite(control, controlNonClick);
        ChangeSprite(returnMenu, returnMenuNonClick);
    }
    void ShowControl()
    {
        ChangeSprite(returnGame, returnGameNonClick);
        ChangeSprite(control, controlClick);
        ChangeSprite(returnMenu, returnMenuNonClick);
    }
    void ShowReturnMenu()
    {
        ChangeSprite(returnGame, returnGameNonClick);
        ChangeSprite(control, controlNonClick);
        ChangeSprite(returnMenu, returnMenuClick);
    }
    void ShowReturnYes()
    {
        ChangeSprite(returnYes, yesClick);
        ChangeSprite(returnNo, noNonClick);
    }
    void ShowReturnNo()
    {
        ChangeSprite(returnYes, yesNonClick);
        ChangeSprite(returnNo, noClick);
    }
    #endregion

    #region 入力
    void UpdateButtonPause()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (pauseState == PauseState.PauseState_ReturnGame)
                SoundManager.PlayerPlayerSe("cancelSe");
            else
                SoundManager.PlayerPlayerSe("decisionSe");
            //isReturnMenuStateによって変わる break pointの
            switch (pauseState)
            {
                case PauseState.PauseState_ReturnGame:
                    Pauser.Resume();
                    return;
                case PauseState.PauseState_Control:
                    //GOTO:scene.control
                    return;
                case PauseState.PauseState_ReturnMenu:
                    isReturnMenuState = true;
                    return;
            }

            
        }
    }
    void UpdateButtonReturnMenu()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (returnState == ReturnState.ReturnState_No)
                SoundManager.PlayerPlayerSe("cancelSe");
            else
                SoundManager.PlayerPlayerSe("decisionSe");
            switch (returnState)
            {
                case ReturnState.ReturnState_Yes:
                    SceneManager.LoadScene("Menu");
                    return;
                case ReturnState.ReturnState_No:
                    isReturnMenuState = false;
                    return;
            }
        }
    }

    void UpdateSelectPause()
    {
        now = Input.GetAxis("Vertical");

        if (old == 0 && now != 0)
        {
            SoundManager.PlayerPlayerSe("cursorSe");
            if (Input.GetAxis("Vertical") < 0)
                pauseState++;
            if (Input.GetAxis("Vertical") > 0)
                pauseState--;
        }

        old = Input.GetAxis("Vertical");

        if (pauseState > PauseState.PauseState_ReturnMenu)
            pauseState = PauseState.PauseState_ReturnGame;
        if (pauseState < PauseState.PauseState_ReturnGame)
            pauseState = PauseState.PauseState_ReturnMenu;
    }
    void UpdateSelectMenu()
    {
        now = Input.GetAxis("Horizontal");

        if (old == 0 && now != 0)
        {
            SoundManager.PlayerPlayerSe("cursorSe");
            if (Input.GetAxis("Horizontal") > 0)
                returnState++;
            if (Input.GetAxis("Horizontal") < 0)
                returnState--;
        }

        old = Input.GetAxis("Horizontal");

        if (returnState > ReturnState.ReturnState_No)
            returnState = ReturnState.ReturnState_Yes;
        if (returnState < ReturnState.ReturnState_Yes)
            returnState = ReturnState.ReturnState_No;
    }
    #endregion
}
