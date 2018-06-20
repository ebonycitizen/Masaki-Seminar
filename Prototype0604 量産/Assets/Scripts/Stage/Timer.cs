using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    PlayerController playerController;
    PlayerLifeControl lifeControl;
    Text timerText;

    float timer;


	// Use this for initialization
	void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        lifeControl = GameObject.Find("Player").GetComponent<PlayerLifeControl>();
        timerText = GameObject.Find("Timer").GetComponent<Text>();

        timer = 0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateTimer();
        ChangeTimerText();
        if (lifeControl.lifeCount == 0)
            ResetTimer();
    }

    void UpdateTimer()
    {
        if(playerController.P_CanInput && playerController.P_CanControlPlayer)
        {
            timer += Time.fixedDeltaTime;
        }
    }

    void ChangeTimerText()
    {
        int s = (int)(timer % 1 * 10 % 10);
        timerText.text = (((int)timer).ToString() + ":" + (s).ToString());
    }

    void ResetTimer()
    {
        timer = 0;
    }
}
