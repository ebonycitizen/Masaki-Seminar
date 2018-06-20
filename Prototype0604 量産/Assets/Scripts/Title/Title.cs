using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// タイトル画面の入力や演出
/// </summary>
public class Title : MonoBehaviour {
    float increaseRgbTpu;
    float increaseRgbBg;
    float increaseRgbLogo;
    float increaseAlphaBtn;
    float randY;
    float nowCountTime;

    public float p_IncreaseRgbBg
    {
        get { return increaseRgbBg; }
    }

    bool isShown;

    const float fadeInterval = 2f;
    const float colorInterval = 0.01f;

    Image tpu;
    Image bg;
    Image logo;
    Image pressBtn;

    [SerializeField]
    Sprite pressAnyClickSprite;

	// Use this for initialization
	void Start () {
        tpu = GameObject.Find("tpu").GetComponent<Image>();
        bg = GameObject.Find("bg").GetComponent<Image>();
        logo = GameObject.Find("logo").GetComponent<Image>();
        pressBtn = GameObject.Find("pressAnyBtn").GetComponent<Image>();

        increaseRgbTpu = 0;
        increaseRgbBg = 0;
        increaseRgbLogo = 0;
        increaseAlphaBtn = 0;
        randY = 0;
        nowCountTime = 0;

        isShown = false;
    }
    // Update is called once per frame
    void FixedUpdate () {
        nowCountTime += Time.fixedDeltaTime;

        if (increaseRgbLogo >= 1 && RecieveInput())
        {
            pressBtn.sprite = pressAnyClickSprite;
            pressBtn.color = new Color(1, 1, 1, 1);

            SoundManager.PlayerPlayerSe("decisionSe");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("Title");
            return;
        }

        ShowTpu();
        ShowBg();
        Showlogo();
        ShowPressAnyBtn();

    }

    #region draw func
    void ShowTpu()
    {
        if (!tpu.gameObject.activeSelf)
            return;

        if (RecieveInput())
        {
            tpu.gameObject.SetActive(false);
            nowCountTime = 0;
        }

        if (nowCountTime < fadeInterval / 2)
            tpu.color = Color.black;
        
        if(nowCountTime >= fadeInterval && nowCountTime < fadeInterval*2)
        {
            if (increaseRgbTpu != 1)
                increaseRgbTpu += colorInterval;
            else
                increaseRgbTpu = 1f;
        }

        if (nowCountTime >= fadeInterval * 2)
        {
            if (increaseRgbTpu > 0)
                increaseRgbTpu -= colorInterval;
            else
                increaseRgbTpu = 0f;
        }

        if (increaseRgbTpu == 0 && nowCountTime >= fadeInterval * 2)
        {
            tpu.gameObject.SetActive(false);
            nowCountTime = 0;
            return;
        }

        tpu.color = new Color(increaseRgbTpu, increaseRgbTpu, increaseRgbTpu);
    }

    void ShowBg()
    {
        if (tpu.gameObject.activeSelf)
            return;

        if (RecieveInput() && !tpu.gameObject.activeSelf && nowCountTime > fadeInterval)
        {
            increaseRgbBg = 1f;
            increaseRgbLogo = 1f;
        }

        if (nowCountTime < fadeInterval)
            bg.color = Color.black;

        if (nowCountTime >= fadeInterval)
        {
            if (increaseRgbBg != 1)
                increaseRgbBg += colorInterval;
            else
                increaseRgbBg = 1f;
        }
        bg.color = new Color(increaseRgbBg, increaseRgbBg, increaseRgbBg);
    }

    void Showlogo()
    {
        if (logo == null || increaseRgbBg < 1)
            return;

        if (increaseRgbLogo != 1)
            increaseRgbLogo += colorInterval;
        else
            increaseRgbLogo = 1f;

        logo.color = new Color(increaseRgbLogo, increaseRgbLogo, increaseRgbLogo, 1);
    }

    void ShowPressAnyBtn()
    {
        if (increaseRgbLogo < 1 || pressBtn.sprite == pressAnyClickSprite)
            return;

        if (!isShown)
        {
            if(increaseAlphaBtn < 1)
                increaseAlphaBtn += colorInterval;
            else
            {
                increaseAlphaBtn = 1f;
                isShown = true;
            }
        } 

        if (isShown)
        {
             if(increaseAlphaBtn > 0)
                increaseAlphaBtn -= colorInterval;
            else
            {
                increaseAlphaBtn = 0f;
                isShown = false;
            }
        }

        pressBtn.color = new Color(1, 1, 1, increaseAlphaBtn);
    }
    #endregion

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
}
