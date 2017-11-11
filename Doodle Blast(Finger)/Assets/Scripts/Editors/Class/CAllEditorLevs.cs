using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllEditorLevs  {
    private static CAllEditorLevs instance;
    private CAllEditorLevs() { }
    public static CAllEditorLevs GetInstance
    {
        get
        {
            if (instance == null)
                instance = new CAllEditorLevs();
            return instance;
        }
    }

    public Dictionary<int, CLevel> allEditorLevs = new Dictionary<int, CLevel>();
    public List<int> allLevID = new List<int>();
}
