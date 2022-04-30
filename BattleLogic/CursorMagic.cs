using UnityEngine;
using System.Text;
using System;

public class CursorMagic : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private ContactFilter2D contactFilter2D = new ContactFilter2D();
    private RaycastHit2D[] results = new RaycastHit2D[8];

    private float[] passiveDamage;
    private float[] passiveHeal;

    public void Start()
    {
        mainCamera = GetComponent<Camera>();
        S.GetCursorMagic(out passiveDamage, out passiveHeal);
    }

    void FixedUpdate()
    {
        if (InputSettings.MouseLayer != 0)
            return;

        Vector2 _cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Ray2D ray = new Ray2D(_cursorPosition, new Vector3(0, 0, 1));
        int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0), contactFilter2D, results, mainCamera.farClipPlane);
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
            stats.Heal(passiveHeal);
        }
        else
        {
            stats.Damage(passiveDamage);
        }
    }

    private void MouseHeal(Stats stats)
    {
        MouseDamage(stats, true);
    }
}
