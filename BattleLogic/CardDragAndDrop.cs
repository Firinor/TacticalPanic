using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragAndDrop : MonoBehaviour, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    private new Camera camera;
    [SerializeField]
    private float cardPositionOffset;
    private float cardSiblingOffset;
    private float currenPositionY;
    [SerializeField]
    private float smoothness = 0.25f;

    private GameObject cardUnit;
    private Stats statsUnit;

    private bool dragCard = false;
    private bool cursorOnCard = false;

    public void Start()
    {
        cardUnit = GetComponent<CardStats>().cardUnit;
        statsUnit = cardUnit.GetComponent<Stats>();
        camera = Camera.main;
        gameObject.transform.Find("Name").GetComponent<Text>().text = statsUnit.GetName();
        cardSiblingOffset = -(gameObject.GetComponent<RectTransform>().rect.width 
            + gameObject.GetComponentInParent<HorizontalLayoutGroup>().spacing)/2;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && dragCard)
        {
            dragCard = false;
            WontToDeploy(false);
        }

        if(currenPositionY != transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, currenPositionY, transform.position.z);
        }
    }

    public void FixedUpdate()
    {
        if (!dragCard && transform.position.y != (cursorOnCard? cardPositionOffset: 0))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3( transform.position.x, cursorOnCard ? cardPositionOffset : 0, transform.position.z), smoothness);
            currenPositionY = transform.position.y;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            dragCard = true;
            //statsUnit.SetVisualState(Stats.Visual.Haziness);
            statsUnit.SetVisualState(Stats.Visual.Grayness);
        }
        else
        {
            eventData.pointerDrag = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragCard && eventData.button == PointerEventData.InputButton.Left)
        {
            //Vector3 pos = _camera.ScreenToWorldPoint(eventData.position);
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            cardUnit.transform.localPosition = pos;
            CheckPosition(eventData.position.x);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        WontToDeploy(InputSettings.MouseLayer == 0);
    }
    
    public void WontToDeploy(bool wontToDeploy = false)
    {
        dragCard = false;

        if (wontToDeploy)
        {
            if (statsUnit.CheckTermsAndDeploy())
            {
                Destroy(gameObject);
            }
            else
            {
                UnitOff();
            }
        }
        else
        {
            UnitOff();
        }
    }

    private void UnitOff()
    {
        statsUnit.SetVisualState(Stats.Visual.Off);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorOnCard = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorOnCard = false;
    }

    void CheckPosition(float posX)
    {
        Transform defaultParent = gameObject.transform.parent;
        posX += cardSiblingOffset;
        int ChildCount = defaultParent.childCount;
        int NewIndex = ChildCount;

        for (int i = 0; i < ChildCount; i++)
        {
            if (posX <= defaultParent.GetChild(i).position.x)
            {
                NewIndex = i;
                break;
            }
        }
        currenPositionY = transform.position.y;
        transform.SetSiblingIndex(NewIndex);
    }
}
