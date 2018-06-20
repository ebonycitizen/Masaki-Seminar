using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour {
    static List<Pauser> targets = new List<Pauser>();

    // ポーズ対象のコンポーネント
    Behaviour[] pauseBehavs = null;

    Rigidbody[] rgBodies = null;
    Vector3[] rgBodyVels = null;
    Vector3[] rgBodyAVels = null;

    // Use this for initialization
    void Start () {
        targets.Add(this);
    }

    #region ポーズされたとき
    void OnPause()
    {
        if (pauseBehavs != null)
            return;
        
        pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled; });
        foreach (var com in pauseBehavs)
        {
            com.enabled = false;
        }

        rgBodies = Array.FindAll(GetComponentsInChildren<Rigidbody>(), (obj) => { return !obj.IsSleeping(); });
        rgBodyVels = new Vector3[rgBodies.Length];
        rgBodyAVels = new Vector3[rgBodies.Length];
        for (var i = 0; i < rgBodies.Length; ++i)
        {
            rgBodyVels[i] = rgBodies[i].velocity;
            rgBodyAVels[i] = rgBodies[i].angularVelocity;
            rgBodies[i].Sleep();
        }
    }
    #endregion

    #region ポーズ解除されたとき
    void OnResume()
    {
        if (pauseBehavs == null)
            return;
       
        foreach (var com in pauseBehavs)
        {
            com.enabled = true;
        }

        for (var i = 0; i < rgBodies.Length; ++i)
        {
            rgBodies[i].WakeUp();
            rgBodies[i].velocity = rgBodyVels[i];
            rgBodies[i].angularVelocity = rgBodyAVels[i];
        }

        pauseBehavs = null;

        rgBodies = null;
        rgBodyVels = null;
        rgBodyAVels = null;
    }
    #endregion

    public static void Pause()
    {
        foreach (var obj in targets)
            obj.OnPause();
        if (!SceneManager.GetSceneByName("Pause").isLoaded)
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        SoundManager.PlayerPlayerSe("pauseSe");
        SoundManager.PauseBgm();
        SoundManager.PauseGimmickSe();
    }

    public static void Resume()
    {
        foreach (var obj in targets)
            obj.OnResume();
        if (SceneManager.GetSceneByName("Pause").isLoaded)
            SceneManager.UnloadSceneAsync("Pause");
        SoundManager.UnPauseBgm();
        SoundManager.UnPauseGimmickSe();
    }

    public static void DestoryTarget(){
        targets.Clear();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "OutArea")
        {
            GetComponent<Pauser>().enabled = false;
        }
    }
}
