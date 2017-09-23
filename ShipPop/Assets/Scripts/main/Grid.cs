using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
    private const int rowCount =5;
    private const int colCount =5;
    private const float OFFSETX = 1.4f;
    private const float OFFSETY = 1.8f;
    private int[] rangeID;
    private GamePiece[,] pieces;
    private Vector2[,] backs = new Vector2[rowCount, colCount];
    private bool IsMoveLeft, IsMoveRight, IsMoveUp, IsMoveDown;
    private const float movespeed = 20.0f;
    
    private List<GamePiece> m_Move;//记录需要移动的队列
    private static int HighDex;//记录最高分数的物体

    public Text text_Score;
    public GameObject m_Prefab;
    public GameObject window_over;
    public GameObject GoldEffect, EnemyEffect;
    
    public void Init(bool bol)
    {
        HighDex = 1;
        GetRandomnum();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int id = rangeID[i * colCount + j];
                int role = LoadDataClass.lstRole[id];
                int lev = LoadDataClass.lstLev[id];
                Vector2 tempPos = GetWorldPos(i, j);
                backs[i, j] = tempPos;
                GameObject current;
                if (bol)
                {
                    current = Instantiate(m_Prefab, tempPos, Quaternion.identity);
                }
                else
                {
                    current = pieces[i, j].gameObject;
                }
                
                pieces[i, j] = current.GetComponent<GamePiece>();
                pieces[i, j].Init(i, j, this, lev, (PieceType)role);
                current.transform.parent = transform;
            }
        }
        if(!bol)
        {
            text_Score.GetComponent<ScoreAnim>().InitScore();
        }
    }

    void Start()
    {
        pieces = new GamePiece[rowCount, colCount];
        Init(true);
    }
  
	// Update is called once per frame
	void Update () {
        if (IsMoveLeft) { MoveLeft();}
        if (IsMoveRight) {MoveRight(); }
        if (IsMoveDown) {MoveDown(); }
        if (IsMoveUp) {MoveUp(); }
        if(IsOver() == null && !window_over.activeSelf)
        {
            window_over.SetActive(true);
            SetUnActive();
        }
    }

    void SetUnActive()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                pieces[i, j].enabled = false;
            }
        }
    }
    public Vector2 GetWorldPos(int x,int y)
    {
        return new Vector2((y - colCount / 2) * OFFSETX + transform.position.x,
           (rowCount / 2 - x) * OFFSETY + transform.position.y);
    }

    void GetRandomnum()
    {
        //随机总数组  
        int[] sequence = new int[rowCount*colCount];
        //取到的不重复数字的数组长度  
         rangeID = new int[rowCount * colCount];

        for (int i = 0; i < rowCount * colCount; i++)
        {
            sequence[i] = i;
        }
        int end = rowCount * colCount -1;

        for (int i = 0; i < rowCount * colCount; i++)
        {
            //随机一个数，每随机一次，随机区间-1  
            int num = Random.Range(0, end + 1);

            rangeID[i] = sequence[num];
            //将区间最后一个数赋值到取到数上  
            sequence[num] = sequence[end];
            end--;
        }
    }
    /// <summary>
    /// 挪动物体1是否可以消除物体2
    /// </summary>
    /// <param name="obj1">挪动的物体</param>
    /// <param name="obj2">是否可以消除的物体</param>
    public bool  CanDelete(GamePiece obj1,GamePiece obj2,Vector3 poss)
    {
        m_Move = new List<GamePiece>();
        Vector3 oldPos = Vector3.zero;
        if (obj2 == null) return false;
        if(obj1.Type == PieceType.Gold)
        {
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetCurrentLev == obj1.NumComponent.GetCurrentLev)
            {
                obj1.transform.position = Vector3.zero;
                GetBool(poss);
                ReSort(obj1, poss);
                obj2.PlayAnim();
                //这里可以加入消除的特效
                return true;
            }
        }
        if(obj1.Type == PieceType.My)
        {
            if (obj2.Type == PieceType.Enemy && obj2.NumComponent.GetCurrentLev <= obj1.NumComponent.GetCurrentLev)
            {
                oldPos = obj2.transform.position;
                obj2.transform.position = Vector3.zero;
                GetBool(poss);
                ReSort(obj2, poss);
                //这里可以加入消除的特效
                Instantiate(EnemyEffect, oldPos, Quaternion.identity);
                return true;
            }
            if (obj2.Type == PieceType.My && obj2.NumComponent.GetCurrentLev == obj1.NumComponent.GetCurrentLev)
            {
                obj1.transform.position = Vector3.zero;
                GetBool(poss);
                if(obj2.NumComponent.GetCurrentLev >= (HighDex-1))
                    HighDex ++;

                ReSort(obj1, poss);
                obj2.PlayAnim();
                //这里可以加入消除的特效
                return true;
            }
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetCurrentLev <= obj1.NumComponent.GetCurrentLev)
            {
                oldPos = obj2.transform.position;
                obj2.transform.position = Vector3.zero;
                GetBool(poss);
                int scor = (int)(obj2.NumComponent.GetCurrentLev + 1) * 3;

                text_Score.GetComponent<ScoreAnim>().AddScores(scor);

                //text_Score.text = ((int)(obj2.NumComponent.GetCurrentLev + 1) * 3 + scor).ToString();

                ReSort(obj2, poss);
                //这里可以加入消除的特效，同时加分
                Instantiate(GoldEffect, oldPos, Quaternion.identity);

                return true;
            }
        }
        return false;
    }

    public GamePiece IsOver()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if(pieces[i,j].Type != PieceType.Enemy)
                {
                    if((j>0&&EnDelete(pieces[i,j],pieces[i,j-1])) || (j<colCount-1&& EnDelete(pieces[i, j], pieces[i, j + 1]) )|| (i < rowCount -1 && EnDelete(pieces[i, j], pieces[i + 1, j]) ) || ( i>0 &&EnDelete(pieces[i, j], pieces[i - 1, j])))
                    {
                        return pieces[i, j];
                    }
                }
            }
        }
        return null;
       
    }

     bool EnDelete(GamePiece obj1, GamePiece obj2)
    {
        if (obj2 == null) return false;

        if (obj1.Type == PieceType.Gold)
        {
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetCurrentLev == obj1.NumComponent.GetCurrentLev)
            {
                return true;
            }
        }
        if (obj1.Type == PieceType.My)
        {
            if (obj2.Type == PieceType.Enemy && obj2.NumComponent.GetCurrentLev <= obj1.NumComponent.GetCurrentLev)
            {
                return true;
            }
            if (obj2.Type == PieceType.My && obj2.NumComponent.GetCurrentLev == obj1.NumComponent.GetCurrentLev)
            {
                return true;
            }
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetCurrentLev <= obj1.NumComponent.GetCurrentLev)
            {
                return true;
            }
        }
        return false;
    }

    void GetBool(Vector3 poss)
    {
        if(poss.x == 5.0f)
        {
            IsMoveLeft = true;
        }
        if(poss.x == -5.0f)
        {
            IsMoveRight = true;
        }
        if(poss.y == 5.0f)
        {
            IsMoveDown = true;
        }
        if(poss.y == -5.0f)
        {
            IsMoveUp = true;
        }
    }
    /// <summary>
    /// 根据x，y坐标获取对应的物体
    /// </summary>
    /// <param name="x">y坐标</param>
    /// <param name="y">x坐标</param>
    /// <returns></returns>
    public GamePiece GetTransWithXY(int x,int y)
    {
        return pieces[y, x];
    }
    /// <summary>
    /// 根据类型获取级别
    /// </summary>
    /// <param name="类型"></param>
    /// <returns></returns>
    private int GetLev(int type)
    {
        switch (type)
        {
            case 0:
                return 0;
            case 1:
                return 0;
            case 2:
                {
                    if (HighDex >= 3)
                    {
                        int dex = Random.Range(0, HighDex-1);
                        int rangenum = Random.Range(1, 100);
                        //红色高等级物体出现的概率
                        if (rangenum >= 1 && rangenum <= 20)
                        {
                            return HighDex - 1;
                        }
                        else {return dex; }
                    }
                    return 0;
                }
            default:
                return 0;
        }
    }

    //排序
    public void ReSort(GamePiece deleteObj, Vector3 pos)
    {
        int x = deleteObj.X;
        int y = deleteObj.Y;
        deleteObj.transform.position = pos;

        int id0 = Random.Range(0, 10);
        int ty = LoadDataClass.lstCreat[id0];
        int lev = GetLev(ty);
        deleteObj.Init(0, 0, this, lev,(PieceType)ty);
        if(IsMoveLeft)
        {
            for (int i = y; i < colCount; i++)
            {
                if (i != colCount - 1)
                {
                    pieces[x, i] = pieces[x, i + 1];

                    m_Move.Add(pieces[x, i]);
                }

                if (i == colCount - 1)
                {
                    pieces[x, i] = deleteObj;
                    deleteObj.Init(x, i + 1);
                    m_Move.Add(pieces[x, i]);
                }
            }
        }
        if (IsMoveRight)
        {
            for (int i = y; i >=0; i--)
            {
                if (i != 0)
                {
                    pieces[x, i] = pieces[x, i - 1];

                    m_Move.Add(pieces[x, i]);
                }

                if (i == 0)
                {
                    pieces[x, i] = deleteObj;
                    deleteObj.Init(x, i - 1);
                    m_Move.Add(pieces[x, i]);
                }

            }
        }
        if (IsMoveUp)
        {
            for (int i = x; i < rowCount; i++)
            {
                if (i != rowCount - 1)
                {
                    pieces[i, y] = pieces[ i + 1,y];

                    m_Move.Add(pieces[i, y]);
                }

                if (i == rowCount - 1)
                {
                    pieces[i, y] = deleteObj;
                    deleteObj.Init(i+1, y);
                    m_Move.Add(pieces[i, y]);
                }

            }
        }
        if (IsMoveDown)
        {
            for (int i = x; i >= 0; i--)
            {
                if (i != 0)
                {
                    pieces[i, y] = pieces[i - 1, y];

                    m_Move.Add(pieces[i, y]);
                }

                if (i == 0)
                {
                    pieces[i, y] = deleteObj;
                    deleteObj.Init(i - 1, y);
                    m_Move.Add(pieces[i, y]);
                }
            }
        }
    }

    public void MoveLeft()
    {
        for(int i =0;i<m_Move.Count;i++)
        {
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y - 1];

            m_Move[i].transform.position = Vector3.MoveTowards(m_Move[i].transform.position, pp, movespeed * Time.deltaTime);

        }

        if(!CanStopLeft())
        {
            for (int i = 0; i < m_Move.Count; i++)
            {
                  m_Move[i].Init(m_Move[i].X, m_Move[i].Y - 1);
            }
            IsMoveLeft = false;
        }
    }
    public void MoveRight()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y + 1];

            m_Move[i].transform.position = Vector3.MoveTowards(m_Move[i].transform.position, pp, movespeed * Time.deltaTime);

        }

        if (!CanStopRight())
        {
            for (int i = 0; i < m_Move.Count; i++)
            {
                m_Move[i].Init(m_Move[i].X, m_Move[i].Y + 1);
            }
            IsMoveRight = false;
        }
    }
    public void MoveUp()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X - 1, m_Move[i].Y];

            m_Move[i].transform.position = Vector3.MoveTowards(m_Move[i].transform.position, pp, movespeed * Time.deltaTime);

        }

        if (!CanStopUp())
        {
            for (int i = 0; i < m_Move.Count; i++)
            {
                m_Move[i].Init(m_Move[i].X -1, m_Move[i].Y);
            }
            IsMoveUp = false;
        }
    }
    public void MoveDown()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X + 1, m_Move[i].Y];

            m_Move[i].transform.position = Vector3.MoveTowards(m_Move[i].transform.position, pp, movespeed * Time.deltaTime);

        }

        if (!CanStopDown())
        {
            for (int i = 0; i < m_Move.Count; i++)
            {
                m_Move[i].Init(m_Move[i].X + 1, m_Move[i].Y);
            }
            IsMoveDown = false;
        }
    }

    bool CanStopLeft()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y - 1];

            if (m_Move[i].transform.position != pp)
            {
                return true;
            }
        }
        return false;
    }
    bool CanStopRight()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y + 1];

            if (m_Move[i].transform.position != pp)
            {
                return true;
            }
        }
        return false;
    }

    bool CanStopUp()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X - 1, m_Move[i].Y];

            if (m_Move[i].transform.position != pp)
            {
                return true;
            }
        }
        return false;
    }
    bool CanStopDown()
    {
        for (int i = 0; i < m_Move.Count; i++)
        {
            Vector3 pp = backs[m_Move[i].X + 1, m_Move[i].Y];

            if (m_Move[i].transform.position != pp)
            {
                return true;
            }
        }
        return false;
    }

}

