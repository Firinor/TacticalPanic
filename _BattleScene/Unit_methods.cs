using UnityEngine;
using Unity.Mathematics;
using System.Text;

public partial class Unit : MonoBehaviour, IInfoble
{ 
    private void Death()
    {
        audioOperator.PlaySound(UnitSounds.Death, this);
        SetUnitActivity(false);
        AnimDeath();
    }
    private void AnimDeath()
    {
        gameObject.GetComponentInChildren<Animator>().Play("Death");
    }
    public void DeathAnimationEnds()
    {
        Destroy(gameObject);
    }

    public void Deploy()
    {
        SetUnitActivity(true);
        SetVisualState(VisualOfUnit.Normal);
    }

    public bool CheckTermsAndDeploy()
    {
        bool Landing = false;

        if (CheckTerms())
        {
            PlayerOperator.DrawMana(GetManaPrice());
            Landing = true;
            Deploy();
        }

        return Landing;
    }

    public bool CheckTerms()
    {
        bool Check = true;

        for (int i = 0; i < PlayerOperator.GistsCount; i++)
        {
            if (GistBasis[i].manaPrice > 0 && PlayerOperator.GetCurrentMana(i) < GistBasis[i].manaPrice)
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
        if (!IsAlive || damage == 0 ||element.slider == null || element.points <= 0)
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
            audioOperator.PlaySound(UnitSounds.Hit, this);
        }
        UnitInfoPanelOperator.RefreshPointsInfo(gameObject.GetComponent<Unit>());
    }
    public void Heal(float[] cure)
    {
        for (int i = 0; i < cure.Length && i < PlayerOperator.GistsCount; i++)
        {
            if (cure[i] != 0 && GistsOfUnit[i].slider != null)
            {
                Damage( - cure[i], GistsOfUnit[i]);
            }
        }
    }
    public void Heal( float cure, Gist gist = Gist.Life)
    {
        if (cure == 0)
            return;

        Damage(- cure, GistBasis[PlayerOperator.GetIndexByGist(gist)].gist);
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
        if(gameObject.TryGetComponent(out PlayerDebuger scriptPlayer))
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
                gameObject.GetComponent<SpriteRenderer>().color = SideColor.player;
                break;
            case ConflictSide.Neutral:
                gameObject.tag = "Untagget";
                gameObject.GetComponent<SpriteRenderer>().color = SideColor.neutral;
                break;
            default: //ConflictSide.Enemy
                gameObject.tag = "Enemy";
                gameObject.GetComponent<SpriteRenderer>().color = SideColor.enemy;
                break;
        }
    }
    public void SetVisualState(VisualOfUnit visual = VisualOfUnit.Normal)
    {
        switch (visual)
        {
            case VisualOfUnit.Haziness:
                gameObject.SetActive(true);
                unitAnimator.enabled = false;
                unitSpriteRenderer.color = new Color(.25f, 1f, .25f, .8f);
                break;
            case VisualOfUnit.Grayness:
                gameObject.SetActive(true);
                unitAnimator.enabled = false;
                unitSpriteRenderer.color = new Color(1f, .25f, .25f, .8f);
                break;
            case VisualOfUnit.Off:
                gameObject.SetActive(false);
                break;
            default: //Visual.Normal
                gameObject.SetActive(true);
                unitAnimator.enabled = true;
                break;
        }
    }
    public string GetName()
    {
        return name;
    }
    public Sprite GetCardSprite()
    {
        return unitBasis.unitInformator.unitSprite;
    }

    public void Pick()
    {
        unitSpriteRenderer.material = InputOperator.PickMaterial;
    }

    public void UnPick()
    {
        unitSpriteRenderer.material = InputOperator.DefaultMaterial;
    }

    public AudioClip GetDeathSound()
    {
        return sounds.Death;
    }
    public AudioClip GetHitSound()
    {
        return sounds.Hit;
    }
    public AudioClip GetAttackSound()
    {
        return sounds.Attack;
    }

    public string GetTextInfo()
    {
        return GetName();
    }
}