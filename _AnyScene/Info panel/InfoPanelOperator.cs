using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class InfoPanelOperator : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private GistInfoOperator[] parameters;
        [SerializeField]
        private Text description;

        private IInfoble picedObject;

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

        public void RefreshPointsInfo(IInfoble picedObject)
        {
            if (this.picedObject == picedObject)
                return;

            this.picedObject = picedObject;

            SetImage(picedObject);
            SetDescription(picedObject);
            SetNumerical(picedObject);
        }

        private void SetImage(IInfoble picedObject)
        {
            if (!image.enabled)
                image.enabled = true;

            image.sprite = picedObject.SpriteInfo;
        }

        private void SetDescription(IInfoble picedObject)
        {
            description.text = picedObject.GetTextInfo();
        }

        private void SetNumerical(IInfoble picedObject)
        {
            DisableAllCollum();
            foreach (GistBasis gist in picedObject.GistBasis)
            {
                parameters[(int)gist.gist].SetNumerical(gist);
            }
        }

        private void DisableAllCollum()
        {
            foreach (var collum in parameters)
            {
                collum.SetActive(false);
            }
        }
    }

    public static class SelectedUnitsInformator
    {
        private static bool debugLoadCompleted = false;

        public static ObservableCollection<UnitOperator> SelectedUnits { get; set; } = new ObservableCollection<UnitOperator>();

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
            //UnitInfoPanelOperator.RefreshInfoPanel();
        }
    }
}