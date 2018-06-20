using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;

public class Menu : MonoBehaviour {
    enum MenuState { MenuState_SelectStage, MenuState_HowToPlay, MenuState_PV, MenuState_Exit };
    MenuState menuState;

    #region image
    Image selectStage;
    Image howToPlay;
    Image pv;
    Image exit;
    #endregion

    #region sprite
    [SerializeField]
    Sprite selectStageSprite;
    [SerializeField]
    Sprite howToPlaySprite;
    [SerializeField]
    Sprite pvSprite;
    [SerializeField]
    Sprite exitSprite;
    [SerializeField]
    Sprite selectStageClickSprite;
    [SerializeField]
    Sprite howToPlayClickSprite;
    [SerializeField]
    Sprite pvClickSprite;
    [SerializeField]
    Sprite exitClickSprite;
    #endregion

    float old;
    float now;

    // Use this for initialization
    void Start () {
        InitializeImage();

        menuState = MenuState.MenuState_SelectStage;
        old = 0;
        now = 0;
        Pauser.DestoryTarget();
    }

    void InitializeImage()
    {
        selectStage = GameObject.Find("selectStage").GetComponent<Image>();
        howToPlay = GameObject.Find("howToPlay").GetComponent<Image>();
        pv = GameObject.Find("pv").GetComponent<Image>();
        exit = GameObject.Find("exit").GetComponent<Image>();
    }

    private void Update()
    {
        UpdateInput();
        Select();
    }
    // Update is called once per frame
    void FixedUpdate () {

        DrawMenu();
    }

    #region draw func
    void DrawMenu()
    {
        switch (menuState)
        {
            case MenuState.MenuState_SelectStage:
                ShowSelectStage();
                break;
            case MenuState.MenuState_HowToPlay:
                ShowHowToPlay();
                break;
            case MenuState.MenuState_PV:
                ShowPv();
                break;
            case MenuState.MenuState_Exit:
                ShowExit();
                break;
        }
    }
    void ChangeSprite(Image img, Sprite sprite)
    {
        img.sprite = sprite;
    }

    void ShowSelectStage()
    {
        ChangeSprite(selectStage, selectStageClickSprite);
        ChangeSprite(howToPlay, howToPlaySprite);
        ChangeSprite(pv, pvSprite);
        ChangeSprite(exit, exitSprite);
    }
    void ShowHowToPlay()
    {
        ChangeSprite(selectStage, selectStageSprite);
        ChangeSprite(howToPlay, howToPlayClickSprite);
        ChangeSprite(pv, pvSprite);
        ChangeSprite(exit, exitSprite);
    }
    void ShowPv()
    {
        ChangeSprite(selectStage, selectStageSprite);
        ChangeSprite(howToPlay, howToPlaySprite);
        ChangeSprite(pv, pvClickSprite);
        ChangeSprite(exit, exitSprite);
    }
    void ShowExit()
    {
        ChangeSprite(selectStage, selectStageSprite);
        ChangeSprite(howToPlay, howToPlaySprite);
        ChangeSprite(pv, pvSprite);
        ChangeSprite(exit, exitClickSprite);
    }
    #endregion

    void UpdateInput()
    {
        now = Input.GetAxis("Vertical");

        if (old == 0 && now != 0)
        {
            SoundManager.PlayerPlayerSe("cursorSe");
            if (Input.GetAxis("Vertical") < 0)
                menuState++;
            if (Input.GetAxis("Vertical") > 0)
                menuState--;
        }

        old = Input.GetAxis("Vertical");

        if (menuState > MenuState.MenuState_Exit)
            menuState = MenuState.MenuState_SelectStage;
        if (menuState < MenuState.MenuState_SelectStage)
            menuState = MenuState.MenuState_Exit;
    }

    void Select()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton0)||Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlayerPlayerSe("decisionSe");
            switch (menuState)
            {
                case MenuState.MenuState_SelectStage:
                    SceneManager.LoadScene("StageSelect");
                    break;
                case MenuState.MenuState_HowToPlay:
                    //SceneManager.LoadScene("HowToPlay");
                    break;
                case MenuState.MenuState_PV:
                    //SceneManager.LoadScene("Pv");
                    break;
                case MenuState.MenuState_Exit:
                   // EditorApplication.isPlaying = false;
                    Application.Quit();
                    break;
            }
        }
    }
}
