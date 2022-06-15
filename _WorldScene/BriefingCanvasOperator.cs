using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCanvasOperator : SinglBehaviour<BriefingCanvasOperator>
{
    [SerializeField]
    private GameObject partyPanel;

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }
}
