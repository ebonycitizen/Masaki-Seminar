using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSe : MonoBehaviour {
    PlayerController playerController;
	// Use this for initialization
	void Start () {
        playerController = gameObject.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        ChangePlayerSe();
    }

    void ChangePlayerSe()
    {
        if (playerController.g_duringJump)
        {
            SoundManager.StopPlayerSe();
            SoundManager.PlayerPlayerSe("jumpSe");
        }
        else if (playerController.isGround && playerController.P_CanControlPlayer && playerController.g_VeclocityX != 0 && !SoundManager.IsPlayingPlayerSe())
        {
            SoundManager.PlayerPlayerSe("walkSe");
        }
    }
}
