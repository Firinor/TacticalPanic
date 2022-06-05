using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadOperator : MonoBehaviour
{
    public static SquadOperator instance { get; private set; }

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
        Singltone();

        if (tavernPanel.transform.childCount <= 1)
        {
            foreach (UnitBasis unit in DB.Units)
            {
                if (!ComplianceRequirement(unit))
                    continue;

                GameObject mercenaryCard = Instantiate(mercenaryCardPrefab, tavernPanel.transform);
                mercenaryCard.GetComponent<UnitTavernCardOperator>().SetUnit(unit);
            }
        }
    }

    private void Singltone()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
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
    }

    internal static void CardOnDrop(GameObject cardOperator, GameObject parent)
    {
        cardOperator.GetComponent<UnitTavernCardOperator>().SetParent(parent.transform);
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
}
