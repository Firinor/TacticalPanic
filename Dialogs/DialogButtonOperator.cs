using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class DialogButtonOperator : MonoBehaviour, IDialog
    {
        [SerializeField]
        private DialogInformator dialog;

        public DialogInformator Dialog
        {
            get => dialog;
            set { }
        }
    }
}
