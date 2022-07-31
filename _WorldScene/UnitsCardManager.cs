using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public enum CardDirectionView { Left = -1, Right = 1 }
    public enum CardHolder { SquadCanvas, BriefingCanvas }

    public class UnitsCardManager : SinglBehaviour<UnitsCardManager>
    {
        public static Dictionary<UnitBasis, GameObject> unitCards { get; private set; }
            = new Dictionary<UnitBasis, GameObject>();

        [SerializeField]
        private GameObject mercenaryCardPrefab;
        [SerializeField]
        private Transform defaultParent;

        private void Awake()
        {
            SingletoneCheck(this);
            CreateUnits();
        }

        public static void CreateUnits()
        {
            if (unitCards.Count == 0)
            {
                unitCards.Clear();
                foreach (UnitBasis unit in DB.Units)
                {
                    if (!ComplianceRequirement(unit))
                        continue;

                    GameObject mercenaryCard = Instantiate(instance.mercenaryCardPrefab, instance.defaultParent);
                    mercenaryCard.GetComponent<UnitCardOperator>().SetUnit(unit);
                    unitCards.Add(unit, mercenaryCard);
                }
            }
        }

        internal static void DirectionOfCards(List<UnitBasis> enemies, CardDirectionView direction)
        {
            foreach (UnitBasis unit in enemies)
            {
                unitCards[unit].transform.localScale = new Vector3((float)direction, 1, 1);
            }
        }

        public static void CardsToParent(CardHolder holder)
        {
            switch (holder)
            {
                case CardHolder.SquadCanvas:
                    CardsToParent(PlayerManager.Party, SquadCanvasOperator.GetPartyTransform(), blockRaycasts: true);
                    CardsToParent(UnitsNotInParty(), SquadCanvasOperator.GetTavernTransform(), blockRaycasts: true);
                    break;
                case CardHolder.BriefingCanvas:
                    CardsToParent(PlayerManager.Party, BriefingCanvasOperator.GetPartyTransform());
                    break;
            }
        }

        private static List<UnitBasis> UnitsNotInParty()
        {
            List<UnitBasis> result = new List<UnitBasis>();

            foreach (UnitBasis unit in DB.Units)
            {
                if (unit != null && unit.unitInformator != null && unit.unitInformator.Playable && !PlayerManager.UnitInParty(unit))
                {
                    result.Add(unit);
                }
            }

            return result;
        }

        public static void CardsToParent(List<UnitBasis> units, Transform parentTransform,
            bool blockRaycasts = false)
        {
            DisabledAllCards(parentTransform);

            foreach (UnitBasis unit in units)
            {
                GameObject unitCard = unitCards[unit];
                UnitCardOperator cardOperator = unitCard.GetComponent<UnitCardOperator>();
                unitCard.transform.SetParent(parentTransform);
                unitCard.SetActive(true);
                cardOperator.BlockRaycasts(blockRaycasts);
            }
        }

        private static void DisabledAllCards(Transform transform)
        {
            foreach (UnitCardOperator cardOperator in transform.gameObject.GetComponentsInChildren<UnitCardOperator>())
            {
                cardOperator.gameObject.SetActive(false);
            }
        }

        private static bool ComplianceRequirement(UnitBasis unit)
        {
            UnitInformator unitInformator = unit.unitInformator;

            if (unitInformator == null) return false;
            if (unitInformator.unitFace == null) return false;

            return true;
        }

        public static void SetParty(UnitCardOperator[] partyPanel)
        {
            unitCards.Clear();
            PlayerManager.ClearParty();
            foreach (UnitCardOperator unitCard in partyPanel)
            {
                unitCards.Add(unitCard.GetUnit(), unitCard.gameObject);
                PlayerManager.AddUnitToParty(unitCard.GetUnit());
            }
        }
    }
}
