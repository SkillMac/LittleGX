using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMager : MonoBehaviour {
    private SphereWindowMager m_Window;
    private ButtonCreat m_CreatMager;
    private float width;
    private float screenWidth;

    public void Init(SphereWindowMager window,ButtonCreat creat)
    {
        m_Window = window;
        m_CreatMager = creat;
    }
    // Use this for initialization
    void Start () {
        screenWidth = CDataMager.screenWidth;
    }
	
    void OnMouseDrag()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled == false) return;
        width = transform.lossyScale.x * transform.GetComponent<SpriteRenderer>().sprite.rect.width / 200;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.x > (screenWidth / 300f - width))
        {
            pos.x = screenWidth / 300f - width;
        }
        else if (pos.x < -screenWidth / 200f + width)
        {
            pos.x = -screenWidth / 200f + width;
        }
        transform.position = new Vector3(pos.x, transform.position.y, 0);
    }

    void OnMouseDown()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled == false) return;
        m_CreatMager.InitColor();
        m_Window.m_SelectSphere.SetColor(Color.red);
        SetColor(Color.black);
    }

    public void SetColor(Color color)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = color;
    }
    //小球变大位置不动可能超出边界
    public void ChangePositionAsScale(float addscale)
    {
        float offset = addscale *transform.GetComponent<SpriteRenderer>().sprite.rect.width / 200;
        width = (transform.lossyScale.x + addscale) * transform.GetComponent<SpriteRenderer>().sprite.rect.width / 200;
        if(transform.position.x - width < -screenWidth / 200f)
        {
            Vector3 pos = transform.position;
            pos.x += offset;
            transform.position = pos;
        }
        else if(transform.position.x + width >screenWidth/300f)
        {
            Vector3 pos = transform.position;
            pos.x -= offset;
            transform.position = pos;
        }
    }

    public void SetInitCollider(bool bol)
    {
        transform.GetComponent<BoxCollider2D>().enabled = bol;
    }

    public void SetRigidbody2D(RigidbodyType2D type)
    {
        transform.GetComponent<Rigidbody2D>().bodyType = type;
    }
}
