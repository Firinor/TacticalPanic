using UnityEngine;

public class FightOperator : MonoBehaviour
{
    public float Cooldown = 1f;
    public float TimeToSwing = 0.15f;
    public float TimeToArcOff = 0.3f;
    public SpriteRenderer KickSide;

    private float currentCooldown = 0f;
    private float currentArcCooldown = 0f;
    private bool readyToAttack = true;
    private bool attackAction = false;

    private UnitOperator unit;

    private Collider2D attackArea;
    private Collider2D[] arrayColliders = new Collider2D[16];
    private enum AttackStages { swing, arc, rollback };
    private AttackStages attackStage = AttackStages.rollback;

    [SerializeField]
    private ContactFilter2D filter2D;

    private string compareTag = "";

    private AudioSourceOperator audioOperator;

    public void Start()
    {
        unit = GetComponent<UnitOperator>();
        attackArea = GetComponents<Collider2D>()[1];
        audioOperator = GetComponentInChildren<AudioSourceOperator>();

        switch (gameObject.tag)
        {
            case "Player":
                compareTag = "Enemy";
                break;
            case "Enemy":
                compareTag = "Player";
                break ;
            default:
                return;
        }
    }

    public void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        if (attackAction)
        {
            if (readyToAttack)
            {
                readyToAttack = false;
                currentCooldown = 0f;
                attackStage = AttackStages.swing;
            }

            currentCooldown += deltaTime;// * 3/100;//Attack speed

            if (attackStage == AttackStages.swing)
            {
                if (currentCooldown > TimeToSwing)
                {
                    attackStage = AttackStages.arc;
                    currentArcCooldown = 0;
                    AreaDamage();
                }
            }
            else if (attackStage == AttackStages.arc)
            {
                currentArcCooldown += deltaTime;
                if (currentArcCooldown > TimeToArcOff)
                {
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

    private void AreaDamage()
    {
        if (attackArea.OverlapCollider(filter2D, arrayColliders) > 0)
        {
            foreach (Collider2D enemy in arrayColliders)
            {
                if (enemy != null && enemy.gameObject.CompareTag(compareTag))
                {
                    enemy.GetComponent<UnitOperator>().Damage(1, Gist.Life);//Strenght to damage
                    unit.Damage(7, Gist.Energy);
                    audioOperator.PlaySound(UnitSounds.Attack, unit);
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!readyToAttack)
            return;

        if (collision.gameObject.CompareTag(compareTag))
        {
            if (!collision.isTrigger)
            {
                attackAction = true;
            }
        }
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
