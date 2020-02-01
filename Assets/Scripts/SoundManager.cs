using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public List<AudioClass> audios = new List<AudioClass>();

    [EnumNamedArray(typeof(EAudioClip))]
    public List<AudioClip> clips = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        PlayClip(EAudioClip.BACKGROUND_MUSIC,true,true);
    }

    public void PlayAudioClip(string name)
    {
        for(int i=0;i<audios.Count;++i)
        {
            if(audios[i].audioName==name)
            {
                audioSource.clip = audios[i].audioClip;
                audioSource.Play();
                break;

            }
        }
    }

    public void PlayClip(EAudioClip clip, bool loop = false, bool dontDestroy = false,float destroyAfter = 0)
    {
        if(clips[(int)clip] == null)
        {
            return;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = loop;
        audioSource.clip = clips[(int)clip];
        audioSource.Play();
        if(!dontDestroy)
        {
            Destroy(audioSource, destroyAfter);
        }
    }
}

public enum EAudioClip
{
    NONE = -1,
    BACKGROUND_MUSIC,
    SUCCESS_SFX,
    FAILURE_SFX,
}

[System.Serializable]
public class AudioClass
{
    public string audioName;
    public AudioClip audioClip;
}