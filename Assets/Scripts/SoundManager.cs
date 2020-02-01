using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public List<AudioClass> audios = new List<AudioClass>();

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
        audioSource = GetComponent<AudioSource>();
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


}

[System.Serializable]
public class AudioClass
{
    public string audioName;
    public AudioClip audioClip;
}