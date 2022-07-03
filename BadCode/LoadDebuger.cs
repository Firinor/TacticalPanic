using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDebuger : MonoBehaviour
{
    [Range(0, 2)]
    public int Account = 0;
    public LevelInformator level;

    void Awake()
    {
        if(SaveManager.PlayerAccount() == -1)
        {
            SaveManager.Load(Account);
            PlayerManager.OnLoad();
        }
        if(level != null)
        {
            PlayerManager.PickedLevel = level;
        }
        Destroy(gameObject);
    }
}
