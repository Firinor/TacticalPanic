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
    private Vector3 offset;
    [SerializeField]
    private float cardPositionOffset;
    [SerializeField]
    private Vector3 positionUp;
    private Vector3 positionStandart;
    [SerializeField]
    private float smoothness = 0.25f;

    private GameObject cardUnit;
    private Stats statsUnit;

    //В FixedUpdate используется процедура Vector3.Lerp( , которая при старте сцены сразу уводит карту в нулевую точку.
    //После присвоения offset ошибка пропадает.
    private bool showCardInfoDebug = false;

    private bool dragCard = false;
    private bool cursorOnCard = false;

    public void Start()
    {
        cardUnit = GetComponent<CardStats>().cardUnit;
        statsUnit = cardUnit.GetComponent<Stats>();
        camera = Camera.main;
        gameObject.transform.Find("Name").GetComponent<Text>().text = statsUnit.GetName();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && dragCard)
        {
            dragCard = false;
            WontToDeploy(false);
        }
    }

    public void FixedUpdate()
    {
        if (showCardInfoDebug && !dragCard && transform.position != offset)
            transform.position = Vector3.Lerp(transform.position, offset, smoothness);
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
        if (!cursorOnCard)
        {
            positionStandart.x = offset.x;
            positionStandart.y = 0;
            offset = positionStandart;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorOnCard = true;
        showCardInfoDebug = true;
        positionStandart = transform.position;
        positionUp.x = transform.position.x;
        offset = positionUp;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorOnCard = false;
        if (!dragCard)
        {
            positionStandart.y = 0;
            offset = positionStandart;
        }
    }

    void CheckPosition(float posX)
    {
        Transform defaultParent = gameObject.transform.parent;
        posX += cardPositionOffset;
        int ChildCount = defaultParent.childCount;
        int NewIndex = ChildCount;

        for (int i = 0; i < ChildCount; i++)
        {
            if (posX <= defaultParent.GetChild(i).position.x)
            {
                offset.x = defaultParent.GetChild(i).position.x;
                NewIndex = i;
                break;
            }
        }

        transform.SetSiblingIndex(NewIndex);
        transform.position = offset;
    }
}
