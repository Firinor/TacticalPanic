using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
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

    public static AudioSource GlobalUIAudioSource { get; private set; }

    void Awake()
    {
        GlobalUIAudioSource = GetComponent<AudioSource>();

        var BackgroundOperator = SoundInformator.GetBackgroundMusicOperator();
        BackgroundOperator.SetSound(backgroundMusic);

        var ButtonOperators = SoundInformator.GetButtonsMusicOperators();
        List<AudioClip> buttonsSound = new List<AudioClip>() { buttonOnMouseEnter, buttonOnMouseExit , buttonOnClic };
        foreach (var Operator in ButtonOperators)
            {
                Operator.SetSound(buttonsSound);
            }

        var CardOperators = SoundInformator.GetCardMusicOperators();
        List<AudioClip> cardsSound = new List<AudioClip>() { cardOnMouseEnter, cardOnMouseExit, cardOnClic };
        foreach (var Operator in CardOperators)
        {
            Operator.SetSound(cardsSound);
        }
    }
}
