using UnityEngine;

namespace TacticalPanicCode
{
    public class FightOperator : MonoBehaviour
    {

        [SerializeField]
        private Collider agroRadiusTriggerCollider;

        public float Cooldown = 1f;
        public float TimeToSwing = 0.15f;
        public float TimeToArcOff = 0.3f;

        private GameObject UnitTarget;
        private Vector2Int TerraTarget;

        private float currentCooldown = 0f;
        private float currentArcCooldown = 0f;
        private bool readyToAttack = true;
        private bool attackAction = false;

        private UnitOperator unit;

        private enum AttackStages { swing, arc, rollback };
        private AttackStages attackStage = AttackStages.rollback;

        private string compareTag = "";

        public void Awake()
        {
            unit = GetComponent<UnitOperator>();
            switch (gameObject.tag)
            {
                case "Player":
                    compareTag = "Enemy";
                    break;
                case "Enemy":
                    compareTag = "Player";
                    break;
                default:
                    break;
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

                currentCooldown += deltaTime;//Attack speed

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
            //if (enemy != null && enemy.gameObject.CompareTag(compareTag))
            //{
            //    enemy.GetComponent<UnitOperator>().Damage(1, Gist.Life);//Strenght to damage
            //    unit.Damage(7, Gist.Energy);
            //    audioOperator.PlaySound(UnitSounds.Attack, unit);
            //}
        }

        public void OnTriggerStay(Collider collision)
        {
            if (!readyToAttack)
                return;

            if (collision.gameObject.CompareTag(compareTag))
            {
                if (!collision.isTrigger)
                {
                    attackAction = true;
                    collision.gameObject.GetComponent<MoveOperator>().InBattle();
                }
            }
        }

        public void Deactivate()
        {
            enabled = false;
        }
    }
}
