using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLevelOperator : MonoBehaviour, IDialog
{
    public DialogInformator Dialog
    {
        get => PlayerManager.PickedLevel.BriefingDialog;
        set { }
    }
}
