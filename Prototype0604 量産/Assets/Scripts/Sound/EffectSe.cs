using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSe : MonoBehaviour {
    PlayerController playerController;
    PlayerLifeControl playerLifeControl;
    bool oldCanControlPlayer;
    bool oldPlayClearAnimation;
    int oldLifeCount;
    // Use this for initialization

    void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerLifeControl = GameObject.Find("Player").GetComponent<PlayerLifeControl>();
        oldCanControlPlayer = true;
        oldPlayClearAnimation = false;
        oldLifeCount = 1;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        PlayClearSe();
        PlayDoorSe();
        PlayGhostRespwanSe();
    }

    void PlayClearSe()
    {
        if (oldCanControlPlayer != playerController.P_CanControlPlayer)
        {
            SoundManager.StopPlayerSe();
            SoundManager.PlayerPlayerSe("clearSe");
        }
        oldCanControlPlayer = playerController.P_CanControlPlayer;
    }

    void PlayDoorSe()
    {
        if(oldPlayClearAnimation != playerController.P_playClearAnimation)
        {
            SoundManager.PlayerPlayerSe("doorSe");
        }
        oldPlayClearAnimation = playerController.P_playClearAnimation;
    }

    void PlayGhostRespwanSe()
    {
        if (oldLifeCount == 1 && playerLifeControl.lifeCount == 0)
            SoundManager.PlayerPlayerSe("ghostSe");

        oldLifeCount = playerLifeControl.lifeCount;
    }
}
