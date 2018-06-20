using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {
    const int stageNumMax = 10;
    public enum StageNum { Stage1 = 1, Stage2, Stage3, Stage4, Stage5, Stage6, Stage7, Stage8, Stage9, Stage10 };
    static StageNum stageNum;

    #region image
    Image stage1;
    Image stage2;

    Image difficulty1;
    Image difficulty2;
    Image difficulty3;
    //Image difficulty1;

    Image clear;
    #endregion

    #region sprite
    [SerializeField]
    Sprite zeroSprite;
    [SerializeField]
    Sprite oneSprite;
    [SerializeField]
    Sprite twoSprite;
    [SerializeField]
    Sprite threeSprite;
    [SerializeField]
    Sprite fourSprite;
    [SerializeField]
    Sprite fiveSprite;
    [SerializeField]
    Sprite sixSprite;
    [SerializeField]
    Sprite sevenSprite;
    [SerializeField]
    Sprite eightSprite;
    [SerializeField]
    Sprite nineSprite;
    [SerializeField]
    Sprite grayStarSprite;
    [SerializeField]
    Sprite yellowStarSprite;
    [SerializeField]
    Sprite clearSprite;
    #endregion

    float oldh;
    float nowh;

    #region initialize
    // Use this for initialization
    void Start()
    {
        InitializeImage();

        stageNum = StageNum.Stage1;
        oldh = 0;
        nowh = 0;
        Pauser.DestoryTarget();
        if (!SoundManager.IsPlayingBgm())
            SoundManager.PlayBGM("titleBgm");
    }

    void InitializeImage()
    {
        stage1 = GameObject.Find("stage#1").GetComponent<Image>();
        stage2 = GameObject.Find("stage#2").GetComponent<Image>();

        difficulty1 = GameObject.Find("Star(Gray) (0)").GetComponent<Image>();
        difficulty2 = GameObject.Find("Star(Gray) (1)").GetComponent<Image>();
        difficulty3 = GameObject.Find("Star(Gray) (2)").GetComponent<Image>();

        clear = GameObject.Find("Clear").GetComponent<Image>();

        clear.enabled = false;
    }
    #endregion

    public static StageNum getStageNum()
    {
        return stageNum; 
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateInput();

    }
    void FixedUpdate()
    {
        ShowStage2ImgObj();
        ShowDifficulty();
        ShowClear();
        ShowStage();
    }

    #region draw func
    void ShowStage2ImgObj()
    {
        if (stageNum > StageNum.Stage9)
            stage2.enabled = true;
        else
            stage2.enabled = false;
    }
    void ShowClear()
    {
        if(PlayerPrefs.HasKey(stageNum.ToString()))
            clear.enabled = true;
        else
            clear.enabled = false;
    }
    void ChangeSprite(Image img, Sprite sprite)
    {
        img.sprite = sprite;
    }

    #region stages
    void ShowStage1()
    {
        ChangeSprite(stage1, oneSprite);
    }
    void ShowStage2()
    {
        ChangeSprite(stage1, twoSprite);
    }
    void ShowStage3()
    {
        ChangeSprite(stage1, threeSprite);
    }
    void ShowStage4()
    {
        ChangeSprite(stage1, fourSprite);
    }
    void ShowStage5()
    {
        ChangeSprite(stage1, fiveSprite);
    }
    void ShowStage6()
    {
        ChangeSprite(stage1, sixSprite);
    }
    void ShowStage7()
    {
        ChangeSprite(stage1, sevenSprite);
    }
    void ShowStage8()
    {
        ChangeSprite(stage1, eightSprite);
    }
    void ShowStage9()
    {
        ChangeSprite(stage1, nineSprite);
    }
    void ShowStage10()
    {
        ChangeSprite(stage1, oneSprite);
        ChangeSprite(stage2, zeroSprite);
    }
    #endregion
    void ShowStage()
    {
        switch (stageNum)
        {
            case StageNum.Stage1:
                ShowStage1();
                break;
            case StageNum.Stage2:
                ShowStage2();
                break;
            case StageNum.Stage3:
                ShowStage3();
                break;
            case StageNum.Stage4:
                ShowStage4();
                break;
            case StageNum.Stage5:
                ShowStage5();
                break;
            case StageNum.Stage6:
                ShowStage6();
                break;
            case StageNum.Stage7:
                ShowStage7();
                break;
            case StageNum.Stage8:
                ShowStage8();
                break;
            case StageNum.Stage9:
                ShowStage9();
                break;
            case StageNum.Stage10:
                ShowStage10();
                break;
        }
    }

    #region difficulty
    void ShowDifficulty1()
    {
        ChangeSprite(difficulty1, yellowStarSprite);
        ChangeSprite(difficulty2, grayStarSprite);
        ChangeSprite(difficulty3, grayStarSprite);
    }
    void ShowDifficulty2()
    {
        ChangeSprite(difficulty1, yellowStarSprite);
        ChangeSprite(difficulty2, yellowStarSprite);
        ChangeSprite(difficulty3, grayStarSprite);
    }
    void ShowDifficulty3()
    {
        ChangeSprite(difficulty1, yellowStarSprite);
        ChangeSprite(difficulty2, yellowStarSprite);
        ChangeSprite(difficulty3, yellowStarSprite);
    }
    #endregion
    void ShowDifficulty()
    {
        switch ((int)stageNum/(stageNumMax/3))
        {
            case 0:
            case 1:
                ShowDifficulty1();
                break;
            case 2:
                ShowDifficulty2();
                break;
            case 3:
                ShowDifficulty3();
                break;
        }
    }
    #endregion

    #region input
    void UpdateInput()
    {
        nowh = Input.GetAxis("Horizontal");

        if (oldh == 0 && nowh != 0)
        {
            SoundManager.PlayerPlayerSe("cursorSe");
            if (Input.GetAxis("Horizontal") > 0)
                stageNum++;
            if (Input.GetAxis("Horizontal") < 0)
                stageNum--;

            if (stageNum > StageNum.Stage10)
                stageNum = StageNum.Stage1;
            if (stageNum < StageNum.Stage1)
                stageNum = StageNum.Stage10;
        }

        oldh = Input.GetAxis("Horizontal");
        UpdateButton();
    }

    void UpdateButton()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            SoundManager.PlayerPlayerSe("cancelSe");
            if (SoundManager.IsPlayingBgm())
                SoundManager.StopBGM();
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0)||Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlayerPlayerSe("decisionSe");
            if (SoundManager.IsPlayingBgm())
                SoundManager.StopBGM();
            SceneManager.LoadScene("Loading");
        }
    }
    #endregion
}
