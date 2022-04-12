using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLayerControllingScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        InputSettings.MouseLayer++;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputSettings.MouseLayer--;
    }
}
