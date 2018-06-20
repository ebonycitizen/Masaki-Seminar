using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRolling : MonoBehaviour {
    const int mshinIto_Count = 3;
    GameObject[] mshinIto = new GameObject[mshinIto_Count];
    float FallenVeloc;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < mshinIto_Count; i++)
        {
            mshinIto[i] = GameObject.Find("ito" + (i + 1));
        }
        FallenVeloc = mshinIto[0].GetComponent<MshinItoScript>().FallenVeloc;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            for (int i = 0; i < mshinIto_Count; i++)
            {
                mshinIto[i].GetComponent<Rigidbody>().isKinematic = false;
                mshinIto[i].transform.GetComponent<Rigidbody>().AddForce(0, FallenVeloc, 0);
            }
            
        }
    }
}
