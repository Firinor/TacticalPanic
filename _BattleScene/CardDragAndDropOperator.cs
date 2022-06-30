using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragAndDropOperator : MonoBehaviour, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    private new Camera camera;
    [SerializeField]
    private float cardPositionOffset;
    private float cardSiblingOffset;
    [SerializeField]
    private float smoothness = 0.25f;

    private GameObject cardUnit;
    private Unit statsUnit;

    private bool dragCard = false;
    private bool cursorOnCard = false;

    public void Start()
    {
        cardUnit = GetComponent<CardStats>().GetUnitPrefab();
        statsUnit = cardUnit.GetComponent<Unit>();
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
            WantToDeploy(false);
        }
    }

    public void FixedUpdate()
    {
        if (!dragCard && transform.position.y != (cursorOnCard? cardPositionOffset: 0))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3( transform.position.x, cursorOnCard ? cardPositionOffset : 0, transform.position.z), smoothness);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            dragCard = true;
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
            if (transform.position.y != cardPositionOffset)
                transform.position = new Vector3(transform.position.x, cardPositionOffset, transform.position.z);

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(camera.transform.position, Vector3.forward);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            //if (hit.point )
            Vector3 pos = hit.point;
            //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            //pos.z = 0;
            cardUnit.transform.localPosition = pos;
            if (statsUnit.CheckTerms())
            {
                statsUnit.SetVisualState(VisualOfUnit.Haziness);
            }
            else
            {
                statsUnit.SetVisualState(VisualOfUnit.Grayness);
            }
            CheckPosition(eventData.position.x);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        WantToDeploy(InputMouseInformator.MouseLayer == 0);
    }
    
    public void WantToDeploy(bool wantToDeploy = false)
    {
        dragCard = false;

        if (wantToDeploy)
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
        statsUnit.SetVisualState(VisualOfUnit.Off);
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
        if(transform.GetSiblingIndex() != NewIndex)
            transform.SetSiblingIndex(NewIndex);
    }
}
