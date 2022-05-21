using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AudioType { Button, HeroCard, Background}

[RequireComponent(typeof(AudioSource))]
public class AudioSourceOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        int SoundNumber = 0;
        switch (audioType)
        {
            case AudioType.Button:
                if (CheckClip(SoundNumber))
                    GetSource().PlayOneShot(audioClips[SoundNumber]);
                break;
            case AudioType.HeroCard:
                if (CheckClip(SoundNumber))
                    GetSource().PlayOneShot(audioClips[SoundNumber]);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int SoundNumber = 1;
        switch (audioType)
        {
            case AudioType.Button:
                if (CheckClip(SoundNumber))
                    GetSource().PlayOneShot(audioClips[SoundNumber]);
                break;
            case AudioType.HeroCard:
                if (CheckClip(SoundNumber))
                    GetSource().PlayOneShot(audioClips[SoundNumber]);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int SoundNumber = 2;
        switch (audioType)
        {
            case AudioType.Button:
                if (CheckClip(SoundNumber))
                    SoundManager.GlobalUIAudioSource.PlayOneShot(audioClips[SoundNumber]);
                break;
            case AudioType.HeroCard:
                if (CheckClip(SoundNumber))
                    GetSource().PlayOneShot(audioClips[SoundNumber]);
                break;
        }
    }

    private bool CheckClip(int SoundNumber)
    {
        return audioClips.Count > SoundNumber || audioClips[SoundNumber] != null;
    }
}
