using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class MainMenuInformator : SinglBehaviour<MainMenuInformator>
    {
        [SerializeField]
        private GameObject baner;
        [SerializeField]
        private GameObject credits;
        [SerializeField]
        private GameObject saves;
        private GameObject options;

        void Awake()
        {
            SingletoneCheck(this);
        }

        public static GameObject GetBaner()
        {
            return instance.baner;
        }

        public static GameObject GetCredits()
        {
            return instance.credits;
        }

        public static GameObject GetSaves()
        {
            return instance.saves;
        }
    }
}
