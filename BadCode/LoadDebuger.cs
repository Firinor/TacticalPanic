using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDebuger : MonoBehaviour
{
    [Range(0, 2)]
    public int Account = 0;
    public UnitsCardManager squadManager;

    void Awake()
    {
        if(SaveManager.PlayerAccount() == -1)
        {
            SaveManager.Load(Account);
            S.OnLoad();
        }
        Destroy(gameObject);
    }
}
