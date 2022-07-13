using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerOperator : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Transform imageTransform;
    [SerializeField]
    private Color speakerOnBackgroundColor;
    [SerializeField]
    private Vector3 scaleOnBackground;

    internal void SetImage(UnitInformator speaker)
    {
        image.sprite = speaker.unitSprite;
    }

    public void ToTheBackground()
    {
        image.color = speakerOnBackgroundColor;
        imageTransform.localScale = scaleOnBackground;
    }

    public void ToTheForeground()
    {
        image.color = Color.white;
        imageTransform.localScale = Vector3.one;
    }
}
