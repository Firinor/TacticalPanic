using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public delegate void UnitPicDelegate();

public static class UnitController
{
    public static event UnitPicDelegate UnitPic;

    public static ObservableCollection<GameObject> SelectedUnits { get; set; } = new ObservableCollection<GameObject>();

    //public static ObservableCollection<GameObject> SelectedUnits
    //{
    //    get { return selectedUnits; }
    //    set
    //    {
    //        selectedUnits = value;
    //        UnitPic?.Invoke();
    //    }
    //}

    public static void ShowUnitInfo(object sendler, NotifyCollectionChangedEventArgs e)
    {
        if (SelectedUnits != null)
        {
            Debug.Log(SelectedUnits[0].name);
        }
        else
        {
            Debug.Log("Show log info!");
        }
    }

}
