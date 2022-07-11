using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/New dialog", fileName = "Dialog")]
public class DialogInformator : ScriptableObject
{
    public List<SpeakersPhrase> Dialog;

    [System.Serializable]
    public class SpeakersPhrase
    {
        public enum PositionOnTheStage { Left, Center, Right, Off }

        public Sprite background;
        public UnitInformator Speaker;
        public PositionOnTheStage Position;
        [Multiline]
        public string text;
    }
}


