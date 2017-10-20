using UnityEngine;

public class GetUIVertexs : MonoBehaviour
{
    private Vector3 leftUp;
    private Vector3 rightDown;

    void Awake()
    {
        leftUp = GetVertexs(true);
        rightDown = GetVertexs(false);
    }

    private Vector3 GetVertexs(bool isLeft)
    {
        RectTransform rect = GetComponent<RectTransform>();
        float sacleY = Camera.main.orthographicSize / (Screen.height / 200f);

        if (!isLeft)
        return new Vector3(transform.position.x + sacleY * rect.rect.width /200, 
            transform.position.y - sacleY * rect.rect.height / 200, 0);
        else
            return new Vector3(transform.position.x - sacleY * rect.rect.width / 200,
            transform.position.y + sacleY * rect.rect.height / 200, 0);
    }

    public bool IsInUIWindow(Vector3 pos)
    {
        if (pos.x >= leftUp.x && pos.x <= rightDown.x
            && pos.y >= rightDown.y && pos.y <= leftUp.y) return true;
        return false;
    }
}
