using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class UnitCardStats : MonoBehaviour
    {
        private UnitOperator unit;
        private GameObject unitPrefab;
        [SerializeField]
        private Image cardImage;

        [SerializeField]
        private Text nameOfUnit;

        [SerializeField]
        private Text[] ManaText = new Text[PlayerOperator.GistsCount];

        public void Start()
        {
            unit.Prepare(ConflictSide.Player);

            cardImage.sprite = unit.SpriteInfo;
            GetManaPriseText();
            nameOfUnit.text = unit.GetTextInfo();
        }

        private void GetManaPriseText()
        {
            for (int i = 0; i < unit.GistBasis.Length; i++)
            {
                GistBasis basis = unit.GistBasis[i];
                int manaPrise = unit.GetElementManaPrice(i);
                if (ManaText[i] != null && manaPrise != 0)
                {
                    ManaText[i].gameObject.SetActive(true);
                    ManaText[i].text = $"<color={unit.GetElementColorString(basis.gist)}>{manaPrise}</color>";
                }
            }
        }

        public void SetCardUnit(UnitOperator unit)
        {
            this.unit = unit;
            unitPrefab = unit.gameObject;
        }

        public GameObject GetUnitPrefab()
        {
            return unitPrefab;
        }
    }
}