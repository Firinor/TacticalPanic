using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AudioType { Button, HeroCard, Background, Unit}

[RequireComponent(typeof(AudioSource))]
public class AudioSourceOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;
    [SerializeField]
    private AudioType audioType;

    void Awake()
    {
        audioClips = SoundInformator.GetClips(audioType);
        if (audioType == AudioType.Background)
        {
            GetSource().clip = audioClips[0];
            GetSource().Play();
        }
    }

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
            case AudioType.HeroCard:
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
                PlaySound(SoundNumber, false);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int SoundNumber = 1;
        switch (audioType)
        {
            case AudioType.Button:
                PlaySound(SoundNumber, false);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int SoundNumber = 2;
        switch (audioType)
        {
            case AudioType.Button:
                PlaySound(SoundNumber, true);
                break;
            case AudioType.HeroCard:
                PlaySound(SoundNumber, false);
                break;
        }
    }

    public void PlaySound(int SoundNumber, bool global)
    {
        if (CheckClip(SoundNumber))
            if(global)
                SoundInformator.GlobalUIAudioSource.PlayOneShot(audioClips[SoundNumber]);
            else
                GetSource().PlayOneShot(audioClips[SoundNumber]);
    }

    public void PlaySound(UnitSounds unitSounds, Unit unit)
    {
        switch (unitSounds)
        {
            case UnitSounds.Death:
                SoundInformator.GlobalUIAudioSource.PlayOneShot(unit.GetDeathSound());
                break;
            case UnitSounds.Hit:
                if (!GetSource().isPlaying)
                {
                    GetSource().clip = unit.GetHitSound();
                    GetSource().Play();
                }
                
                break;
            case UnitSounds.Attack:
                if (!GetSource().isPlaying)
                {
                    GetSource().clip = unit.GetAttackSound();
                    GetSource().Play();
                }
                break;
        }
    }

    private bool CheckClip(int SoundNumber)
    {
        return audioClips.Count > SoundNumber && audioClips[SoundNumber] != null;
    }
}
