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

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(spawnTime);
        //Instantiate();
        yield return this;
    }
}
