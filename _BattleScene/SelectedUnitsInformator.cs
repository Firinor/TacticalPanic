using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public static class SelectedUnitsInformator
{
    private static bool loadCompleted = false;

    public static ObservableCollection<GameObject> SelectedUnits { get; set; } = new ObservableCollection<GameObject>();

    public static void Start()
    {
        if (!loadCompleted) 
        {
            SelectedUnits.CollectionChanged += ShowUnitInfo;
            loadCompleted = true;
            SelectedUnits.Clear();
        }
    }

    public static void ShowUnitInfo(object sendler, NotifyCollectionChangedEventArgs e)
    {
        UnitInfoPanelOperator.RefreshInfoPanel();
    }
}
