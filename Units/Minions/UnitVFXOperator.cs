using System;
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
        private float lifeTime;
        [SerializeField]
        private bool clockwise;
        [SerializeField]
        private Color slashColor;

        private Dictionary<string, int> indexes;

        internal void PlayOnce(Skill skill)
        {
            visualEffect.Play();
        }

        void Awake()
        {
            indexes = GetIndexes();
            SetValue(indexes[nameof(loocAtAngle)], loocAtAngle);
            SetValue(indexes[nameof(lifeTime)], lifeTime);
            SetValue(indexes[nameof(clockwise)], clockwise);
            SetValue(indexes[nameof(slashColor)], slashColor);
        }

        private Dictionary<string, int> GetIndexes()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result.Add(nameof(loocAtAngle), Shader.PropertyToID(nameof(loocAtAngle)));
            result.Add(nameof(lifeTime), Shader.PropertyToID(nameof(lifeTime)));
            result.Add(nameof(clockwise), Shader.PropertyToID(nameof(clockwise)));
            result.Add(nameof(slashColor), Shader.PropertyToID(nameof(slashColor)));

            return result;
        }

        private void SetValue<T>(int index, in T value)
        {
            string nameOfType = typeof(T).Name;

            switch (nameOfType)
            {
                case "Int32":
                    if(value is int newIntValue)
                    {
                        visualEffect.SetInt(index, newIntValue); 
                    }
                    break;
                case "Single":
                    if (value is float newFloatValue)
                    {
                        visualEffect.SetFloat(index, newFloatValue);
                    }
                    break;
                case "Boolean":
                    if (value is bool newBoolValue)
                    {
                        visualEffect.SetBool(index, newBoolValue);
                    }
                    break;
                case "Color":
                    if (value is Color newColorValue)
                    {
                        visualEffect.SetVector4(index, newColorValue);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
