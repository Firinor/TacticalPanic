using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    [RequireComponent(typeof(UnitOperator))]
    public class UnitSoundOperator : MonoBehaviour
    {
        [SerializeField]
        private Sounds sounds;

        internal void PlaySound(UnitSounds death)
        {
            audioOperator.PlaySound((int)death, this);
        }

        private AudioSourceOperator audioOperator;

        public void Awake()
        {
            sounds.SetSounds(SoundInformator.GetUnitSounds(sounds.GetMaterials()));
            audioOperator = GetComponentInChildren<AudioSourceOperator>();
        }

        public AudioClip GetDeathSound()
        {
            return sounds.Death;
        }
        public AudioClip GetHitSound()
        {
            return sounds.Hit;
        }
        public AudioClip GetAttackSound()
        {
            return sounds.Attack;
        }

        [Serializable]
        private class Sounds
        {
            [HideInInspector]
            public AudioClip Death;
            [HideInInspector]
            public AudioClip Hit;
            [HideInInspector]
            public AudioClip Attack;

            [SerializeField]
            private MaterialSoundType DeathSoundType;
            [SerializeField]
            private MaterialSoundType HitSoundType;
            [SerializeField]
            private MaterialSoundType AttackSoundType;

            public void SetSounds(AudioClip[] clips)
            {
                if (clips != null && clips.Length == 3)
                {
                    Death = clips[0];
                    Hit = clips[1];
                    Attack = clips[2];
                }
            }

            public MaterialSoundType[] GetMaterials()
            {
                return new MaterialSoundType[] { DeathSoundType, HitSoundType, AttackSoundType };
            }
        }
    }
}
