using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    };
    public PiecePrefab[] piecePrefabs;
    private Dictionary<PieceType, GameObject> m_Prefabs = new Dictionary<PieceType, GameObject>();
    private const int rowCount =5;
    private const int colCount =5;
    private const float OFFSETX = 1.4f;
    private const float OFFSETY = 1.8f;
    public GameObject background;
    private int[] rangeID;
    private int[] rolePob = { 9,10,6};
    private int[] levPob = {23,2};
    private int[] creatPob = { 4,2,4};
    private List<int> lstRole = new List<int>();
    private List<int> lstLev = new List<int>();
    private List<int> lstCreat = new List<int>();

    private GamePiece[,] pieces;
    private Vector2[,] backs = new Vector2[rowCount, colCount];
    
    private bool IsMoveLeft, IsMoveRight, IsMoveUp, IsMoveDown;
    public float movespeed;
    
    private List<GamePiece> m_Move;

    private static int HighDex;//记录最高分数的物体

    public Text text_Score;

    public GameObject window_over;
    
    public GameObject GoldEffect, EnemyEffect;
    

    private void Awake()
    {
        InitLstRole();
        InitLstLev();
        InitLstCreat();
        HighDex = 1;

        for (int i =0;i< piecePrefabs.Length;i++)
        {
            m_Prefabs.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
        }

        GetRandomnum();
        pieces = new GamePiece[rowCount, colCount];
        
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int id = rangeID[i * colCount + j];
                int tp = lstRole[id];
                int dex = lstLev[id];
                Vector2 tempPos = GetWorldPos(i, j);
                backs[i, j] = tempPos;
                GameObject oob = Instantiate(m_Prefabs[(PieceType)tp], tempPos, Quaternion.identity);
                oob.GetComponent<PieceNum>().Setsprite((NumType)dex);
                pieces[i, j] = oob.GetComponent<GamePiece>();
                pieces[i, j].Init(i, j, this, (PieceType)tp);
                oob.transform.parent = transform;
            }
        }
    }

    private void InitLstRole()
    {
        for(int i =0;i<rolePob.Length;i++)
        {
            for(int j =0;j<rolePob[i];j++)
            {
                lstRole.Add(i);
            }
        }
    }

    private void InitLstLev()
    {
        for(int i =0;i<levPob.Length;i++)
        {
            for(int j =0;j<levPob[i];j++)
            {
                lstLev.Add(i);
            }
        }
    }

    private void InitLstCreat()
    {
        for (int i = 0; i < creatPob.Length; i++)
        {
            for (int j = 0; j < creatPob[i]; j++)
            {
                lstCreat.Add(i);
            }
        }
    }

    // Use this for initialization
    void Start () {
        
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

        if(IsOver() == null && !window_over.activeSelf)
        {
            window_over.SetActive(true);
            Debug.Log("GameOver");

            SetUnActive();
        }

        //闪烁效果
        //if(Input.GetMouseButtonUp(0) && !window_over.activeSelf)
        //{
        //    timer = Time.realtimeSinceStartup;
        //}

        //if(Input.GetMouseButtonDown(0) && !window_over.activeSelf)
        //{
        //    if (current != null)
        //    {
        //        current.SetFalse();
        //        timer = Time.realtimeSinceStartup;
        //    }
        //}
        //if(IsOver() != null &&Time.realtimeSinceStartup - timer > 5.0f)
        //{
        //    current = IsOver();
        //    current.SetTrue();
        //}
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
                Instantiate(EnemyEffect, obj2.transform.position, Quaternion.identity);
                return true;
            }
            if (obj2.Type == PieceType.My && obj2.NumComponent.GetnewType == obj1.NumComponent.GetnewType)
            {
                Destroy(obj1.gameObject);
                GetBool(poss);

                if(obj2.NumComponent.GetnewType >= (NumType)(HighDex-1))
                    HighDex ++;

                ReSort(obj1, poss);
                obj2.PlayAnim();
                //这里可以加入消除的特效
                return true;
            }
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetnewType <= obj1.NumComponent.GetnewType)
            {
                Destroy(obj2.gameObject);
                GetBool(poss);

                int scor = (int)(obj2.NumComponent.GetnewType + 1) * 3;

                text_Score.GetComponent<ScoreAnim>().AddScores(scor);

                //text_Score.text = ((int)(obj2.NumComponent.GetnewType + 1) * 3 + scor).ToString();

                ReSort(obj2, poss);
                //这里可以加入消除的特效，同时加分
                Instantiate(GoldEffect, obj2.transform.position, Quaternion.identity);

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
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetnewType == obj1.NumComponent.GetnewType)
            {
                return true;
            }
        }
        if (obj1.Type == PieceType.My)
        {
            if (obj2.Type == PieceType.Enemy && obj2.NumComponent.GetnewType <= obj1.NumComponent.GetnewType)
            {
                return true;
            }
            if (obj2.Type == PieceType.My && obj2.NumComponent.GetnewType == obj1.NumComponent.GetnewType)
            {
                return true;
            }
            if (obj2.Type == PieceType.Gold && obj2.NumComponent.GetnewType <= obj1.NumComponent.GetnewType)
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
        if(poss.y == -5.8f)
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
    /// 根据当前物体随机获取其他颜色的物体，红色的最大为当前最高的记录
    /// </summary>
    /// <param name="obj"></param>
    void GetCurrenNum(GameObject obj)
    {
        if(obj.GetComponent<GamePiece>().Type == PieceType.Gold || obj.GetComponent<GamePiece>().Type == PieceType.My)
        {
            obj.transform.GetComponent<PieceNum>().Setsprite(NumType.num0001);
        }
        if(obj.GetComponent<GamePiece>().Type == PieceType.Enemy)
        {
            if(HighDex >=3)
            {
                int dex = Random.Range(0, HighDex);

                //红色高等级物体出现的概率
                //if(dex>=1 && dex<=10)
                //{
                //    obj.transform.GetComponent<PieceNum>().Setsprite((NumType)(HighDex - 1));
                //    Debug.Log(111);
                //}
                //else
                //{
                obj.transform.GetComponent<PieceNum>().Setsprite((NumType)dex);
                //obj.transform.GetComponent<PieceNum>().Setsprite(NumType.num0008);
                // }
            }
            else
            {
                obj.transform.GetComponent<PieceNum>().Setsprite(NumType.num0001);
            }

        }
    }

    //排序
    public void ReSort(GamePiece ob,Vector3 pos)
    {
        int x = ob.X;
        int y = ob.Y;
        int id0 = Random.Range(0, 9);
        int ty = lstCreat[id0];
        GameObject newobj = Instantiate(m_Prefabs[(PieceType)ty], pos, Quaternion.identity);
        GamePiece newob = newobj.GetComponent<GamePiece>();
        newob.Init(0,0, this, (PieceType)ty);
        GetCurrenNum(newobj);
        newob.transform.parent = transform;
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
                    pieces[x, i] = newob;
                    newob.Init(x, i + 1, this, (PieceType)ty);
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
                    newob.Init(x, i - 1, this, (PieceType)ty);
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
                    pieces[i, y] = newob;
                    newob.Init(i+1, y, this, (PieceType)ty);
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
                    newob.Init(i - 1, y, this, (PieceType)ty);
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

public enum PieceType
{
    My,
    Gold,
    Enemy,
}