using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchSe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayWatchSe();
    }

    void PlayWatchSe()
    {
        //0度と180度の時に再生（0.7は何なのかわからない）
        if(!SoundManager.IsPlayingGimmickSe() && 
            (transform.rotation.x < -0.7 || transform.rotation.x > 0.7))
        SoundManager.PlayGimmickSe("pendulumSe");
    }
}
