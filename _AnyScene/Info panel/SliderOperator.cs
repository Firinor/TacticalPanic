using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class SliderOperator : MonoBehaviour
    {
        [SerializeField]
        private GameObject textContent;
        private RectTransform textTransform;
        private RectTransform parentTransform;

        public Slider Slider;

        private float viewHeight;

        public void Start()
        {
            textTransform = textContent.GetComponent<RectTransform>();
            parentTransform = textTransform.parent.GetComponent<RectTransform>();
            viewHeight = parentTransform.rect.height;
            Slider.maxValue = textContent.GetComponent<Text>().preferredHeight;

            if (Slider.maxValue > viewHeight)
                Slider.maxValue -= viewHeight;

            Slider.value = 0;

        }
        public void ScrollTextContent(Vector2 vector2)
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                Slider.value -= Input.mouseScrollDelta.y * InputMouseInformator.TextScrollSensivity;
            }
            else
            {
                Slider.value = textTransform.anchoredPosition.y;
            }
        }

        public void SlideTextContent()
        {
            textTransform.anchoredPosition = new Vector2(0, Slider.value);
        }
    }
}
