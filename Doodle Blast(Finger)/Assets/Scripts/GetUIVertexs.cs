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
        float tempSize = Camera.main.orthographicSize;
        if (tempSize >= 4)
            tempSize = 4;
        float scale = (Screen.height / 200f) /tempSize;
        if (!isLeft)
        return new Vector3(rect.position.x * scale +  rect.sizeDelta.x /200, 
            rect.position.y * scale - rect.sizeDelta.y / 200, 0);
        else
            return new Vector3(rect.position.x * scale - rect.sizeDelta.x / 200,
            rect.position.y * scale + rect.sizeDelta.y / 200, 0);
    }

    public bool IsInUIWindow(Vector3 pos)
    {
        if (pos.x >= leftUp.x && pos.x <= rightDown.x
            && pos.y >= rightDown.y && pos.y <= leftUp.y) return true;
        return false;
    }
}
