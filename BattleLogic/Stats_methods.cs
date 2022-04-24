using UnityEngine;
using Unity.Mathematics;

public partial class Stats : MonoBehaviour
{
    private void Death()
    {
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
        SetVisualState(Visual.Normal);
    }

    public bool CheckTermsAndDeploy()
    {
        bool Landing = true;

        for (int i = 0; i < S.GistsCount; i++)
        {
            if (Element[i].manaPrice > 0 && S.GetCurrentMana(i) < Element[i].manaPrice)
            {
                Landing = false;
                break;
            }
        }

        if (Landing)
        {
            S.DrawMana(GetManaPrice());
            Deploy();
        }

        return Landing;
    }

    public string GetElementColorString(int index)
    {
        return Element[index].colorString;
    }

    public string GetElementColorString(Gist gist)
    {
        return GetElementColorString(S.GetIndexByGist(gist));
    }

    public int GetElementManaPrice(int index)
    {
        return Element[index].manaPrice;
    }

    public int GetElementManaPrice(Gist gist)
    {
        return GetElementManaPrice(S.GetIndexByGist(gist));
    }

    public int[] GetManaPrice()
    {
        int[] price = new int[Element.Length];
        for (int i = 0; i < Element.Length; i++)
            price[i] = Element[i].manaPrice;
        return price;
    }

    public void Damage(float[] damage)
    {
        for (int i = 0; i < damage.Length && i < S.GistsCount; i++)
        {
            if (damage[i] != 0 && Element[i].slider != null)
            {
                Damage(damage[i], Element[i]);
            }
        }
    }

    public void Damage(float damage, Gist gist = Gist.Life)
    {
        if (damage == 0)
            return;

        Damage(damage, Element[S.GetIndexByGist(gist)]);
    }

    private void Damage(float damage, BodyElement element)
    {
        if (damage == 0 || element.slider == null)
            return;

        element.currentValue -= damage;
        element.currentValue = math.clamp(element.currentValue, 0, element.maxValue);
        element.slider.value = element.currentValue;

        if (element.currentValue <= 0 && DeathElement == element)
        {
            Death();
        }
    }

    public void Heal(float[] cure)
    {
        for (int i = 0; i < cure.Length && i < S.GistsCount; i++)
        {
            if (cure[i] != 0 && Element[i].slider != null)
            {
                Damage(-cure[i], Element[i]);
            }
        }
    }

    public void Heal(float cure, Gist gist = Gist.Life)
    {
        if (cure == 0)
            return;

        Damage(-cure, Element[S.GetIndexByGist(gist)]);
    }

    private void RefreshBar()
    {
        for (int i = 0; i < S.GistsCount; i++)
        {
            if (Element[i].slider != null)
                Element[i].slider.value = Element[i].currentValue;
        }
    }

    public void SetUnitActivity(bool flag)
    {
        if(gameObject.TryGetComponent(out Player scriptPlayer))
            scriptPlayer.enabled = flag;
        if (gameObject.TryGetComponent(out MoveEnemy scriptMoveEnemy))
            scriptMoveEnemy.enabled = flag;
        if (gameObject.TryGetComponent(out Fight scriptFight))
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

    public void SetVisualState(Visual visual = Visual.Normal)
    {
        switch (visual)
        {
            case Visual.Haziness:
                gameObject.SetActive(true);
                UnitSprite.color = new Color(1f, 1f, 1f, 0.1f);
                break;
            case Visual.Grayness:
                gameObject.SetActive(true);
                UnitSprite.color = new Color(.4f, .4f, .4f, .4f);
                break;
            case Visual.Off:
                gameObject.SetActive(false);
                break;
            default: //Visual.Normal
                gameObject.SetActive(true);
                UnitSprite.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }

    public string GetName()
    {
        return name;
    }
}