using UnityEngine;
using Unity.Mathematics;

public partial class Stats : MonoBehaviour
{
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
}
