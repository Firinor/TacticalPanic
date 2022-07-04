using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySquadsInformator
{
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private UnitInformator Unit;
    private List<UnitOperator> units = new List<UnitOperator>();
    [SerializeField]
    public int Count;
    [SerializeField]
    public UnitOnLevelPathInformator enemyPath;

    public UnitBasis UnitBasis
    {
        get
        {
            return Unit.unitBasis;
        }
    }

    public void AddUnitOperatorToSquad(UnitOperator unit)
    {
        if (!units.Contains(unit))
        {
            units.Add(unit);
        }
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(spawnTime);
        for(int i = 0; i < units.Count; i++)
        {
            units[i].SpawnToPoint(enemyPath);
        }
    }
}
