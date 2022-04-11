using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public float Cooldown = 1f;
    public float TimeToSwing = 0.15f;
    public float TimeToArcOff = 0.3f;
    public SpriteRenderer KickSide;

    private float currentCooldown = 0f;
    private float currentArcCooldown = 0f;
    private bool readyToAttack = true;
    private bool attackAction = false;

    private Stats _stats;

    private Collider2D attackArea;
    private Collider2D[] arrayColliders = new Collider2D[16];
    private enum AttackStages { swing, arc, rollback };
    private AttackStages attackStage = AttackStages.rollback;

    [SerializeField]
    private ContactFilter2D filter2D;

    private string _compareTag = "";

    void Start()
    {
        _stats = GetComponent<Stats>();
        attackArea = GetComponents<Collider2D>()[1];

        if (gameObject.tag == "Player")
        {
            _compareTag = "Enemy";
        }
        else
        {
            _compareTag = "Player";
        }

        DisabledKick();
    }

    public void DisabledKick()
    {
        KickSide.enabled = false;
    }

    void FixedUpdate()
    {
        if (attackAction)
        {
            if (readyToAttack)
            {
                readyToAttack = false;
                currentCooldown = 0f;
                attackStage = AttackStages.swing;
            }

            currentCooldown += Time.fixedDeltaTime * _stats.CurrentAttackSpeed/100;

            if (attackStage == AttackStages.swing)
            {
                if (currentCooldown > TimeToSwing)
                {
                    KickSide.enabled = true;
                    attackStage = AttackStages.arc;
                    currentArcCooldown = 0;

                    if (attackArea.OverlapCollider(filter2D, arrayColliders) > 0)
                    {
                        foreach (Collider2D enemy in arrayColliders)
                        {
                            if (enemy != null && enemy.gameObject.CompareTag(_compareTag))
                            {
                                enemy.GetComponent<Stats>().Damage(_stats.Strenght, Stats.Points.HP);
                                _stats.Damage(7, Stats.Points.CP);
                            }
                        }
                    }
                }
            }
            else if (attackStage == AttackStages.arc)
            {
                currentArcCooldown += Time.fixedDeltaTime;
                if (currentArcCooldown > TimeToArcOff)
                {
                    KickSide.enabled = false;
                    attackStage = AttackStages.rollback;
                }
            }
            else if (attackStage == AttackStages.rollback)
            {
                if (currentCooldown >= Cooldown)
                {
                    readyToAttack = true;
                    attackAction = false;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!readyToAttack)
            return;

        if (collision.gameObject.CompareTag(_compareTag))
        {
            if (collision.isTrigger)
            {

            }
            else
            {
                attackAction = true;
            }
        }
    }

    public void Deactivate()
    {
        DisabledKick();
        enabled = false;
    }
}

