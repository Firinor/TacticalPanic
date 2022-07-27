using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TacticalPanicCode
{
    [RequireComponent(typeof(UnitOperator))]
    public class UnitVFXOperator : MonoBehaviour
    {
        [SerializeField]
        private VisualEffect visualEffect;
        [SerializeField]
        [Range (0, 359)]
        private int loocAtAngle;
        [SerializeField]
        private float slashSpeed = 10;

        void Awake()
        {
            SetAngle();
            SetSpeed();
        }

        private void SetAngle(int loocAtAngle)
        {
            this.loocAtAngle = loocAtAngle;
            SetAngle();
        }
        private void SetAngle()
        {
            visualEffect.SetFloat("SlashSpeed", loocAtAngle);
        }

        private void SetSpeed(float slashSpeed)
        {
            this.slashSpeed = slashSpeed;
            SetSpeed();
        }
        private void SetSpeed()
            {
                visualEffect.SetFloat("SlashSpeed", slashSpeed);
        }
    }
}
