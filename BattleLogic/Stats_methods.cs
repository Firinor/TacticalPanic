using UnityEngine;
using Unity.Mathematics;
using System.Text;

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

    public void Damage(StringBuilder logs, float[] damage, float deltaTime)
    {
        for (int i = 0; i < damage.Length && i < S.GistsCount; i++)
        {
            if (damage[i] != 0 && Element[i].slider != null)
            {
                Damage(logs, damage[i], deltaTime, Element[i]);
            }
        }
    }

    public void Damage(StringBuilder logs, float damage, float deltaTime, Gist gist = Gist.Life)
    {
        if (damage == 0)
            return;

        Damage(logs, damage, deltaTime, Element[S.GetIndexByGist(gist)]);
    }

    private void Damage(StringBuilder logs, float damage, float deltaTime, BodyElement element)
    {
        if (damage == 0 || deltaTime <= 0 ||element.slider == null || element.currentValue <= 0)
            return;

        var forLog = new StringBuilder();
        element.currentValue -= damage * deltaTime;
        element.currentValue = math.clamp(element.currentValue, 0, element.maxValue);
        forLog.Append($"{logs} = > {GetName()} {-damage * deltaTime} {element.name}.");
        element.slider.value = element.currentValue;

        if (element.currentValue <= 0 && DeathElement == element)
        {
            forLog.AppendLine($"\n{GetName()} die!");
            
            Death();
        }
        Log.Info(forLog);
    }

    public void Heal(StringBuilder logs, float[] cure, float deltaTime)
    {
        for (int i = 0; i < cure.Length && i < S.GistsCount; i++)
        {
            if (cure[i] != 0 && Element[i].slider != null)
            {
                Damage(logs, - cure[i], deltaTime, Element[i]);
            }
        }
    }

    public void Heal(StringBuilder logs, float cure, float deltaTime, Gist gist = Gist.Life)
    {
        if (cure == 0)
            return;

        Damage(logs, - cure, deltaTime, Element[S.GetIndexByGist(gist)]);
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
                unitSpriteRenderer.color = new Color(1f, 1f, 1f, 0.1f);
                break;
            case Visual.Grayness:
                gameObject.SetActive(true);
                unitSpriteRenderer.color = new Color(.4f, .4f, .4f, .4f);
                break;
            case Visual.Off:
                gameObject.SetActive(false);
                break;
            default: //Visual.Normal
                gameObject.SetActive(true);
                unitSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetCardSprite()
    {
        return unitSprite;
    }
}