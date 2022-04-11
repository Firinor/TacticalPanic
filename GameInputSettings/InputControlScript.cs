using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InputControlScript : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (InputSettings.MouseTarget != null)
            {
                //SliderLogScript SliderLogScript = InputSettings.MouseTarget.GetComponent<SliderLogScript>();
                //SliderLogScript._Slider.value += Input.mouseScrollDelta.y * InputSettings.MouseSensitivity * 100;
                //SliderLogScript.SlideTextContent();
            }
            else
            {
                _camera.orthographicSize = math.max(_camera.orthographicSize + Input.mouseScrollDelta.y * InputSettings.MouseSensitivity, 0.1f);
            }
        }

    }
}
