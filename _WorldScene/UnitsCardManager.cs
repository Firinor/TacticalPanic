using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsCardManager : SinglBehaviour<UnitsCardManager>
{
    public static List<GameObject> unitCards { get; private set; } = new List<GameObject>();

    [SerializeField]
    private GameObject mercenaryCardPrefab;

    private void Awake()
    {
        SingletoneCheck(this);
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

                GameObject mercenaryCard = Instantiate(instance.mercenaryCardPrefab, instance.gameObject.transform);
                mercenaryCard.GetComponent<UnitTavernCardOperator>().SetUnit(unit);
                unit.SetUnitParty(Array.Exists(SaveManager.Data.Party, x => x == unit.id));
                unitCards.Add(mercenaryCard);
            }
        }
    }

    public void PartyToParent(Transform transform)
    {

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
        foreach (UnitTavernCardOperator unitCard in partyPanel)
        {
            unitCards.Add(unitCard.gameObject);
        }
    }
}
