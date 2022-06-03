using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitTavernCardOperator : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UnitBasis unit;
    private UnitSprite unitSprite;
    [SerializeField]
    private Image highlighting;
    [SerializeField]
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SquadOperator.inctance.RefreshPointsInfo(unit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlighting.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighting.enabled = false;
    }

    public void SetUnit(UnitBasis unit)
    {
        this.unit = unit;
        unitSprite = unit.GetUnitSprite();
        image.sprite = unitSprite.unitFace;
    }


}
