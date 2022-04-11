using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputSettings : MonoBehaviour
{
    public float MouseSensitivity;

    public void Awake()
    {
        InputSettings.MouseSensitivity = MouseSensitivity;
    }
}
