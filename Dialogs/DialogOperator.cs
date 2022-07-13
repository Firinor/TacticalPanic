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

    [SerializeField]
    private DialogInformator dialog;

    private StringBuilder strindBuilder = new StringBuilder();

    private Dictionary<UnitInformator, SpeakerOperator> speakers = new Dictionary<UnitInformator, SpeakerOperator>();
    private SpeakerOperator activeSpeaker = null;

    public static GameObject Left { get { return instance.leftSpeaker; } }
    public static GameObject Center { get { return instance.centerSpeaker; } }
    public static GameObject Right { get { return instance.rightSpeaker; } }
    public static string Text { get { return instance.text.text; } set{ instance.text.text = value; } }
    public static Sprite Background { get { return instance.background.sprite; } set { instance.background.sprite = value; } }

    void Awake()
    {
        SingletoneCheck<DialogOperator>(this);
        StartCoroutineDialog(dialog);
    }

    public void StartCoroutineDialog(DialogInformator dialog)
    {
        StartCoroutine(StartDialog(dialog));
    }

    public IEnumerator StartDialog(DialogInformator dialog)
    {
        ClearAllSpeakers();
        bool NextInput = false;
        for (int i = 0; i < dialog.Length; i++)
        {
            DialogInformator.SpeakersPhrase speakersPhrase = dialog[i];

            if(speakersPhrase.Position == StagePosition.Off)
            {
                LeaveTheStage(speakersPhrase.Speaker);
                continue;
            }

            strindBuilder.Clear();
            nextArrow.enabled = false;
            NextInput = false;
            text.text = strindBuilder.ToString();
            
            SetBackground(speakersPhrase.background);
            UnitInformator speaker = CheckSpeaker(speakersPhrase);
            SetActiveSpeaker(speaker);
            string phrase = speakersPhrase.text;
            for (int j = 0; j < phrase.Length; j++)
            {
                strindBuilder.Append(speakersPhrase.text[j]);
                text.text = strindBuilder.ToString();
                yield return new WaitForSeconds(lettersDelay);
            }
            while (!NextInput)
            {
                if (Input.anyKeyDown) { NextInput = true; }
                yield return null;
            }
        }
        EndOfDialog();
    }

    private void LeaveTheStage(UnitInformator speaker)
    {
        SpeakerOperator speakerOperator = null;

        if (speakers.ContainsKey(speaker))
        {
            speakerOperator = speakers[speaker];
            speakers.Remove(speaker);
        }

        if (speakerOperator != null)
            Destroy(speakerOperator.gameObject);
    }

    private void EndOfDialog()
    {
        gameObject.SetActive(false);
    }

    private void ClearAllSpeakers()
    {
        speakers.Clear();

        List<SpeakerOperator> gameObjectToDelete = new List<SpeakerOperator>();
        AllSpeakersIn(gameObjectToDelete, leftSpeaker);
        AllSpeakersIn(gameObjectToDelete, centerSpeaker);
        AllSpeakersIn(gameObjectToDelete, rightSpeaker);

        foreach(SpeakerOperator speaker in gameObjectToDelete)
        {
            Destroy(speaker.gameObject);
        }
    }

    private void AllSpeakersIn(List<SpeakerOperator> gameObjectToDelete, GameObject side)
    {
        foreach (SpeakerOperator gameObject in side.GetComponentsInChildren<SpeakerOperator>())
        {
            gameObjectToDelete.Add(gameObject);
        }
    }

    private UnitInformator CheckSpeaker(DialogInformator.SpeakersPhrase speakersPhrase)
    {
        UnitInformator speaker = speakersPhrase.Speaker;
        if (!speakers.ContainsKey(speaker))
        {
            GameObject newSpeaker = Instantiate(speakerPrefab, GetSpeakerParent(speakersPhrase.Position));
            speakers.Add(speakersPhrase.Speaker, newSpeaker.GetComponent<SpeakerOperator>());
            speakers[speaker].SetImage(speaker);
        }
        else
        {
            speakers[speaker].transform.SetParent(GetSpeakerParent(speakersPhrase.Position));
        }

        speakerName.text = speaker.Name;

        return speaker;
    }

    private void SetBackground(Sprite background)
    {
        this.background.sprite = background;
    }

    private void SetActiveSpeaker(UnitInformator speaker)
    {
        if ( activeSpeaker != null 
            && activeSpeaker == speakers[speaker])
            return;

        if (activeSpeaker != null)
            activeSpeaker.ToTheBackground();

        activeSpeaker = speakers[speaker];
        activeSpeaker.ToTheForeground();
    }

    private Transform GetSpeakerParent(StagePosition position)
    {
        return position switch
        {
            StagePosition.Left => leftSpeaker.transform,
            StagePosition.Right => rightSpeaker.transform,
            _ => centerSpeaker.transform,
        };
    }
}
