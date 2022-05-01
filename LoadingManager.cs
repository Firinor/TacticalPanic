using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject playerCard;
    [SerializeField]
    private GameObject[] playerParty;

    void Awake()
    {
        UnitController.Start();
        UnitInfo.InfoEvent += UnitInfo.RefreshPointsInfo;

        for (int i = 0; i < 3; i++)
        {
            var Card = Instantiate(playerCard, playerHand.transform);
            var CardStats = Card.GetComponent<CardStats>();
            CardStats.SetCardUnit(playerParty[i]);
        }
        //SetStatsOfUnits();
    }
}
