using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;
using StagePosition = DialogInformator.SpeakersPhrase.PositionOnTheStage;

public class DialogOperator : SinglBehaviour<DialogOperator>
{
    [SerializeField]
    private GameObject speakerPrefab;
    [Space]
    [SerializeField]
    private Image background;
    [SerializeField]
    private GameObject leftSpeaker;
    [SerializeField]
    private GameObject centerSpeaker;
    [SerializeField]
    private GameObject rightSpeaker;
    [SerializeField]
    private TextMeshProUGUI speakerName;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private float lettersDelay;
    [SerializeField]
    private Image nextArrow;

    private StringBuilder strindBuilder = new StringBuilder();

    private Dictionary<UnitInformator, SpeakerOperator> speakers = new Dictionary<UnitInformator, SpeakerOperator>();
    private UnitInformator activeSpeaker;

    public static GameObject Left { get { return instance.leftSpeaker; } }
    public static GameObject Center { get { return instance.centerSpeaker; } }
    public static GameObject Right { get { return instance.rightSpeaker; } }
    public static string Text { get { return instance.text.text; } set{ instance.text.text = value; } }
    public static Sprite Background { get { return instance.background.sprite; } set { instance.background.sprite = value; } }

    void Awake()
    {
        SingletoneCheck<DialogOperator>(this);
    }

    public IEnumerator StartDialog(DialogInformator dialog)
    {
        bool AllLetters = false;
        strindBuilder.Clear();
        for (int i = 0; i < dialog.Length; i++)
        {
            nextArrow.enabled = false;
            AllLetters = false;
            text.text = "";
            DialogInformator.SpeakersPhrase speakersPhrase = dialog[i];
            SetBackground(speakersPhrase.background);
            UnitInformator speaker = CheckSpeaker(speakersPhrase);
            SetActiveSpeaker(speaker);
            while (!AllLetters)
            {

                yield return new WaitForSeconds(lettersDelay);
            }
                


        }
        yield break;
    }

    private UnitInformator CheckSpeaker(DialogInformator.SpeakersPhrase speakersPhrase)
    {
        UnitInformator speaker = speakersPhrase.Speaker;
        if (!speakers.ContainsKey(speaker))
        {
            GameObject newSpeaker = Instantiate(speakerPrefab, GetParent(speakersPhrase.Position));
            speakers.Add(speakersPhrase.Speaker, newSpeaker.GetComponent<SpeakerOperator>());
        }
        else
        {
            speakers[speaker].transform.SetParent(GetParent(speakersPhrase.Position));
        }

        return speaker;
    }

    private void SetBackground(Sprite background)
    {
        throw new NotImplementedException();
    }

    private void SetActiveSpeaker(UnitInformator speaker)
    {
        if (activeSpeaker == speaker)
            return;

        speakers[speaker].ToTheWayside();

        activeSpeaker = speaker;
    }

    private Transform GetParent(StagePosition position)
    {
        return position switch
        {
            StagePosition.Left => leftSpeaker.transform,
            StagePosition.Right => rightSpeaker.transform,
            _ => centerSpeaker.transform,
        };
    }
}
