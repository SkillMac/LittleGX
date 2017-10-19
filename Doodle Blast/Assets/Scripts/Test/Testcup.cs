using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testcup : MonoBehaviour
{
    public Transform leftUp;
    public Transform rightUp;
    public Transform leftDown;

    public bool IsInCup(Vector3 pos)
    {
        if (pos.x >= leftUp.position.x && pos.x <= rightUp.position.x
            && pos.y >= leftDown.position.y && pos.y <= leftUp.position.y) return true;
        return false;
    }
}
