using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLayerControllingScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private CursorType cursorType;
    [SerializeField]
    private bool mouseLayer = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(mouseLayer)
            InputSettings.MouseLayer++;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseLayer)
            InputSettings.MouseLayer--;
        InputControlScript.ResetMouseDrugPosition();
    }

    public void OnMouseEnter()
    {
        if (cursorType != CursorType.Arrow)
            S.GetCursorManager().CursorOverlap(cursorType);
    }
    public void OnMouseExit()
    {
        if (cursorType != CursorType.Arrow)
            S.GetCursorManager().CursorRemove(cursorType);
    }
}
