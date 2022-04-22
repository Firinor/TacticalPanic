using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragAndDrop : MonoBehaviour, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _defaultParent;
    private Vector3 _offset;
    [SerializeField]
    private float _cardPositionOffset;
    [SerializeField]
    private Vector3 _positionUp;
    private Vector3 _positionStandart;
    [SerializeField]
    private float _smoothness = 0.25f;

    public GameObject _cardUnit;
    private Stats statsUnit;

    //В FixedUpdate используется процедура Vector3.Lerp( , которая при старте сцены сразу уводит карту в нулевую точку.
    //После присвоения _offset ошибка пропадает.
    private bool _showCardInfoDebug = false;

    private bool _dragCard = false;
    private bool _cursorOnCard = false;

    public void Start()
    {
        _cardUnit = GetComponent<CardStats>().cardUnit;
        statsUnit = _cardUnit.GetComponent<Stats>();
        _camera = Camera.main;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && _dragCard)
        {
            _dragCard = false;
            WontToDeploy(false);
        }
    }

    public void FixedUpdate()
    {
        if (_showCardInfoDebug && !_dragCard && transform.position != _offset)
            transform.position = Vector3.Lerp(transform.position, _offset, _smoothness);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _dragCard = true;
            statsUnit.SetVisualState(Stats.Visual.Haziness);
            //statsUnit.SetVisualState(Stats.Visual.Grayness);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_dragCard && eventData.button == PointerEventData.InputButton.Left)
        {
            //Vector3 pos = _camera.ScreenToWorldPoint(eventData.position);
            Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            _cardUnit.transform.localPosition = pos;
            CheckPosition(eventData.position.x);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        WontToDeploy(InputSettings.MouseLayer == 0);
    }
    
    public void WontToDeploy(bool wontToDeploy = false)
    {
        _dragCard = false;

        if (wontToDeploy && statsUnit.CheckTermsToDeploy())
        {
            statsUnit.Deploy();
            Destroy(gameObject);
        }
        else
        {
            statsUnit.SetVisualState(Stats.Visual.Off);
            if (!_cursorOnCard)
            {
                _positionStandart.x = _offset.x;
                _positionStandart.y = 0;
                _offset = _positionStandart;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cursorOnCard = true;
        _showCardInfoDebug = true;
        _positionStandart = transform.position;
        _positionUp.x = transform.position.x;
        _offset = _positionUp;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cursorOnCard = false;
        if (!_dragCard)
        {
            _positionStandart.y = 0;
            _offset = _positionStandart;
        }
    }

    void CheckPosition(float posX)
    {
        posX += _cardPositionOffset;
        int ChildCount = _defaultParent.childCount;
        int NewIndex = ChildCount;

        for (int i = 0; i < ChildCount; i++)
        {
            if (posX <= _defaultParent.GetChild(i).position.x)
            {
                _offset.x = _defaultParent.GetChild(i).position.x;
                NewIndex = i;
                break;
            }
        }

        transform.SetSiblingIndex(NewIndex);
        transform.position = _offset;
    }
}
