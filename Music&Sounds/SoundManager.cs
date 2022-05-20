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

    public static AudioSource GlobalAudioSource { get; private set; }

    void Awake()
    {
        GlobalAudioSource = GetComponent<AudioSource>();

        var BackgroundOperator = SoundInformator.GetBackgroundMusicOperator();
        BackgroundOperator.SetSound(backgroundMusic);

        var ButtonOperators = SoundInformator.GetButtonsMusicOperators();
        List<AudioClip> buttonsSound = new List<AudioClip>() { buttonOnMouseEnter, buttonOnMouseExit , buttonOnClic };
        foreach (var Operator in ButtonOperators)
            {
                Operator.SetSound(buttonsSound);
            }
    }
}
