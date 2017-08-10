using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {

    public static DrawLine Instance;

    private List<Vector3> list;

    private bool IsDraw = false;

    private LineRenderer lineRenderer;

    private EdgeCollider2D el2d;

    public GameObject prefabs;//线

    private GameObject current;//当前的线

    List<Vector2> Allvert;//存储所有的顶点
    Vector2[] AllVer;//存储所有的顶点

    public static float m_Lenght;//当前线的长度

    public float max_Lenght;//当前关卡最大的长度

    private float oldLenght;

    private Dictionary<GameObject, float> m_Draw;//存储所有画出的线以及相应的长度
    private List<GameObject> m_draw;

    public Window_canvas canvas;

    public bool isBegin = true;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_Draw = new Dictionary<GameObject, float>();
        m_draw = new List<GameObject>();
    }

    //确保激活的时候从新绘制
    private void OnEnable()
    {
        IsDraw = false;
        
        m_Lenght  = Window_canvas.current_Lenght;

        oldLenght = m_Lenght;
    }
    private void OnDisable()
    {
        if (Allvert == null || el2d == null || Allvert.Count <= 5)
            return;
        AllVer = new Vector2[Allvert.Count];

        el2d.points = new Vector2[Allvert.Count];

        for (int i = 0; i < Allvert.Count; i++)
        {
            AllVer[i] = Allvert[i];
        }
        el2d.points = AllVer;

        if (el2d.pointCount <= 5)
        {
            Destroy(current);
            return;
        }

        if (!m_Draw.ContainsKey(current))
        {
            //错点
            m_Draw.Add(current, GetDis(Allvert));
        }

        if (!m_draw.Contains(current))
        {
            m_draw.Add(current);
        }
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBegin)
        {
             oldLenght = m_Lenght;

            GameObject obj = Instantiate(prefabs);
            obj.transform.parent = transform;

            current = obj;

            lineRenderer = obj.GetComponent<LineRenderer>();
            el2d = obj.GetComponent<EdgeCollider2D>();

            if (list == null)
                list = new List<Vector3>();

            list.Clear();

            IsDraw = true;
            lineRenderer.positionCount = 0;

            Allvert = new List<Vector2>();
        }
        if (Input.GetMouseButton(0) && IsDraw)
        {
            list.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsDraw = false;
            
            if (Allvert == null || Allvert.Count <= 5)
            {
                Destroy(current);
                Debug.Log("Empty");
                return;
            }

            AllVer = new Vector2[Allvert.Count];

            el2d.points = new Vector2[Allvert.Count];

            for (int i = 0; i < Allvert.Count; i++)
            {
                AllVer[i] = Allvert[i];
            }
            el2d.points = AllVer;

            if (el2d.pointCount <= 5)
            {
                Destroy(current);
                return;
            }

            if (!m_Draw.ContainsKey(current))
            {
                m_Draw.Add(current, GetDis(Allvert));
            }

            if(!m_draw.Contains(current))
            {
                m_draw.Add(current);
            }

        }
      
        drawBezierCurve();
    }
    
    //使用贝塞尔的情况  
    private void drawBezierCurve()
    {
        if (IsDraw && list.Count > 5 && (m_Lenght<= max_Lenght))
        {
            List<Vector3> bcList;

            BezierPath bc = new BezierPath();
            
            bcList = bc.CreateCurve(list);//  通过贝塞尔曲线 平滑  

            lineRenderer.positionCount = bcList.Count;

            for (int i = 0; i < bcList.Count; i++)
            {
                Vector3 v = bcList[i];
                v += new Vector3(0, 0, 0);
                lineRenderer.SetPosition(i, v);

                Vector2 pos = new Vector2(v.x, v.y);

                if (!Allvert.Contains(pos))
                    Allvert.Add(pos);
                
            }
            
            m_Lenght = oldLenght + GetDis(Allvert);
         
            Debug.Log(m_Lenght);

            if (max_Lenght != 0)
            {
                 canvas.SetBut(m_Lenght / max_Lenght);
            }
        }

    }
    //普通没有使用贝塞尔的情况  
    private void drawInputPointCurve()
    {
        if (IsDraw && list.Count > 0)
        {
            lineRenderer.positionCount = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                Vector3 v = list[i];
                v += new Vector3(0, 0.5f, 0);
                lineRenderer.SetPosition(i, v);
            }

        }
    }
    //获取当前已经画线的长度
    private float GetDis(List<Vector2> list)
    {
        float dis = 0;

        if (list.Count == 0) return 0;

        for(int i =1;i<list.Count;i++)
        {
            float ds = Mathf.Abs(Vector2.Distance(list[i - 1], list[i]));

            dis += ds;
        }

        return dis;
    }
    //清除前一个线条
    public void Undo()
    {
        deleteBefor();
    }

    //清除所有的线条
    public void ClearAll()
    {
        Clear();
    }

    //清除前一个线条
    private void deleteBefor()
    {
        if (m_draw.Count == 0) return;

        GameObject obj = m_draw[m_draw.Count - 1];

        //????????????????
        m_Lenght -= m_Draw[obj];

        canvas.SetBut(m_Lenght / max_Lenght);

        m_Draw.Remove(obj);
        m_draw.Remove(obj);


        Destroy(obj);
    }

    //清除所有的线条
    private void Clear()
    {
        if (m_draw.Count == 0) return;

        for(int i =0; i< m_draw.Count;i++)
        {
            Destroy(m_draw[i]);
        }
        m_Lenght = 0;
        canvas.SetBut(m_Lenght / max_Lenght);

        m_draw.Clear();
        m_Draw.Clear();
    }

    public void SetBool()
    {
        isBegin = false;
    }
}
