using UnityEngine;
using Unity.Mathematics;

namespace TacticalPanicCode
{
    public partial class UnitOperator : MonoBehaviour, IInfoble
    {
        private void Death()
        {
            unitSoundOperator.PlaySound(UnitSounds.Death);
            SetUnitActivity(false);
            AnimDeath();
        }
        private void AnimDeath()
        {
            animationOperator.AnimDeath();
        }
        public void DeathAnimationEnds()
        {
            Destroy(gameObject);
        }
        public void Prepare(ConflictSide side)
        {
            SetConflictSide(side);
            SetVisualState(VisualOfUnit.Off);
        }
        public void Deploy(Vector3 spawnPoint)
        {
            gameObject.transform.position = spawnPoint;
            Deploy();
        }
        public void Deploy()
        {
            animationOperator.Deploy();
            SetVisualState(VisualOfUnit.Normal);
        }

        public bool CheckTermsAndDeploy()
        {
            bool success = false;

            if (CheckTerms())
            {
                PlayerOperator.DrawMana(GetManaPrice());
                success = true;
                Deploy();
            }

            return success;
        }

        public void SpawnToPoint(UnitOnLevelPathInformator enemyPath)
        {
            Deploy(enemyPath.GetSpawnPoint());
            unitBehaviour.CreateBehaviour(enemyPath);
        }

        public bool CheckTerms()
        {
            bool Check = true;

            for (int i = 0; i < GistBasis.Length; i++)
            {
                int gistManaPrise = GistBasis[i].manaPrice;
                if (gistManaPrise > 0 && PlayerOperator.GetCurrentMana(GistBasis[i].gist) < gistManaPrise)
                {
                    Check = false;
                    break;
                }
            }

            return Check;
        }

        public string GetElementColorString(int index)
        {
            return GistColorInformator.ColorByIndex(index).TextColor;
        }
        public string GetElementColorString(Gist gist)
        {
            return GetElementColorString(PlayerOperator.GetIndexByGist(gist));
        }
        public int GetElementManaPrice(int index)
        {
            return GistBasis[index].manaPrice;
        }
        public int GetElementManaPrice(Gist gist)
        {
            return GetElementManaPrice(PlayerOperator.GetIndexByGist(gist));
        }
        public int[] GetManaPrice()
        {
            int[] price = new int[GistBasis.Length];
            for (int i = 0; i < GistBasis.Length; i++)
                price[i] = GistBasis[i].manaPrice;
            return price;
        }
        public void Damage(float[] damage)
        {
            for (int i = 0; i < damage.Length && i < PlayerOperator.GistsCount; i++)
            {
                if (damage[i] != 0 && GistsOfUnit[i].slider != null)
                {
                    Damage(damage[i], GistsOfUnit[i]);
                }
            }
        }
        public void Damage(float damage, Gist gist = Gist.Life)
        {
            if (damage == 0)
                return;

            Damage(damage, GistBasis[PlayerOperator.GetIndexByGist(gist)].gist);
        }
        private void Damage(float damage, GistOfUnit element)
        {
            if (!IsAlive || damage == 0 || element.slider == null || element.points <= 0)
                return;

            element.points -= (int)damage;
            element.points = math.clamp(element.points, 0, element.gist.points);
            element.slider.value = element.points;

            if (element.points <= 0 && unitBasis.GistOfDeath == element.gist.gist)
            {
                IsAlive = false;
                Death();
            }
            else
            {
                //audioOperator.PlaySound(UnitSounds.Hit, this);
            }
            //UnitInfoPanelOperator.RefreshPointsInfo(gameObject.GetComponent<UnitOperator>());
        }
        public void Heal(float[] cure)
        {
            for (int i = 0; i < cure.Length && i < PlayerOperator.GistsCount; i++)
            {
                if (cure[i] != 0 && GistsOfUnit[i].slider != null)
                {
                    Damage(-cure[i], GistsOfUnit[i]);
                }
            }
        }
        public void Heal(float cure, Gist gist = Gist.Life)
        {
            if (cure == 0)
                return;

            Damage(-cure, GistBasis[PlayerOperator.GetIndexByGist(gist)].gist);
        }

        private void RefreshBar()
        {
            for (int i = 0; i < PlayerOperator.GistsCount; i++)
            {
                if (GistsOfUnit[i].slider != null)
                    GistsOfUnit[i].slider.value = GistsOfUnit[i].points;
            }
        }
        public void SetUnitActivity(bool flag)
        {
            if (gameObject.TryGetComponent(out PlayerDebuger scriptPlayer))
                scriptPlayer.enabled = flag;
            if (gameObject.TryGetComponent(out MoveOperator scriptMoveEnemy))
                scriptMoveEnemy.enabled = flag;
            if (gameObject.TryGetComponent(out FightOperator scriptFight))
                scriptFight.enabled = flag;

            foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            {
                collider2D.enabled = flag;
            }
        }
        public void SetConflictSide(ConflictSide side = ConflictSide.Enemy)
        {

            switch (side)
            {
                case ConflictSide.Player:
                    gameObject.tag = "Player";
                    pedestal.color = SideColor.player;
                    break;
                case ConflictSide.Neutral:
                    gameObject.tag = "Untagget";
                    pedestal.color = SideColor.neutral;
                    break;
                default: //ConflictSide.Enemy
                    gameObject.tag = "Enemy";
                    pedestal.color = SideColor.enemy;
                    break;
            }
        }
        public void SetVisualState(VisualOfUnit visual = VisualOfUnit.Normal)
        {
            switch (visual)
            {
                case VisualOfUnit.Haziness:
                    unitBodyRenderer.color = new Color(.25f, 1f, .25f, .8f);
                    break;
                case VisualOfUnit.Grayness:
                    unitBodyRenderer.color = new Color(1f, .25f, .25f, .8f);
                    break;
                case VisualOfUnit.Off:
                    unitBodyRenderer.color = new Color(1f, 1f, 1f, 0f);
                    break;
                default: //Visual.Normal
                    unitBodyRenderer.color = Color.white;
                    break;
            }
        }
        public string GetName()
        {
            return unitBasis.GetTextInfo();
        }
        public void Pick()
        {
            unitBodyRenderer.material = InputOperator.PickMaterial;
        }
        public void UnPick()
        {
            unitBodyRenderer.material = InputOperator.DefaultMaterial;
        }
        public string GetTextInfo()
        {
            return GetName();
        }

        public void OnAgroRadiusEnter(Collider other)
        {
            targets.Add(other.GetComponent<UnitOperator>());
            unitBehaviour.GoToTarget();

        }
        public void OnAgroRadiusExit(Collider other)
        {
            targets.Remove(other.GetComponent<UnitOperator>());
            unitBehaviour.StopThePersecution();
        }
        public void OnAttackRadiusEnter(Collider other)
        {
            unitBehaviour.Attack();
        }
        public void OnAttackRadiusExit(Collider other)
        {
            unitBehaviour.GoToTarget();
        }
    }
}