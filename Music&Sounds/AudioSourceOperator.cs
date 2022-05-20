using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AudioType { Button, Background}

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(AudioSource))]
public class AudioSourceOperator : MonoBehaviour
{
    private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;
    [SerializeField]
    private AudioType audioType;

    private AudioSource GetSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        return audioSource;
    }

    public void SetSound(AudioClip clip)
    {
        SetSound(new List<AudioClip>() { clip });
    }

    public void SetSound(List<AudioClip> clips)
    {
        if (clips.Count <= 0) 
            return;

        switch (audioType)
        {
            case AudioType.Button:
                audioClips = clips;
                break;
            case AudioType.Background:
                audioClips = clips;
                GetSource().clip = audioClips[0];
                GetSource().Play();
                break;
            default:
                new Exception("Unrealized audio type!");
                break;
        }
    }

    public AudioType GetAudioType()
    {
        return audioType;
    }

    public void PlaySoundFromList(int i, bool global)
    {
        if (audioClips.Count <= i || audioClips[i] == null)
            return;

        if (global)
            SoundManager.GlobalAudioSource.PlayOneShot(audioClips[i]);
        else
            GetSource().PlayOneShot(audioClips[i]);
    }

}
