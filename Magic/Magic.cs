using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetManaCostInfo(int i)
    {
        if (i == 0){ return 0; }
        if (i == 1) { return 0; }
        return 255;
    }
}
