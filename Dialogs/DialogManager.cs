using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : SinglBehaviour<DialogManager>
{
    [SerializeField]
    private GameObject leftSpeaker;
    [SerializeField]
    private GameObject centerSpeaker;
    [SerializeField]
    private GameObject rightSpeaker;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private Image background;

    public static GameObject Left { get { return instance.leftSpeaker; } }
    public static GameObject Center { get { return instance.centerSpeaker; } }
    public static GameObject Right { get { return instance.rightSpeaker; } }
    public static string Text { get { return instance.text.text; } set{ instance.text.text = value; } }
    public static Sprite Background { get { return instance.background.sprite; } set { instance.background.sprite = value; } }

    void Awake()
    {
        SingletoneCheck<DialogManager>(this);
    }

    public IEnumerator StartDialog()
    {
        yield break;
    }
}
