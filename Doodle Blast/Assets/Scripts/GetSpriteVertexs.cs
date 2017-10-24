using UnityEngine;

public class GetSpriteVertexs : MonoBehaviour
{
    public Transform leftUp;
    public Transform rightDown;

    void Awake()
    {
        //leftUp = GetVertexs(true);
        //rightDown = GetVertexs(false);
    }

    //private Vector3 GetVertexs(bool isLeft)
    //{
    //    float width = transform.GetComponent<SpriteRenderer>().sprite.rect.width * transform.lossyScale.x / 200;
    //    float height = transform.GetComponent<SpriteRenderer>().sprite.rect.height * transform.lossyScale.y / 200;

    //    if (!isLeft)
    //        return new Vector3(transform.position.x + width,
    //            transform.position.y - height);
    //    else
    //        return new Vector3(transform.position.x - width,
    //        transform.position.y + height, 0);
    //}

    public bool IsInCup(Vector3 pos)
    {
        //leftUp = GetVertexs(true);
        //rightDown = GetVertexs(false);

        if (pos.x >= leftUp.position.x && pos.x <= rightDown.position.x
            && pos.y >= rightDown.position.y && pos.y <= leftUp.position.y) return true;
        return false;
    }
}
