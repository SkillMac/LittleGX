using UnityEngine;

public class GetSpriteVertexs : MonoBehaviour
{
    public Transform leftUp;
    public Transform rightDown;
    public Transform maxRight;
    public Transform maxUp;
 
    public bool IsInCup(Vector3 pos)
    {
        if (pos.x >= leftUp.position.x && pos.x <= rightDown.position.x
            && pos.y >= rightDown.position.y && pos.y <= leftUp.position.y) return true;
        return false;
    }

    public bool IsInObj(Vector3 pos)
    {
        if (pos.x >= maxRight.position.x || pos.y >= maxUp.position.y) return true;
        return false;
    }
}
