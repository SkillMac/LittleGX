using UnityEngine;
using System;

[Serializable]
public class CSphere{
    public Vector3 position;
    public Vector3 scale;
    public CSphere(Vector3 position, Vector3 scale)
    {
        this.position = position;
        this.scale = scale;
    }
}
