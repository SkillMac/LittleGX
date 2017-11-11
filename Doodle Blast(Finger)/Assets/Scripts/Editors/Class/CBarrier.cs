using UnityEngine;
using System;

[Serializable]
public class CBarrier {
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
    public CBarrier(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }
}
