using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class GistInfoOperator : MonoBehaviour
    {
        [SerializeField]
        private Text attackText;
        [SerializeField]
        private Text reattackText;
        [SerializeField]
        private Text defenseText;
        [SerializeField]
        private Text pointsText;
        [SerializeField]
        private Text regenText;
        [SerializeField]
        private Text moveSpeedText;
        [SerializeField]
        private Text manaPriceText;

        private void Awake()
        {
            SetActive(false);
        }

        public void SetNumerical(GistBasis gist)
        {
            SetActive(true);
            attackText.text = gist.attack == 0 ? "" : gist.attack.ToString();
            reattackText.text = gist.reattack == 0 ? "" : gist.reattack.ToString();
            defenseText.text = gist.defense == 0 ? "" : gist.defense.ToString();
            pointsText.text = gist.points == 0 ? "" : gist.points.ToString();
            regenText.text = gist.regen == 0 ? "" : gist.regen.ToString();
            moveSpeedText.text = gist.moveSpeed == 0 ? "" : gist.moveSpeed.ToString();
            manaPriceText.text = gist.manaPrice == 0 ? "" : gist.manaPrice.ToString();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
