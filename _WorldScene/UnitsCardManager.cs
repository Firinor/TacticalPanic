using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardHolder { SquadCanvas, BriefingCanvas}

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
                mercenaryCard.GetComponent<UnitTavernCardOperator>().SetUnit(unit);
                unit.SetUnitParty(Array.Exists(SaveManager.Data.Party, x => x == unit.id));
                unitCards.Add(unit, mercenaryCard);
            }
        }
    }
    public static void CardsToParent(CardHolder holder)
    {
        switch (holder)
        {
            case CardHolder.SquadCanvas:
                CardsToParent(SquadCanvasOperator.GetPartyTransform(),
                    SquadCanvasOperator.GetTavernTransform(),
                    DragAndDropEnable: true);
                break;
            case CardHolder.BriefingCanvas:
                CardsToParent(BriefingCanvasOperator.GetPartyTransform());
                break;
        }
    }
    public static void CardsToParent(Transform inPartyTransform = null,
        Transform inTawernTransform = null,
        bool DragAndDropEnable = false)
    {
        foreach(UnitBasis unit in unitCards.Keys)
        {
            GameObject unitCard = unitCards[unit];
            UnitTavernCardOperator cardOperator = unitCard.GetComponent<UnitTavernCardOperator>();
            if (unit.inParty && inPartyTransform != null)
            {
                unitCard.transform.SetParent(inPartyTransform);
                unitCard.SetActive(true);
                cardOperator.BlockRaycasts(DragAndDropEnable);
            }
            else if (!unit.inParty && inTawernTransform != null)
            {
                unitCard.transform.SetParent(inTawernTransform);
                unitCard.SetActive(true);
                cardOperator.BlockRaycasts(DragAndDropEnable);
            }
            else
            {
                unitCard.SetActive(false);
            }
        }
    }

    private static bool ComplianceRequirement(UnitBasis unit)
    {
        UnitSprite unitSprite = unit.GetUnitSprite();

        if (unitSprite == null) return false;
        if (unitSprite.unitFace == null) return false;

        return true;
    }

    public static void SetParty(UnitTavernCardOperator[] partyPanel)
    {
        unitCards.Clear();
        S.ClearParty();
        foreach (UnitTavernCardOperator unitCard in partyPanel)
        {
            unitCards.Add(unitCard.GetUnit(), unitCard.gameObject);
            S.AddUnitToParty(unitCard.GetUnit());
        }
    }
}
