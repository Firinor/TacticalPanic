using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DropHandlerSystem { OnTavern, BattleField }

public class OnDropInformator : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private DropHandlerSystem system;
    [SerializeField]
    private GameObject keyObject;

    public void OnDrop(PointerEventData eventData)
    {
        switch (system)
        {
            case DropHandlerSystem.OnTavern:
                UnitTavernCardOperator pointerDrag = eventData.pointerDrag.GetComponent<UnitTavernCardOperator>();
                if(pointerDrag != null)
                    SquadOperator.CardOnDrop(pointerDrag, keyObject);
                break;
        }
    }
}
