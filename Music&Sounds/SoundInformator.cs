using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInformator : MonoBehaviour
{
    private List<AudioSourceOperator> backgroundMusicSource = new List<AudioSourceOperator>();
    private List<AudioSourceOperator> buttonsSource = new List<AudioSourceOperator>();

    public static SoundInformator instance;

    void Awake()
    {
        instance = this;
        AudioSourceOperator[] AudioOperators = FindObjectsOfType<AudioSourceOperator>(true);
        for (int i = 0; i < AudioOperators.Length; i++)
        {
            switch (AudioOperators[i].GetAudioType())
            {
                case AudioType.Button:
                    buttonsSource.Add(AudioOperators[i]);
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

    public static AudioSourceOperator GetBackgroundMusicOperator()
    {
        return instance.backgroundMusicSource[0];
    }

    public static List<AudioSourceOperator> GetButtonsMusicOperators()
    {
        return instance.buttonsSource;
    }
}
