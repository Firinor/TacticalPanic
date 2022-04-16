using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    [SerializeField]
    private int[] ManaPrice = new int[4];
    [SerializeField]
    private Text[] ManaText = new Text[4];

    public int DeployLevel = 1;

    public void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            if (ManaText[i] != null && ManaPrice[i] != 0)
            {
                ManaText[i].text = $"<color={S.ColorString[i]}>{ManaPrice[i]}</color>";
            }
            else
            {
                ManaText[i].gameObject.SetActive(false);
            }
        }
    }
}
