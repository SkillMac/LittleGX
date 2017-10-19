using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCup : MonoBehaviour {
    private float width;
    private float screenWidth;
	// Use this for initialization
	void Start () {
        screenWidth = CDataMager.screenWidth;
        width = transform.lossyScale.x * transform.GetComponent<SpriteRenderer>().sprite.rect.width /200;
        SetInitCollider(false);
    }
	
    void OnMouseDrag()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled == false) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.x > (screenWidth / 300f - width/2.0f))
        {
            pos.x = screenWidth / 300f - width / 2.0f;
        }
        else if (pos.x < -screenWidth / 200f + width)
        {
            pos.x = -screenWidth / 200f + width;
        }
        transform.position = new Vector3(pos.x, CDataMager.cupPositionY, 0);
        //Debug.Log(transform.position);
        CDataMager.getInstance.cupPositionX = pos.x;
    }

    public void SetInitCollider(bool bol)
    {
        transform.GetComponent<BoxCollider2D>().enabled = bol;
    }
}
