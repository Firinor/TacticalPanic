using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    [CreateAssetMenu(menuName = "Dialog/New dialog", fileName = "Dialog")]
    public class DialogInformator : ScriptableObject
    {
        public List<SpeakersPhrase> Dialog;
        public int Length { get { return Dialog == null ? 0 : Dialog.Count; } }

        public SpeakersPhrase this[int index]
        {
            get => Dialog[index];
        }

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
}


