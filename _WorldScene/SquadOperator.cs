using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadOperator : MonoBehaviour
{
    [SerializeField]
    private InfoPanel infoPanel;
    [SerializeField]
    private GameObject tavernPanel;
    [SerializeField]
    private GameObject mercenaryCardPrefab;
    public static SquadOperator inctance { get; private set; }

    public void Awake()
    {
        Singltone();

        if (tavernPanel.transform.childCount <= 1)//TODO == 0
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
        if (inctance != null)
            Destroy(gameObject);
        inctance = this;
    }

    private bool ComplianceRequirement(UnitBasis unit)
    {
        UnitSprite unitSprite = unit.GetUnitSprite();
        if (unitSprite == null) return false;
        if (unitSprite.unitFace == null) return false;

        return true;
    }

    public void UnitToParty(Unit unit)
    {

    }

    public void UnitToTavern(Unit unit)
    {

    }

    public void RefreshPointsInfo(IGInfo unit)
    {
        infoPanel.RefreshPointsInfo(unit);
    }
}
