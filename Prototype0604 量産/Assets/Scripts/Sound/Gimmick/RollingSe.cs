using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSe : MonoBehaviour {
    GameObject player;

    private float vecY;
    private float vecX;

    private State state;
    enum State
    {
        Fall,
        Roll,
        Stop
    }

    // Use this for initialization
    void Start () {
        state = State.Stop;
        vecY = transform.position.y;
        vecX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        CheckVec();
	}

    private void CheckVec()
    {
        if (state == State.Stop && vecY != transform.position.y)
        {
            SoundManager.PlayGimmickSe("iwaSe", CalObjDis());
            state = State.Fall;
        }

        if(state == State.Roll)
        {
            if (!SoundManager.IsPlayingGimmickSe())
                SoundManager.PlayGimmickSe("rollSe", CalObjDis());
        }
        
        vecY = transform.position.y;
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (state == State.Stop)
            return;

        if (vecX != transform.position.x)
        {
            state = State.Roll;
        }
        else
            state = State.Stop;

        vecX = transform.position.x;

        //if (!SoundManager.IsPlayingGimmickSe())
        //    SoundManager.PlayGimmickSe("rollSe", CalObjDis());
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
