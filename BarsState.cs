using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsState : MonoBehaviour
{
    [SerializeField] 
    private Color Color;
    [SerializeField]
    private float AncorPointX;
    [SerializeField]
    private float AncorPointY;
    [SerializeField]
    private int MaxValue;
    public int CurrentValue;

    public GameObject RefFill;
    public GameObject RefSlider;
    private GameObject RefCurrentValue;
    
    void Start()
    {
        RefFill.GetComponent<Image>().color = Color;
        //RefSlider.GetComponent<Slider>().maxValue = MaxValue;
        //RefSlider.GetComponent<Slider>().value = CurrentValue;
        transform.Translate(AncorPointX, AncorPointY, 0);
        //rectTransform.localPosition.x = AncorPointX;
        // AncorPointY;
    }
}
