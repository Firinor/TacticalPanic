using UnityEngine;

namespace TacticalPanicCode
{
    [CreateAssetMenu(menuName = "Unit/Unit info", fileName = "UnitInfo")]
    public class UnitInformator : ScriptableObject
    {
        private UnitBasis unit;
        public UnitBasis unitBasis
        {
            get
            {
                return unit;
            }
            set
            {
                if (unit == null)
                    unit = value;
            }
        }

        [Tooltip("Name of unit")]
        [SerializeField]
        private string unitName;
        public string Name { get { return unitName; } }

        [Tooltip("Sprite of unit")]
        [SerializeField]
        private Sprite sprite;
        public Sprite unitSprite { get { return sprite; } }

        [Tooltip("Image of the portrait of the unit's face")]
        [SerializeField]
        private Sprite face;
        public Sprite unitFace { get { return face; } }

        [Tooltip("May player pick this unit to party")]
        [SerializeField]
        private bool playable;
        public bool Playable { get { return playable; } }

        [Tooltip("The central line of the unit in the sprite")]
        [SerializeField]
        private float offset;
        public float centerOffset { get { return offset; } }

        [Tooltip("The point of contact of the feet with the ground")]
        [SerializeField]
        private Vector2 touchPoint = new Vector2(100f, 0f);
        public Vector2 unitTouchPoint { get { return touchPoint; } }
    }
}
