using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicCastButtonOperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private MagicInformator magic;
    private int[] manaCost = new int[PlayerOperator.GistsCount];

    [SerializeField]
    private Image reloadImage;
    [SerializeField]
    private Text manaCostText;
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject manaCostInfoGroup;
    [SerializeField]
    private GameObject[] manaCostInfo = new GameObject[PlayerOperator.GistsCount];
    private Text[] manaCostInfoText = new Text[PlayerOperator.GistsCount];

    void Awake()
    {
        GetManaCostOfMagic();
        GetManaCostInfoText();
    }
    
    private void GetManaCostInfoText()
    {
        for (int i = 0; i < PlayerOperator.GistsCount; i++)
        {
            if (manaCost[i] == 0) 
            {
                manaCostInfo[i].SetActive(false);
            }
            else
            {
                manaCostInfoText[i] = manaCostInfo[i].GetComponentInChildren<Text>();
                manaCostInfoText[i].text = manaCost[i].ToString();
            }
        }
    }
    private void GetManaCostOfMagic()
    {
        for (int i = 0; i < PlayerOperator.GistsCount; i++)
        {
            manaCost[i] = MagicInformator.GetManaCostInfo(i);
        }
    }

    public void ButtomClick()
    {
        if(PlayerOperator.GetCurrentMana(Gist.Magic) >= 5)
        {
            PlayerOperator.DrawMana(new int[] { 0, 5, 0, 0 });
        }
    }

    public void Update()
    {
        float[] mana = PlayerOperator.GetAllCurrentMana();
        int mostNeeded = 0;
        float mostPercentage = 0;
        for (int i = 0; i < PlayerOperator.GistsCount; i++)
        {
            float percentage = 1 - mana[i] / manaCost[i];
            if(percentage > mostPercentage)
            {
                mostNeeded = i;
                mostPercentage = percentage;
            }
        }
        bool EnoughMana = mostPercentage == 0;
        manaCostText.enabled = !EnoughMana;
        if (!EnoughMana)
        {
            reloadImage.color = PlayerOperator.GetManaColor(mostNeeded);
            reloadImage.fillAmount = mostPercentage;
            manaCostText.text = manaCost[mostNeeded].ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        manaCostInfoGroup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manaCostInfoGroup.SetActive(false);
    }
}
