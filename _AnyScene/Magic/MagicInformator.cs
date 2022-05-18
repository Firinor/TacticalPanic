using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInformator : MonoBehaviour
{
    public static int GetManaCostInfo(int i)
    {
        if (i == 0){ return 0; }
        if (i == 1) { return 0; }
        return 255;
    }
}
