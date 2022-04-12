using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputSettings : MonoBehaviour
{
    public float TextScrollSensivity = 1;
    public float ZoomScrollSensivity = 1;

    public void Awake()
    {
        InputSettings.TextScrollSensivity = TextScrollSensivity;
        InputSettings.ZoomScrollSensivity = ZoomScrollSensivity;
    }
}
