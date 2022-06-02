using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text[,] parameters = new Text[4, 4];
    [SerializeField]
    private Text description;

    private IInfo picedObject;

    private void Awake()
    {
        SelectedUnitsInformator.SelectedUnits.CollectionChanged += ShowUnitInfo;
    }

    public void ShowUnitInfo(object sendler, NotifyCollectionChangedEventArgs e)
    {
        RefreshInfoPanel();
    }

    public void RefreshInfoPanel()
    {
        bool Pic = SelectedUnitsInformator.SelectedUnits.Count > 0;
        if (Pic)
        {
            picedObject = SelectedUnitsInformator.SelectedUnits[0];
            SetImage(picedObject);
            SetDescription(picedObject);
        }
        SetNumerical(picedObject);
    }

    private void SetDescription(IInfo picedObject)
    {
        description.text = picedObject.GetTextInfo();
    }

    private void SetNumerical(IInfo picedObject)
    {
        for (int i = 0; i < 4; i++)
        {
            parameters[i,0].text = picedObject.Elements[i]. ToString();
        }
    }

    public void RefreshPointsInfo(IInfo picedObject)
    {
        SetImage(picedObject);
        SetDescription(picedObject);
        SetNumerical(picedObject);
    }

    private void SetImage(IInfo picedObject)
    {
        if (this.picedObject == picedObject)
        {
            return;
        }
        image.sprite = picedObject.SpriteInfo;
    }
}

public static class SelectedUnitsInformator
{
    private static bool debugLoadCompleted = false;

    public static ObservableCollection<Unit> SelectedUnits { get; set; } = new ObservableCollection<Unit>();

    public static void Start()
    {
        if (!debugLoadCompleted)
        {
            SelectedUnits.Clear();
            SelectedUnits.CollectionChanged += ShowUnitInfo;
            debugLoadCompleted = true;
        }
    }

    public static void ShowUnitInfo(object sendler, NotifyCollectionChangedEventArgs e)
    {
        UnitInfoPanelOperator.RefreshInfoPanel();
    }
}