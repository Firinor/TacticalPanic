using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public static class UnitController
{
    private static bool Loadcompleted = false;

    public static ObservableCollection<GameObject> SelectedUnits { get; set; } = new ObservableCollection<GameObject>();

    public static void Start()
    {
        if (!Loadcompleted) 
        {
            SelectedUnits.CollectionChanged += ShowUnitInfo;
            Loadcompleted = true;
            SelectedUnits.Clear();
        }
    }

    public static void ShowUnitInfo(object sendler, NotifyCollectionChangedEventArgs e)
    {
        UnitInfo.RefreshInfoPanel();
    }
}
