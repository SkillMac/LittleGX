using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {
    public GameObject prefabs;
    public MyPigment m_Pigment;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private bool isBeginDraw = false;
    private List<Vector3> allMousePoint;
    private List<Vector2> allVertices;//存储所有的顶点
    private float currentLength;
    private float oldLength;
    private GameObject current;
    private Dictionary<GameObject, float> m_AllLinesAndLength = new Dictionary<GameObject, float>();//存储所有画出的线以及相应的长度
    private List<GameObject> m_AllLines = new List<GameObject>();//存储所有画出的线
    [HideInInspector]
    public int maxPigmentLength;
    
    void OnEnable()
    {
        isBeginDraw = false;
        oldLength = currentLength;
    }
    void OnDisable()
    {
        ProduceLines();
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0) && CanDrawLine())
        {
            oldLength = currentLength;
            GameObject obj = Instantiate(prefabs);
            obj.transform.parent = transform;
            lineRenderer = obj.GetComponent<LineRenderer>();
            edgeCollider = obj.GetComponent<EdgeCollider2D>();
            current = obj;

            allMousePoint = new List<Vector3>();
            lineRenderer.positionCount = 0;
            isBeginDraw = true;
            allVertices = new List<Vector2>();
        }
        if (Input.GetMouseButton(0) && isBeginDraw && CanDrawLine())
        {
            allMousePoint.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }
        if (Input.GetMouseButtonUp(0))
        {
            isBeginDraw = false;
            ProduceLines();
        }
        DrawBezierCurve();
    }
    
    private void ProduceLines()
    {
        if (allVertices == null || allVertices.Count <= 5)
        {
            if (currentLength - oldLength < 0.1f)
            {
                currentLength = oldLength;
            }
            Destroy(current);
            return;
        }
        Vector2[] temp = new Vector2[allVertices.Count];
        if (edgeCollider == null) return;

        edgeCollider.points = new Vector2[allVertices.Count];
        for (int i = 0; i < allVertices.Count; i++)
        {
            temp[i] = allVertices[i];
        }
        edgeCollider.points = temp;

        if (!m_AllLinesAndLength.ContainsKey(current))
        {
            m_AllLinesAndLength.Add(current, GetDrawLineDistance(allVertices));
        }

        if (!m_AllLines.Contains(current))
        {
            m_AllLines.Add(current);
        }
    }

    private void DrawBezierCurve()
    {
        if (isBeginDraw && allMousePoint.Count > 5 && currentLength<= maxPigmentLength)
        {
            List<Vector3> bcList = new List<Vector3>();
            BezierPath bc = new BezierPath();
            bcList = bc.CreateCurve(allMousePoint);//  通过贝塞尔曲线 平滑  
            lineRenderer.positionCount = bcList.Count;

            for (int i = 0; i < bcList.Count; i++)
            {
                Vector3 temp = bcList[i];
                lineRenderer.SetPosition(i, temp);
                Vector2 pos = new Vector2(temp.x, temp.y);
                if (!allVertices.Contains(pos))
                    allVertices.Add(pos);
            }
            float tempLength = oldLength + GetDrawLineDistance(allVertices);
            currentLength = Mathf.Clamp(tempLength, 0, maxPigmentLength);
            m_Pigment.SetImagePigment(SetPigmentImage());
        }
    }
    
    public float SetPigmentImage()
    {
        return (1 - currentLength / maxPigmentLength);
    }
    
    public bool CanDrawLine()
    {
        if (currentLength < maxPigmentLength) return true;
        else return false;
    }

    /// <summary>
    /// 获取当前已经画线的长度
    /// </summary>
    /// <param name="顶点坐标"></param>
    /// <returns></returns>
    private float GetDrawLineDistance(List<Vector2> lst)
    {
        float distance = 0;
        if (lst.Count == 0) return 0;
        for (int i = 1; i < lst.Count; i++)
        {
            float value = Mathf.Abs(Vector2.Distance(lst[i - 1], lst[i]));
            distance += value;
        }
        return distance;
    }

    //清除前一个线条
    public void DeleteBeforeLine()
    {
        //Debug.Log(currentLength);
        if (m_AllLines.Count == 0)
        {
            currentLength = oldLength = 0;
            return;
        }
        GameObject obj = m_AllLines[m_AllLines.Count - 1];
        currentLength -= m_AllLinesAndLength[obj];
        currentLength = oldLength = Mathf.Clamp(currentLength, 0, maxPigmentLength);
        m_Pigment.SetImagePigment(SetPigmentImage());
        m_AllLinesAndLength.Remove(obj);
        m_AllLines.Remove(obj);
        Destroy(obj);
        if (m_AllLines.Count == 0) currentLength = oldLength = 0;
    }

    //清除所有的线条
    public void DeleteAllLines()
    {
        if (m_AllLines.Count == 0) return;
        for (int i = 0; i < m_AllLines.Count; i++)
        {
            Destroy(m_AllLines[i]);
        }
        currentLength = oldLength = 0;
        m_Pigment.SetImagePigment(1);
        m_AllLines.Clear();
        m_AllLinesAndLength.Clear();
    }
    
}
