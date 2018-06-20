using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSe : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.PlayGimmickSe("pressSe", CalObjDis());
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.PlayGimmickSe("pressSe", CalObjDis());
    }
    private float CalObjDis()
    {
        player = GameObject.Find("Player");
        Vector2 dis = new Vector2(gameObject.transform.position.x - player.transform.position.x,
                            gameObject.transform.position.y - player.transform.position.y);

        float d = dis.magnitude;
        d = Mathf.Clamp(d, 0, 10) / 10 / 2;

        d = 0.5f - d;

        return d;
    }
}
