using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TacticalPanicCode
{
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

        [SerializeField]
        private static float maxRayDistance = 100f;
        [SerializeField]
        private LayerMask GroundLayerMask;

        private GameObject cardUnit;
        private UnitOperator unitOperator;

        private bool dragCard = false;
        private bool cursorOnCard = false;

        public void Start()
        {
            cardUnit = GetComponent<UnitCardStats>().GetUnitPrefab();
            unitOperator = cardUnit.GetComponent<UnitOperator>();
            camera = Camera.main;
            transform.Find("Name").GetComponent<Text>().text = unitOperator.GetName();
            cardSiblingOffset = -(gameObject.GetComponent<RectTransform>().rect.width
                + gameObject.GetComponentInParent<HorizontalLayoutGroup>().spacing) / 2;
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
            if (!dragCard && transform.position.y != (cursorOnCard ? cardPositionOffset : 0))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cursorOnCard ? cardPositionOffset : 0, transform.position.z), smoothness);
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
                Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, GroundLayerMask);

                //if (hit.point )
                Vector3 pos = hit.point;
                //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
                //pos.z = 0;
                cardUnit.transform.localPosition = pos;
                if (unitOperator.CheckTerms())
                {
                    unitOperator.SetVisualState(VisualOfUnit.Haziness);
                }
                else
                {
                    unitOperator.SetVisualState(VisualOfUnit.Grayness);
                }
                //CheckPosition(eventData.position.x);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            WantToDeploy(InputMouseInformator.MouseLayer == 0);
        }

        public void WantToDeploy(bool wantToDeploy = false)
        {
            dragCard = false;

            if (!wantToDeploy || !unitOperator.CheckTermsAndDeploy())
                UnitOff();
            else
                Destroy(gameObject);
        }

        private void UnitOff()
        {
            unitOperator.SetVisualState(VisualOfUnit.Off);
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
            if (transform.GetSiblingIndex() != NewIndex)
                transform.SetSiblingIndex(NewIndex);
        }
    }
}
