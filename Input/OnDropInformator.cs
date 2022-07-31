using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TacticalPanicCode
{
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
                    UnitCardOperator pointerDrag = eventData.pointerDrag.GetComponent<UnitCardOperator>();
                    if (pointerDrag != null)
                        SquadCanvasOperator.CardOnDrop(pointerDrag, keyObject);
                    break;
            }
        }
    }
}
