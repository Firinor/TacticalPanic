using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTextScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _mouseTarget;

    void OnMouseOver()
    {
        InputSettings.MouseTarget = _mouseTarget;
    }
}
