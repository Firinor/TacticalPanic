using FirDialogSystem;
using UnityEngine;

namespace TacticalPanicCode
{
    public class DialogLevelOperator : MonoBehaviour, IDialog
    {
        public DialogInformator Dialog
        {
            get => PlayerManager.PickedLevel.BriefingDialog;
            set { }
        }
    }
}
