using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaSe : MonoBehaviour {
    InfiniteFallingStoneScript fallenStone;
    // Use this for initialization
    void Start () {
        fallenStone = gameObject.GetComponent<InfiniteFallingStoneScript>();
    }
	
	// Update is called once per frame
	void Update () {
        PlayIwaSe();
    }

    void PlayIwaSe()
    {
        if (transform.position == fallenStone.P_stoneInitPos)
            SoundManager.PlayGimmickSe("iwaSe");
    }
}
