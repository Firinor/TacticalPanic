using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Unit sprite", fileName = "New unit sprite")]
public class UnitSprite : ScriptableObject
{
    [Tooltip("Name of unit")]
    [SerializeField]
    private string unitName;
    public string Name { get { return unitName; } private set { } }

    [Tooltip("Sprite of unit")]
    [SerializeField]
    private Sprite sprite;
    public Sprite GetSprite { get { return sprite; } private set { } }

    [Tooltip("Image of the portrait of the unit's face")]
    [SerializeField]
    private Rect face = new Rect(new Vector2(50f, 0f), new Vector2(100f,100f));
    public Rect GetFace { get { return face; } private set { } }

    [Tooltip("The point of contact of the feet with the ground")]
    [SerializeField]
    private Vector2 touchPoint = new Vector2(100f, 0f);
    public Vector2 GetTouchPoint { get { return touchPoint; } private set { } }
}
