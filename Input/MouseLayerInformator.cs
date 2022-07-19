using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLayerInformator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private CursorType cursorType;
    [SerializeField]
    private bool mouseLayer = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(mouseLayer)
            InputMouseInformator.MouseLayer++;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseLayer)
            InputMouseInformator.MouseLayer--;
        BattleInputManager.ResetMouseDrugPosition();
    }

    public void OnMouseEnter()
    {
        if (cursorType != CursorType.Arrow)
            PlayerOperator.GetCursorManager().CursorOverlap(cursorType);
    }
    public void OnMouseExit()
    {
        if (cursorType != CursorType.Arrow)
            PlayerOperator.GetCursorManager().CursorRemove(cursorType);
    }
}
