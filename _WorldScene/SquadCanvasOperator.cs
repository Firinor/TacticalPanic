using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCanvasOperator : SinglBehaviour<SquadCanvasOperator>
{
    [Header("Main")]
    [SerializeField]
    private InfoPanelOperator infoPanel;

    [Header("Drag & Drop settings")]
    [SerializeField]
    private GameObject tavernPanel;
    [SerializeField]
    private GameObject partyPanel;
    [SerializeField]
    private UnitTavernCardOperator tawernUnitCardShadow;
    [SerializeField]
    private UnitTavernCardOperator partyUnitCardShadow;

    public void SetParentToAllUnits()
    {
        foreach(UnitBasis unitCard in UnitsCardManager.unitCards.Keys)
        {
            GameObject parent = unitCard.inParty ? partyPanel: tavernPanel;
            UnitsCardManager.unitCards[unitCard].transform.SetParent(parent.transform);
        }
    }

    public void RefreshPointsInfo(IInfoble unit)
    {
        infoPanel.RefreshPointsInfo(unit);
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
        if (instance == null)
            return;
        UnitsCardManager.SetParty(instance.partyPanel.GetComponentsInChildren<UnitTavernCardOperator>());
        SaveManager.Save(S.Account);
    }

    internal static void CardOnDrop(UnitTavernCardOperator cardOperator, GameObject parent)
    {
        cardOperator.SetParent(parent.transform);
    }

    internal static Transform GetTavernTransform()
    {
        return instance.tavernPanel.transform;
    }

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }
}
