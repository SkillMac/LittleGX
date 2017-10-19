using UnityEngine;
using System;

[Serializable]
public class CCup{
    public Vector3 position;
    public Vector3 winStandard;
    public CCup(Vector3 position, Vector3 winStandard)
    {
        this.position = position;
        this.winStandard = winStandard;
    }
}
