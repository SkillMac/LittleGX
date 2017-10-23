using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllFixedLev {
    public static bool isFixedLev;
    private static CAllFixedLev instance;
    private CAllFixedLev() { }
    public static CAllFixedLev GetInstance
    {
        get
        {
            if (instance == null)
                instance = new CAllFixedLev();
            return instance;
        }
    }

    public Dictionary<int, CLevel> allFixedLevs = new Dictionary<int, CLevel>();
}
