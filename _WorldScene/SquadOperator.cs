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

    public void Awake()
    {
        if(tavernPanel.transform.childCount == 0) 
        { 
            foreach(var value in DB.Units)
            {
                if(value.GetUnitSprite() != null && value.GetUnitSprite().unitFace != null)
                { 
                    var mercenaryCard = Instantiate(mercenaryCardPrefab, tavernPanel.transform);
                    mercenaryCard.GetComponent<UnitTavernCardOperator>().SetUnit(value);
                }
            }
        }
    }

    public void UnitToParty(Unit unit)
    {

    }

    public void UnitToTavern(Unit unit)
    {

    }

    public void InfoOfUnit(Unit unit)
    {

    }
}
