using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {

    private int x;
    private int y;

    public int X
    {
        get { return x; }
        set
        {
            if (IsMovable())
            {
                x = value;
            }
        }
    }

    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                y = value;
            }
        }
    }

    private PieceType type;

    public PieceType Type
    {
        get { return type; }
    }

    private Grid grid;

    public Grid GridRef
    {
        get { return grid; }
    }

    private MoveAble movableComponent;

    public MoveAble MovableComponent
    {
        get { return movableComponent; }
    }

    private PieceNum numComponent;

    public PieceNum NumComponent
    {
        get { return numComponent; }
    }

    private Vector3 startPos,oldPos;

    private Animator m_Anim;

    void Awake()
    {
        movableComponent = GetComponent<MoveAble>();
        numComponent = GetComponent<PieceNum>();
        m_Anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {

        startPos = transform.position;
        oldPos = transform.position * 100.0f + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Init(int _x, int _y, Grid _grid,PieceType _type)
    {
        X = _x;
        Y = _y;
        grid = _grid;
        type = _type;
    }
  
    private void OnMouseUp()
    {
        Vector3 dir = Input.mousePosition - oldPos;
        
        if (m_Anim == null) return;
        
        if (Mathf.Abs(dir.normalized.x) > Mathf.Abs(dir.normalized.y) && dir.x < 0)
        {
            if (y == 0 || !grid.CanDelete(this, grid.GetTransWithXY(y - 1, x), new Vector3(5.0f, transform.position.y, 0)))
                m_Anim.SetTrigger("IsLeft");
            else
            {
                grid.Current = this;
                grid.IsMove = true;
            }
        }
        //if (Mathf.Abs(dir.normalized.x) > Mathf.Abs(dir.normalized.y) && dir.x > 0)
        //{
        //    if (y == 4 || !grid.CanDelete(this, grid.GetTransWithXY(y+1,x)))
        //        m_Anim.SetTrigger("IsRight");
        //}
        //if (Mathf.Abs(dir.normalized.x) <= Mathf.Abs(dir.normalized.y)  &&dir.y<0)
        //{
        //    if (x == 4 || !grid.CanDelete(this, grid.GetTransWithXY(y,x+1)))
        //        m_Anim.SetTrigger("IsDown");
        //}
        //if (Mathf.Abs(dir.normalized.x) <= Mathf.Abs(dir.normalized.y)  && dir.y > 0)
        //{
        //    if (x == 0 || !grid.CanDelete(this, grid.GetTransWithXY(y,x-1)))
        //        m_Anim.SetTrigger("IsUp");
        //}
    }

    public void PlayAnim()
    {
        m_Anim.SetTrigger("AddScale");

        NumType nt = numComponent.GetnewType;

        numComponent.Setsprite(nt + 1);
    }
    
    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsColored()
    {
        return numComponent != null;
    }

}
