using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour// SinglBehaviour<EnemyManager>
{
    public static Dictionary<UnitOperator, EnemySquadsInformator> enemies { get; private set; } 
        = new Dictionary<UnitOperator, EnemySquadsInformator>();

    public static void AddEnemyToDictionary(UnitOperator unit, EnemySquadsInformator squad)
    {
        enemies.Add(unit, squad);
        squad.AddUnitOperatorToSquad(unit);
    }
}
