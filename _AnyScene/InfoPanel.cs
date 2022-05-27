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
    private Numerical numerical;
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
        }
    }

    public void RefreshPointsInfo(IInfo picedObject)
    {
        SetImage(picedObject);
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

public class Numerical 
{
    
}

public static class SelectedUnitsInformator
{
    private static bool debugLoadCompleted = false;

    public static ObservableCollection<IInfo> SelectedUnits { get; set; } = new ObservableCollection<IInfo>();

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