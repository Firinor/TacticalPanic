using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLogScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _textContent;
    private RectTransform _textTransform;
    private RectTransform _parentTransform;
    
    public Slider _Slider;

    private float _viewHeight;

    public void Start()
    {
        _textTransform = _textContent.GetComponent<RectTransform>();
        _parentTransform = _textTransform.parent.GetComponent<RectTransform>();
        _viewHeight = _parentTransform.rect.height;
        _Slider.maxValue = _textContent.GetComponent<Text>().preferredHeight;

        if(_Slider.maxValue > _viewHeight)
            _Slider.maxValue -= _viewHeight;

        _Slider.value = 0;

    }
    public void SlideTextContent()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _Slider.value += Input.mouseScrollDelta.y * InputSettings.MouseSensitivity;
        }
        _textTransform.anchoredPosition = new Vector2(0, _Slider.value);
    }
}
