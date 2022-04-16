using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CursorMagic : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private ContactFilter2D _contactFilter2D = new ContactFilter2D();
    private RaycastHit2D[] results = new RaycastHit2D[8];

    void FixedUpdate()
    {
        if (InputSettings.MouseLayer != 0)
            return;

        Vector2 _cursorPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        //Ray2D ray = new Ray2D(_cursorPosition, new Vector3(0, 0, 1));
        int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0), _contactFilter2D, results, _camera.farClipPlane);
        if (rayCollision > 0)
        {
            for (int i = 0; i < rayCollision; i++)
            {
                if (results[i].transform != null)
                {
                    switch (results[i].transform.tag)
                    {
                        case "Enemy":
                            MouseDamage(results[i].transform.GetComponent<Stats>());
                            break;
                        case "Player":
                            MouseHeal(results[i].transform.GetComponent<Stats>());
                            break;
                    }
                }
            }
        }
    }

    private void MouseDamage(Stats stats, bool heal = false)
    {
        if (heal)
        {
            stats.Damage(S.HealPointPower[0], Stats.Points.HP);
            stats.Damage(S.HealPointPower[1], Stats.Points.MP);
            stats.Damage(S.HealPointPower[2], Stats.Points.CP);
            stats.Damage(S.HealPointPower[3], Stats.Points.SP);
        }
        else
        {
            stats.Damage(S.DestroyPointPower[0], Stats.Points.HP);
            stats.Damage(S.DestroyPointPower[1], Stats.Points.MP);
            stats.Damage(S.DestroyPointPower[2], Stats.Points.CP);
            stats.Damage(S.DestroyPointPower[3], Stats.Points.SP);
        }

    }

    private void MouseHeal(Stats stats)
    {
        MouseDamage(stats, true);
    }
}
