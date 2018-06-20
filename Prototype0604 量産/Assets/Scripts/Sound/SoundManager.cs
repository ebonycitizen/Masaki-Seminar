using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {
    #region field
    class AudioClipInfo
    {
        public string key;
        public string resName;
        public AudioClip audioClip;

        public AudioClipInfo(string key, string resName)
        {
            this.key = key;
            this.resName = resName;
            audioClip = Resources.Load(resName) as AudioClip;
        }
    }

    // シングルトン
    static SoundManager singleton = null;
    // インスタンス取得
    public static SoundManager GetInstance()
    {
        return singleton ?? (singleton = new SoundManager());
    }

    enum soundType
    {
        BGM, SE,
    }

    GameObject soundPlayer;
    AudioSource audioSourceBgm;
    AudioSource audioSourcePlayerSe;
    AudioSource audioSourceGimmickSe;
    Dictionary<string, AudioClipInfo> poolBgm = new Dictionary<string, AudioClipInfo>();
    Dictionary<string, AudioClipInfo> poolPlayerSe = new Dictionary<string, AudioClipInfo>();
    Dictionary<string, AudioClipInfo> poolGimmickSe = new Dictionary<string, AudioClipInfo>();
    #endregion

    public SoundManager()
    {
        poolBgm.Add("titleBgm", new AudioClipInfo("titleBgm", "Sound/bgm/TitleBgm"));
        poolBgm.Add("stageBgm", new AudioClipInfo("stageBgm", "Sound/bgm/StageBgm"));

        poolPlayerSe.Add("jumpSe", new AudioClipInfo("jumpSe", "Sound/se/Character/jump3"));
        poolPlayerSe.Add("walkSe", new AudioClipInfo("walkSe", "Sound/se/Character/walk4"));

        poolPlayerSe.Add("clearSe", new AudioClipInfo("clearSe", "Sound/se/Effect/Clear2"));
        poolPlayerSe.Add("doorSe", new AudioClipInfo("doorSe", "Sound/se/Effect/Door2"));
        poolPlayerSe.Add("ghostSe", new AudioClipInfo("ghostSe", "Sound/se/Effect/GhostSpawn2"));

        poolPlayerSe.Add("decisionSe", new AudioClipInfo("decisionSe", "Sound/se/UI/decision"));
        poolPlayerSe.Add("cursorSe", new AudioClipInfo("cursorSe", "Sound/se/UI/cursor"));
        poolPlayerSe.Add("cancelSe", new AudioClipInfo("cancelSe", "Sound/se/UI/cancel"));
        poolPlayerSe.Add("pauseSe", new AudioClipInfo("pauseSe", "Sound/se/UI/pause"));

        poolGimmickSe.Add("bedSe", new AudioClipInfo("bedSe", "Sound/se/Gimmick/BedJump"));
        poolGimmickSe.Add("pendulumSe", new AudioClipInfo("pendulumSe", "Sound/se/Gimmick/Pendulum"));
        poolGimmickSe.Add("pressSe", new AudioClipInfo("pressSe", "Sound/se/Gimmick/PressingMachine"));
        poolGimmickSe.Add("rollSe", new AudioClipInfo("rollSe", "Sound/se/Gimmick/RollingRock"));
        poolGimmickSe.Add("iwaSe", new AudioClipInfo("iwaSe", "Sound/se/Gimmick/iwa"));
    }

    #region bgm
    public static bool PlayBGM(string bgmName)
    {
        return GetInstance()._PlayBgm(bgmName);
    }
    bool _PlayBgm(string bgmName)
    {
        if (!poolBgm.ContainsKey(bgmName))
            return false;

        AudioClipInfo info = poolBgm[bgmName];
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            GameObject.DontDestroyOnLoad(soundPlayer);
            audioSourceBgm = soundPlayer.AddComponent<AudioSource>();
        }

        audioSourceBgm.loop = true;
        audioSourceBgm.clip = info.audioClip;
        audioSourceBgm.Play();

        return true;
    }

    public static bool StopBGM()
    {
        return GetInstance()._StopBgm(); 
    }

    bool _StopBgm()
    {
        audioSourceBgm.Stop();
        return true;
    }
    public static bool IsPlayingBgm()
    {
        return GetInstance()._IsPlayingBgm();
    }

    bool _IsPlayingBgm()
    {
        if (audioSourceBgm != null)
            return audioSourceBgm.isPlaying;
        return false;
    }

    public static bool PauseBgm()
    {
        return GetInstance()._PauseBgm();
    }
    bool _PauseBgm()
    {
        audioSourceBgm.Pause();
        return true;
    }

    public static bool UnPauseBgm()
    {
        return GetInstance()._UnPauseBgm();
    }
    bool _UnPauseBgm()
    {
        audioSourceBgm.UnPause();
        return true;
    }
    #endregion

    #region player se
    public static bool PlayerPlayerSe(string seName)
    {
        return GetInstance()._PlayPlayerSe(seName);
    }
    bool _PlayPlayerSe(string seName)
    {
        if (!poolPlayerSe.ContainsKey(seName))
            return false;

        AudioClipInfo info = poolPlayerSe[seName];
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            GameObject.DontDestroyOnLoad(soundPlayer);

            audioSourcePlayerSe = soundPlayer.AddComponent<AudioSource>();
            audioSourcePlayerSe.clip = info.audioClip;
        }
        if (!HasExitSound(info))
        {
            audioSourcePlayerSe = soundPlayer.AddComponent<AudioSource>();
            audioSourcePlayerSe.clip = info.audioClip;
        }

        audioSourcePlayerSe.PlayOneShot(info.audioClip, 0.5f);

        return true;
    }
    private bool HasExitSound(AudioClipInfo info)
    {
        foreach (AudioSource source in soundPlayer.GetComponents<AudioSource>())
        {
            if (info.audioClip == source.clip)
                return true;
        }

        return false;
    }
    public static bool StopPlayerSe()
    {
        return GetInstance()._StopPlayerSe();
    }

    bool _StopPlayerSe()
    {
        if (audioSourcePlayerSe == null)
            return false;
        audioSourcePlayerSe.Stop();
        return true;
    }

    public static bool IsPlayingPlayerSe()
    {
        return GetInstance()._IsPlayingPlayerSe();
    }

    bool _IsPlayingPlayerSe()
    {
        if (audioSourcePlayerSe != null)
            return audioSourcePlayerSe.isPlaying;

        return false;
    }

    public static bool PausePlayerSe()
    {
        return GetInstance()._PausePlayerSe();
    }
    bool _PausePlayerSe()
    {
        audioSourcePlayerSe.Pause();
        return true;
    }

    public static bool UnPausePlayerSe()
    {
        return GetInstance()._UnPausePlayerSe();
    }
    bool _UnPausePlayerSe()
    {
        audioSourcePlayerSe.UnPause();
        return true;
    }
    #endregion

    #region gimmick se
    public static bool PlayGimmickSe(string seName)
    {
        return GetInstance()._PlayGimmickSe(seName);
    }
    bool _PlayGimmickSe(string seName)
    {
        if (!poolGimmickSe.ContainsKey(seName))
            return false;

        AudioClipInfo info = poolGimmickSe[seName];
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            GameObject.DontDestroyOnLoad(soundPlayer);

            audioSourceGimmickSe = soundPlayer.AddComponent<AudioSource>();
            audioSourceGimmickSe.clip = info.audioClip;
        }
        if (!HasExitSound(info))
        {
            audioSourceGimmickSe = soundPlayer.AddComponent<AudioSource>();
            audioSourceGimmickSe.clip = info.audioClip;
        }

        audioSourceGimmickSe.PlayOneShot(info.audioClip, 0.5f);

        return true;
    }
    public static bool PlayGimmickSe(string seName,float volume)
    {
        return GetInstance()._PlayGimmickSe(seName, volume);
    }
    bool _PlayGimmickSe(string seName, float volume)
    {
        if (!poolGimmickSe.ContainsKey(seName))
            return false;

        AudioClipInfo info = poolGimmickSe[seName];
        if (soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            GameObject.DontDestroyOnLoad(soundPlayer);

            audioSourceGimmickSe = soundPlayer.AddComponent<AudioSource>();
            audioSourceGimmickSe.clip = info.audioClip;
        }
        if (!HasExitSound(info))
        {
            audioSourceGimmickSe = soundPlayer.AddComponent<AudioSource>();
            audioSourceGimmickSe.clip = info.audioClip;
        }
        audioSourceGimmickSe.PlayOneShot(info.audioClip, volume);

        return true;
    }
    public static bool StopGimmickSe()
    {
        return GetInstance()._StopGimmickSe();
    }

    bool _StopGimmickSe()
    {
        if (audioSourceGimmickSe == null)
            return false;
        audioSourceGimmickSe.Stop();
        return true;
    }

    public static bool IsPlayingGimmickSe()
    {
        return GetInstance()._IsPlayingGimmickSe();
    }

    bool _IsPlayingGimmickSe()
    {
        if (audioSourceGimmickSe != null)
            return audioSourceGimmickSe.isPlaying;

        return false;
    }

    public static bool PauseGimmickSe()
    {
        return GetInstance()._PauseGimmickSe();
    }
    bool _PauseGimmickSe()
    {
        audioSourceGimmickSe.Pause();
        return true;
    }

    public static bool UnPauseGimmickSe()
    {
        return GetInstance()._UnPauseGimmickSe();
    }
    bool _UnPauseGimmickSe()
    {
        audioSourceGimmickSe.UnPause();
        return true;
    }
    #endregion
}
