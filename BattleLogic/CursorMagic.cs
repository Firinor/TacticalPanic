using UnityEngine;
using System.Text;

public class CursorMagic : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private ContactFilter2D contactFilter2D = new ContactFilter2D();
    private RaycastHit2D[] results = new RaycastHit2D[8];

    private float[] passiveDamage;
    private float[] passiveHeal;

    private float currentCooldown = 0f;
    private float currentArcCooldown = 0f;

    private StringBuilder stringBuilder = new StringBuilder("Player");

    public void Start()
    {
        S.GetCursorMagic(out passiveDamage, out passiveHeal);
    }

    void FixedUpdate()
    {
        if (InputSettings.MouseLayer != 0)
            return;

        Vector2 _cursorPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        //Ray2D ray = new Ray2D(_cursorPosition, new Vector3(0, 0, 1));
        int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0), contactFilter2D, results, camera.farClipPlane);
        if (rayCollision > 0)
        {
            for (int i = 0; i < rayCollision; i++)
            {
                if (results[i].transform != null)
                {
                    switch (results[i].transform.tag)
                    {
                        case "Enemy":
                            MouseDamage(results[i].transform.GetComponent<Stats>(), Time.fixedDeltaTime);
                            break;
                        case "Player":
                            MouseHeal(results[i].transform.GetComponent<Stats>(), Time.fixedDeltaTime);
                            break;
                    }
                }
            }
        }
    }

    private void MouseDamage(Stats stats, float deltaTime, bool heal = false)
    {
        if (heal)
        {
            stats.Heal(stringBuilder, passiveHeal, deltaTime);
        }
        else
        {
            stats.Damage(stringBuilder, passiveDamage, deltaTime);
        }
        

    }

    private void MouseHeal(Stats stats, float deltaTime)
    {
        MouseDamage(stats, deltaTime, true);
    }
}
