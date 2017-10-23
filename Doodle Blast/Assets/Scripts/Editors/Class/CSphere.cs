using UnityEngine;
using System;

[Serializable]
public class CSphere{
    public Vector3 position;
    public Vector3 scale;
    public string spriteName;
    public CSphere(Vector3 position, Vector3 scale, string spriteName)
    {
        this.position = position;
        this.scale = scale;
        this.spriteName = spriteName;
    }
}
