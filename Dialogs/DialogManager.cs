using FirDialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class DialogManager : SinglBehaviour<DialogManager>
    {
        [SerializeField]
        private GameObject dialog;
        [SerializeField]
        private DialogOperator dialogOperator;

        void Awake()
        {
            SingletoneCheck(this);
        }

        public void StartDialog(DialogLevelOperator dialog)
        {
            StartCoroutineDialog(dialog);
        }
        public void StartDialog(DialogButtonOperator dialog)
        {
            StartCoroutineDialog(dialog);
        }

        public void StartCoroutineDialog(IDialog dialog)
        {
            instance.dialog.SetActive(true);
            instance.dialogOperator.StartCoroutineDialog(dialog.Dialog);
        }
    }
}
