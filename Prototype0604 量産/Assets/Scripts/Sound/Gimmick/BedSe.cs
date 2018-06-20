using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSe : MonoBehaviour {
    PlayerController playerController;
	// Use this for initialization
	void Start () {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(!playerController.isGround)
            SoundManager.PlayGimmickSe("bedSe");
    }
}
