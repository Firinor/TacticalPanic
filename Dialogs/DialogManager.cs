using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SinglBehaviour<DialogManager>
{
    [SerializeField]
    private GameObject dialog;
    [SerializeField]
    private DialogOperator dialogOperator;

    void Awake()
    {
        SingletoneCheck<DialogManager>(this);
    }

    public static void StartDialog(DialogButtonOperator button)
    {
        instance.dialog.SetActive(true);
        instance.dialogOperator.StartCoroutineDialog(button.Dialog);
    }
}
