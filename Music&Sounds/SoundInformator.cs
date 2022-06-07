using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundInformator : SinglBehaviour<SoundInformator>
{
    [SerializeField]
    private AudioClip backgroundMusic;

    [Space]
    [SerializeField]
    private AudioClip buttonOnMouseEnter;
    [SerializeField]
    private AudioClip buttonOnMouseExit;
    [SerializeField]
    private AudioClip buttonOnClic;

    [Space]
    [SerializeField]
    private AudioClip cardOnMouseEnter;
    [SerializeField]
    private AudioClip cardOnMouseExit;
    [SerializeField]
    private AudioClip cardOnClic;

    [Space]
    [Header("Звуки юнитов")]
    [SerializeField]
    private MaterialSounds flesh;
    [SerializeField]
    private MaterialSounds wood;
    [SerializeField]
    private MaterialSounds metal;

    private List<AudioSourceOperator> backgroundMusicSource = new List<AudioSourceOperator>();
    private List<AudioSourceOperator> buttonsSource = new List<AudioSourceOperator>();
    private List<AudioSourceOperator> cardsSource = new List<AudioSourceOperator>();

    public static AudioSource GlobalUIAudioSource { get; private set; }

    void Awake()
    {
        SingltoneCheck(this);
        GlobalUIAudioSource = GetComponent<AudioSource>();

        AudioSourceOperator[] AudioOperators = FindObjectsOfType<AudioSourceOperator>(true);

        for (int i = 0; i < AudioOperators.Length; i++)
        {
            switch (AudioOperators[i].GetAudioType())
            {
                case AudioType.Button:
                    buttonsSource.Add(AudioOperators[i]);
                    break;
                case AudioType.HeroCard:
                    cardsSource.Add(AudioOperators[i]);
                    break;
                case AudioType.Background:
                    backgroundMusicSource.Add(AudioOperators[i]);
                    break;
                default:
                    new Exception("Unrealized audio type!");
                    break;
            }
        }
    }

    public static List<AudioClip> GetClips(AudioType type)
    {
        switch (type)
        {
            case AudioType.Background:
                return GetBackgroundMusic();
            case AudioType.Button:
                return GetButtonsMusic();
            case AudioType.HeroCard:
                return GetCardMusic();
        }
        return new List<AudioClip>();
    }

    public static List<AudioSourceOperator> GetBackgroundOperators()
    {
        return instance.backgroundMusicSource;
    }

    public static List<AudioSourceOperator> GetButtonsOperators()
    {
        return instance.buttonsSource;
    }

    public static List<AudioSourceOperator> GetCardOperators()
    {
        return instance.cardsSource;
    }

    public static AudioClip[] GetUnitSounds(MaterialSoundType[] types)
    {
        AudioClip[] result = new AudioClip[types.Length];

        for (int i = 0; i < result.Length; i++)
        {
            switch (types[i])
            {
                case MaterialSoundType.Flesh:
                    result[i] = instance.flesh.GetClipByIndex(i);
                    break;
                case MaterialSoundType.Wood:
                    result[i] = instance.wood.GetClipByIndex(i);
                    break;
                case MaterialSoundType.Metal:
                    result[i] = instance.metal.GetClipByIndex(i);
                    break;
            }
        }

        return result;
    }

    public static List<AudioClip> GetBackgroundMusic()
    {
        return new List<AudioClip>() { instance.backgroundMusic };
    }

    public static List<AudioClip> GetButtonsMusic()
    {
        return new List<AudioClip>() {
            instance.buttonOnMouseEnter,
            instance.buttonOnMouseExit,
            instance.buttonOnClic };
    }

    public static List<AudioClip> GetCardMusic()
    {
        return new List<AudioClip>() {
            instance.cardOnMouseEnter,
            instance.cardOnMouseExit,
            instance.cardOnClic };
    }
}

[Serializable]
public class MaterialSounds
{
    [SerializeField]
    public AudioClip unitDeath;
    [SerializeField]
    public AudioClip unitHit;
    [SerializeField]
    public AudioClip unitAttack;

    public AudioClip GetClipByIndex(int i)
    {
        switch (i)
        {
            case 0:
                return unitDeath;
            case 1:
                return unitHit;
            case 2:
                return unitAttack;
        }
        new Exception("Sound value outside the array!");
        return null;
    }
}
