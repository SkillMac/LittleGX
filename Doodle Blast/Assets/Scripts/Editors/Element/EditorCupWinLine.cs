using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCupWinLine : MonoBehaviour {
    private const float yMax = -1.4f;
    private const float yMin = -3.4f;
    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.y > yMax)
        {
            pos.y = yMax;
        }
        else if (pos.y < yMin)
        {
            pos.y = yMin;
        }
        Vector3 temp = transform.position;
        temp.y = pos.y;
        transform.position = temp;
        CDataMager.getInstance.winLinePositionY = pos.y;
    }

    public void SetInitCollider(bool bol)
    {
        transform.GetComponent<BoxCollider2D>().enabled = bol;
    }
}
