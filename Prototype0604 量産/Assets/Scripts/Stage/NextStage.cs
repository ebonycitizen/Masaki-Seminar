using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    int nextStageNum = 0;
    bool goNextStage;

    public bool P_GoNextStage
    {
        get { return goNextStage; }
        set { goNextStage = value; }
    }

    private void Start()
    {
        int.TryParse(SceneManager.GetActiveScene().name.Remove(0,5), out nextStageNum);
        nextStageNum++;
        goNextStage = false;
    }
    private void Update()
    {
        if(goNextStage)
        {
            Invoke("LoadNextScene", 1f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            if(SceneManager.GetActiveScene().name == "Stage5")
            {
                GameObject trap = GameObject.Find("Trap");
                Destroy(trap);
            }
        }
    }

    void LoadNextScene()
    {
        Pauser.DestoryTarget();
        SceneManager.LoadScene("Stage" + nextStageNum.ToString(), LoadSceneMode.Single);
    }
}
