using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    };

    public PiecePrefab[] piecePrefabs;
    private Dictionary<PieceType, GameObject> m_Prefabs;

    public int XDim, YDim;//行列

    public float offsetx, offsetY;

    public GameObject background;
    
    int[] output;

    private GamePiece[,] pieces;
    private GameObject[,] backs;
    
    private bool IsMoveLeft, IsMoveRight, IsMoveUp, IsMoveDown;
    public float movespeed;

    public GamePiece gpobj;

    private List<GamePiece> m_Move;

    private void Awake()
    {
        m_Prefabs = new Dictionary<PieceType, GameObject>();
        backs = new GameObject[XDim, YDim];

        for (int i =0;i< piecePrefabs.Length;i++)
        {
            m_Prefabs.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
        }

        //生成背景
        for (int i =0;i<XDim;i++)
        {
            for(int j =0;j<YDim;j++)
            {
                GameObject oob = Instantiate(background, GetWorldPos(i, j), Quaternion.identity);
                oob.transform.parent = transform;
                backs[i, j] = oob;
            }
        }
        
    }
    // Use this for initialization
    void Start () {
        
        GetRandomnum();

        pieces = new GamePiece[XDim, YDim];

        //生成元素
        Tables element = DataManager.tables[TableName.begin];
        
        for (int i = 0; i < XDim; i++)
        {
            for (int j = 0; j < YDim; j++)
            {
                int id = output[i * YDim+j];
                
                int tp = element.GetDataWithIDAndIndex<int>(id.ToString(), 1);
                int dex = element.GetDataWithIDAndIndex<int>(id.ToString(), 2) -1 ;
                
                GameObject oob = Instantiate(m_Prefabs[(PieceType)tp], GetWorldPos(i, j), Quaternion.identity);

                oob.GetComponent<PieceNum>().Setsprite((NumType)dex);

                pieces[i, j] = oob.GetComponent<GamePiece>();
                pieces[i, j].Init(i, j, this, (PieceType)tp);

                oob.transform.parent = transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        if(IsMoveLeft)
        {
            MoveLeft();
        }
        if (IsMoveRight)
        {
            MoveRight();
        }
        if (IsMoveDown)
        {
            MoveDown();
        }
        if (IsMoveUp)
        {
            MoveUp();
        }

    }

    public Vector2 GetWorldPos(int x,int y)
    {
        return new Vector2((y - YDim / 2) * offsetx + transform.position.x,
           (XDim / 2 - x) * offsetY + transform.position.y);
    }

    void GetRandomnum()
    {
        //随机总数组  
        int[] sequence = new int[XDim*YDim];
        //取到的不重复数字的数组长度  
         output = new int[XDim * YDim];

        for (int i = 0; i < XDim * YDim; i++)
        {
            sequence[i] = i;
        }
        int end = XDim * YDim -1;

        for (int i = 0; i < XDim * YDim; i++)
        {
            //随机一个数，每随机一次，随机区间-1  
            int num = Random.Range(0, end + 1);

            output[i] = sequence[num];
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

        if (obj2 == null) return false;

        if(obj1.Type == PieceType.Gold)
        {
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetnewType == obj1.NumComponent.GetnewType)
            {
                Destroy(obj1.gameObject);
                GetBool(poss);

                ReSort(obj1, poss);
                obj2.PlayAnim();
                //这里可以加入消除的特效
                return true;
            }
        }
        if(obj1.Type == PieceType.My)
        {
            if (obj2.Type == PieceType.Enemy && obj2.NumComponent.GetnewType <= obj1.NumComponent.GetnewType)
            {
                Destroy(obj2.gameObject);
                GetBool(poss);

                ReSort(obj2, poss);
                //这里可以加入消除的特效
                return true;
            }
            if (obj2.Type == PieceType.My && obj2.NumComponent.GetnewType == obj1.NumComponent.GetnewType)
            {
                Destroy(obj1.gameObject);
                GetBool(poss);

                ReSort(obj1, poss);
                obj2.PlayAnim();
                //这里可以加入消除的特效
                return true;
            }
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetnewType <= obj1.NumComponent.GetnewType)
            {
                Destroy(obj2.gameObject);
                GetBool(poss);

                ReSort(obj2, poss);
                //这里可以加入消除的特效，同时加分
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
        if(poss.y == 4.0f)
        {
            IsMoveDown = true;
        }
        if(poss.y == -4.0f)
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
    

    public void ReSort(GamePiece ob,Vector3 pos)
    {
        int x = ob.X;
        int y = ob.Y;
        GamePiece newob = Instantiate(gpobj, pos, Quaternion.identity);
        newob.transform.parent = transform;
        
        if(IsMoveLeft)
        {
            for (int i = y; i < YDim; i++)
            {
                if (i != YDim - 1)
                {
                    pieces[x, i] = pieces[x, i + 1];

                    m_Move.Add(pieces[x, i]);
                }

                if (i == YDim - 1)
                {
                    pieces[x, i] = newob;
                    newob.Init(x, i + 1, this, PieceType.My);
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
                    pieces[x, i] = newob;
                    newob.Init(x, i - 1, this, PieceType.My);
                    m_Move.Add(pieces[x, i]);
                }

            }
        }
        if (IsMoveUp)
        {
            for (int i = x; i < XDim; i++)
            {
                if (i != XDim - 1)
                {
                    pieces[i, y] = pieces[ i + 1,y];

                    m_Move.Add(pieces[i, y]);
                }

                if (i == XDim - 1)
                {
                    pieces[i, y] = newob;
                    newob.Init(i+1, y, this, PieceType.My);
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
                    pieces[i, y] = newob;
                    newob.Init(i - 1, y, this, PieceType.My);
                    m_Move.Add(pieces[i, y]);
                }

            }
        }

    }

    public void MoveLeft()
    {
        for(int i =0;i<m_Move.Count;i++)
        {
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y - 1].transform.position;

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
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y + 1].transform.position;

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
            Vector3 pp = backs[m_Move[i].X - 1, m_Move[i].Y].transform.position;

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
            Vector3 pp = backs[m_Move[i].X + 1, m_Move[i].Y].transform.position;

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
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y - 1].transform.position;

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
            Vector3 pp = backs[m_Move[i].X, m_Move[i].Y + 1].transform.position;

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
            Vector3 pp = backs[m_Move[i].X - 1, m_Move[i].Y].transform.position;

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
            Vector3 pp = backs[m_Move[i].X + 1, m_Move[i].Y].transform.position;

            if (m_Move[i].transform.position != pp)
            {
                return true;
            }
        }
        return false;
    }

}

public enum PieceType
{
    Empty,
    My,
    Enemy,
    Gold,
}