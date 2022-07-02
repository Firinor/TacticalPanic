using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class inBattleTileOperator : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMeshPro;
    public string text { get { return textMeshPro.text; } set { textMeshPro.text = value; } }
    [SerializeField]
    public GameObject Switch;
}
