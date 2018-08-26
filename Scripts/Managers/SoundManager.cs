using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundManager", menuName = "ScriptableObjectSingleTon/SoundManager")]
public class SoundManager : ScriptableObjectSingleton<SoundManager>
{
    private void OnEnable()
    {
        isStartGetAudioSource = false;
    }


    #region Inspector
    [SerializeField, SetProperty(nameof(MusicVolume))]
    private float musicVolume;
    [SerializeField, SetProperty(nameof(SFXVolume))]
    private float sFXVolume;
    [SerializeField]
    private float soundCollectUpdateTime = 10f;
    #endregion


    public List<SoundInfo> soundInfoList = new List<SoundInfo>();
    private AudioSource musicAudioSource;
    private bool isStartGetAudioSource;


    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }

        set
        {
            //设置背景音乐音量
            if (MusicAudioSource != null)
            {
                MusicAudioSource.volume = value;
            }
            musicVolume = value;
        }
    }
    public float SFXVolume
    {
        get
        {
            return sFXVolume;
        }

        set
        {
            //设置所有音效音量
            SetAllSFXVolume(value);
            sFXVolume = value;
        }
    }

    protected bool IsStartGetAudioSource
    {
        get
        {
            return isStartGetAudioSource;
        }
        set
        {
            if (value && !isStartGetAudioSource)
            {
                CoroutineManager.Instance.StartOneCoroutine(SoundCollectUpdate());
                isStartGetAudioSource = value;
            }
        }
    }

    public AudioSource MusicAudioSource
    {
        get
        {
            if (musicAudioSource == null)
            {
                musicAudioSource = new GameObject("MusicSource").AddComponent<AudioSource>();
                GameObject.DontDestroyOnLoad(musicAudioSource);
                musicAudioSource.loop = true;
                musicAudioSource.volume = MusicVolume;
            }
            return musicAudioSource;
        }
    }


    public void PlayBackgroundMusic(AudioClip clip)
    {
        MusicAudioSource.clip = clip;
        MusicAudioSource.Play();
    }
    public void PauseBackgroundMusic()
    {
        MusicAudioSource.Pause();
    }



    public SoundInfo PlaySound2D(AudioClip clip)
    {
        AudioSource curAudioSource = GetAudioSourceFromSoundGameObject();
        curAudioSource.clip = clip;
        curAudioSource.spatialBlend = 0;
        curAudioSource.volume = SFXVolume;
        SoundInfo soundInfo = new SoundInfo(SoundType._2DSound, curAudioSource.gameObject, curAudioSource.clip);
        soundInfo.canCollected = true;
        if (soundInfo.audioSource.loop == false)
        {
            soundInfoList.Add(soundInfo);
        }
        curAudioSource.Play();
        return soundInfo;
    }
    public void PauseSound2D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Pause();
    }
    public void StopSound2D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Stop();
        soundInfo.audioSource.clip = null;
        soundInfo.canCollected = false;
        PutSoundGameObject(soundInfo.soundGameObject);
    }



    public SoundInfo PlaySound3D(AudioClip clip, Vector3 soundPos)
    {
        AudioSource curAudioSource = GetAudioSourceFromSoundGameObject();
        curAudioSource.clip = clip;
        curAudioSource.spatialBlend = 1;
        curAudioSource.transform.position = soundPos;
        curAudioSource.volume = SFXVolume;
        SoundInfo soundInfo = new SoundInfo(SoundType._3DSound, curAudioSource.gameObject, curAudioSource.clip);
        soundInfo.canCollected = true;
        if (soundInfo.audioSource.loop == false)
        {
            soundInfoList.Add(soundInfo);
        }
        curAudioSource.Play();
        return soundInfo;
    }
    public void PauseSound3D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Pause();
    }

    public void StopSound3D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Stop();
        soundInfo.audioSource.clip = null;
        soundInfo.canCollected = false;
        PutSoundGameObject(soundInfo.soundGameObject);
    }


    private IEnumerator SoundCollectUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(soundCollectUpdateTime);
            for (int i = 0; i < soundInfoList.Count; i++)
            {
                if (!soundInfoList[i].audioSource.isPlaying && soundInfoList[i].canCollected)
                {
                    StopSound2D(soundInfoList[i]);
                }
            }
        }
    }

    private AudioSource GetAudioSourceFromSoundGameObject()
    {
        IsStartGetAudioSource = true;
        GameObject soundGameObject = ObjectPoolManager.Instance.GetGameObject(FolderPaths.SoundGameObject, "SoundGameObject");
        if (soundGameObject.GetComponent<AudioSource>() != null)
        {
            return soundGameObject.GetComponent<AudioSource>();
        }
        else
        {
            return soundGameObject.AddComponent<AudioSource>();
        }
    }

    private void PutSoundGameObject(GameObject soundGameObject)
    {
        ObjectPoolManager.Instance.PutGameObject(soundGameObject);
    }


    private void SetAllSFXVolume(float volume)
    {
        for (int i = 0; i < soundInfoList.Count; i++)
        {
            soundInfoList[i].audioSource.volume = SFXVolume;
        }
    }
}





public class SoundInfo
{
    public SoundInfo(SoundType soundType, GameObject soundGameObject, AudioClip audioClip)
    {
        this.soundType = soundType;
        this.soundGameObject = soundGameObject;
        this.bindAudioClip = audioClip;
        this.audioSource = soundGameObject.GetComponent<AudioSource>();
    }
    private SoundInfo() { }
    public SoundType soundType;
    public GameObject soundGameObject;
    public AudioClip bindAudioClip;
    public AudioSource audioSource;

    public bool canCollected = true;

}
