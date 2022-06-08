using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadOperator : SinglBehaviour<SquadOperator>
{
    public static List<int> result { get; private set; } = new List<int>() { 0 };

    [Header("Main")]
    [SerializeField]
    private InfoPanelOperator infoPanel;
    [SerializeField]
    private GameObject mercenaryCardPrefab;

    [Header("Drag & Drop settings")]
    [SerializeField]
    private GameObject tavernPanel;
    [SerializeField]
    private GameObject partyPanel;
    [SerializeField]
    private UnitTavernCardOperator tawernUnitCardShadow;

    [SerializeField]
    private UnitTavernCardOperator partyUnitCardShadow;

    private void Awake()
    {
        SingletoneCheck(this);

        if (tavernPanel.transform.childCount <= 1)
        {
            foreach (UnitBasis unit in DB.Units)
            {
                if (!ComplianceRequirement(unit))
                    continue;

                GameObject parent;
                if (Array.Exists(SaveManager.Data.Party, x => x == unit.id))
                    parent = partyPanel;
                else
                    parent = tavernPanel;
                GameObject mercenaryCard = Instantiate(mercenaryCardPrefab, parent.transform);
                mercenaryCard.GetComponent<UnitTavernCardOperator>().SetUnit(unit);
            }
        }
    }

    public static void CardOnBeginDrag(Transform cardTransform, int index)
    {
        GameObject shadowGameObject;

        Transform parent = cardTransform.parent;

        if (parent == instance.tavernPanel.transform)
        {
            shadowGameObject = instance.tawernUnitCardShadow.gameObject;
        }
        else if (parent == instance.partyPanel.transform)
        {
            shadowGameObject = instance.partyUnitCardShadow.gameObject;
        }
        else { return; }

        shadowGameObject.SetActive(true);
        shadowGameObject.transform.SetSiblingIndex(index);
        
        cardTransform.SetParent(instance.gameObject.transform);
    }

    public static void CardOnEndDrag()
    {
        instance.tawernUnitCardShadow.gameObject.SetActive(false);
        instance.partyUnitCardShadow.gameObject.SetActive(false);
        if(instance == null)
            return;
        instance.SetParty();
        SaveManager.Save(S.account);
    }

    internal static void CardOnDrop(UnitTavernCardOperator cardOperator, GameObject parent)
    {
        cardOperator.SetParent(parent.transform);
    }

    private bool ComplianceRequirement(UnitBasis unit)
    {
        UnitSprite unitSprite = unit.GetUnitSprite();

        if (unitSprite == null) return false;
        if (unitSprite.unitFace == null) return false;

        return true;
    }

    public void RefreshPointsInfo(IInfoble unit)
    {
        infoPanel.RefreshPointsInfo(unit);
    }

    public void SetParty()
    {
        result.Clear();
        foreach (UnitTavernCardOperator unitCard in partyPanel.GetComponentsInChildren<UnitTavernCardOperator>())
        {
            result.Add(unitCard.GetUnitID());
        }
    }

    public static int[] GetParty()
    {
        return result.ToArray();
    }
}
