using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {
    private int x;
    private int y;
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }
    public PieceType Type
    {
        get { return numComponent.Type; }
    }
    private Grid grid;
    public Grid GridRef
    {
        get { return grid; }
    }

    private PieceNum numComponent;
    public PieceNum NumComponent
    {
        get { return numComponent; }
    }

    private Vector3 oldPos;
    private Animator m_Anim;

    void Awake()
    {
        numComponent = GetComponent<PieceNum>();
        m_Anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        oldPos = transform.position * 100.0f + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
    }
	
    public void Init(int _x, int _y, Grid _grid,int lev,PieceType _type)
    {
        X = _x;
        Y = _y;
        grid = _grid;
        numComponent.Setsprite(lev, _type);
    }
    public void Init(int _x, int _y)
    {
        X = _x;
        Y = _y;
    }
    
    private void OnMouseDown()
    {
        if (grid.IsOver()) return;
        oldPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (m_Anim == null) return;
        if (grid.IsOver()) return;
        Vector3 dir = Input.mousePosition - oldPos;
        
        if (Mathf.Abs(dir.normalized.x) > Mathf.Abs(dir.normalized.y) && dir.x < 0)
        {
            if(y >0)
            {
                if(!grid.CanDelete(this, grid.GetTransWithXY(y - 1, x), new Vector3(5.0f, transform.position.y, 0)))
                {
                    m_Anim.SetTrigger("IsLeft");
                }
            }
            else
                m_Anim.SetTrigger("IsLeft");
        }
        if (Mathf.Abs(dir.normalized.x) > Mathf.Abs(dir.normalized.y) && dir.x > 0)
        {
            if( y < 4)
            {
                if(!grid.CanDelete(this, grid.GetTransWithXY(y + 1, x), new Vector3(-5.0f, transform.position.y, 0)))
                {
                    m_Anim.SetTrigger("IsRight");
                }
            }
            else
                m_Anim.SetTrigger("IsRight");
        }
        if (Mathf.Abs(dir.normalized.x) <= Mathf.Abs(dir.normalized.y) && dir.y < 0)
        {
            if(x<4)
            {
                if(!grid.CanDelete(this, grid.GetTransWithXY(y, x + 1), new Vector3(transform.position.x, 5.0f, 0)))
                {
                    m_Anim.SetTrigger("IsDown");
                }
            }
            else
                m_Anim.SetTrigger("IsDown");
        }
        if (Mathf.Abs(dir.normalized.x) <= Mathf.Abs(dir.normalized.y) && dir.y > 0)
        {
            if(x>0)
            {
                if(!grid.CanDelete(this, grid.GetTransWithXY(y, x - 1), new Vector3(transform.position.x, -5.0f, 0)))
                {
                    m_Anim.SetTrigger("IsUp");
                }
            }
            else
                m_Anim.SetTrigger("IsUp");
        }
    }

    public void PlayAnim()
    {
        if (m_Anim == null) return;

        m_Anim.SetTrigger("AddScale");

        int lev = numComponent.GetCurrentLev + 1;

        numComponent.Setsprite(lev,Type);
    }
 
}
