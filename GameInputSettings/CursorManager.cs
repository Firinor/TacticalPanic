using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private List<CursorTypeClass> list;

    private CursorTypeClass cursorType;

    private float timer;
    private int frame;
    private bool update;

    public enum CursorType
    {
        Arrow, Heal, Damage, ManaRegen, MagicCast, Off
    }

    void Start()
    {
        SetCursorType(list[0]);
    }

    
    void Update()
    {
        if (update)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer += cursorType.rate;
                frame = (frame + 1) % cursorType.textureArray.Length;
                Cursor.SetCursor(cursorType.textureArray[frame], cursorType.offset, CursorMode.Auto);
            }
        }
    }

    private void SetCursorType(CursorTypeClass cursorType)
    {
        this.cursorType = cursorType;
        frame = 0;
        update = cursorType.rate > 0;
        Cursor.SetCursor(cursorType.textureArray[0], cursorType.offset, CursorMode.Auto);
    }

    [System.Serializable]
    public class CursorTypeClass
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float rate;
        public Vector2 offset;
    }
}
