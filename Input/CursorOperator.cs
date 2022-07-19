using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CursorType
{
    Arrow, Heal, Damage, ManaRegen, MagicCast, Off, a
}

public class CursorOperator : MonoBehaviour
{
    [SerializeField]
    private List<CursorTypeClass> list;
    private int[] mouseTypeBank = new int[Enum.GetValues(typeof(CursorType)).Length];

    private CursorTypeClass cursorType;

    private float timer;
    private int frame;
    private bool update;

    void Start()
    {
        mouseTypeBank[0] = 1;
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
                //Cursor.SetCursor(cursorType.textureArray[frame], cursorType.offset, CursorMode.Auto);
            }
        }
    }

    private void SetCursorType(CursorTypeClass IncomingCursorType)
    {
        if (cursorType != IncomingCursorType)
        { 
            cursorType = IncomingCursorType;
            frame = 0;
            update = cursorType.rate > 0;
            Cursor.SetCursor(cursorType.textureArray[0], cursorType.offset, CursorMode.Auto);
        }
    }

    private void CursorIncrease(CursorType cursorType, int impact)
    {
        switch (cursorType)
        {
            case CursorType.Arrow:
                mouseTypeBank[0] += impact;
                break;
            case CursorType.Heal:
                mouseTypeBank[1] += impact;
                break;
            case CursorType.Damage:
                mouseTypeBank[2] += impact;
                break;
            case CursorType.ManaRegen:
                mouseTypeBank[3] += impact;
                break;
            case CursorType.MagicCast:
                mouseTypeBank[4] += impact;
                break;
            case CursorType.Off:
                mouseTypeBank[5] += impact;
                break;
            default:
                new Exception("There is mouse cursor exeption!");
                break;
        }

        CheckCursorState();
    }

    private void CheckCursorState()
    {
        int maxLayer = mouseTypeBank.Max<int>();
        if (mouseTypeBank[1] == maxLayer)//CursorType.Heal
        {
            SetCursorType(list[1]);
        }
        else if (mouseTypeBank[2] == maxLayer)//CursorType.Damage
        {
            SetCursorType(list[2]);
        }
        else if(mouseTypeBank[4] == maxLayer)//CursorType.MagicCast
        {
            SetCursorType(list[4]);
        }
        else if(mouseTypeBank[3] == maxLayer)//CursorType.ManaRegen
        {
            SetCursorType(list[3]);
        }
        else if(mouseTypeBank[5] == maxLayer)//CursorType.Off
        {
            SetCursorType(list[5]);
        }
        else//CursorType.Arrow
        {
            SetCursorType(list[0]);
        }
    }

    public void CursorOverlap(CursorType cursorType)
    {
        CursorIncrease(cursorType, 1);
    }

    public void CursorRemove(CursorType cursorType)
    {
        CursorIncrease(cursorType, -1);
    }

    [Serializable]
    public class CursorTypeClass
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float rate;
        public Vector2 offset;
    }
}
