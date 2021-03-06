﻿using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {
    public GameObject prefabs;
    public MyPigment m_Pigment;
    public GetSpriteVertexs[] allSpriteRegions;
    public GetUIVertexs[] allUIRegions;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private bool isBeginDraw = false;
    private bool mayDrawLine = true;
    private bool isBeginInObj = false;
    private List<Vector3> allMousePoint;
    private List<Vector2> allVertices;//存储所有的顶点
    private float currentLength;
    private float oldLength;
    private GameObject current;
    private Dictionary<GameObject, float> m_AllLinesAndLength = new Dictionary<GameObject, float>();//存储所有画出的线以及相应的长度
    private List<GameObject> m_AllLines = new List<GameObject>();//存储所有画出的线
    [HideInInspector]
    public int maxPigmentLength;
    public Transform m_Tip;
    public GameObject m_Png;
    private Vector3 offsetPos;

    void Start()
    {
        offsetPos = m_Png.transform.GetChild(0).position;
        Debug.Log(offsetPos);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!CDataMager.canDraw) return;
        if (Input.GetMouseButtonDown(0) && mayDrawLine)
        {
            m_Png.SetActive(false);
            Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) + offsetPos;
            if (JudgeInObj(temp))
            {
                isBeginInObj = true;
            }
            else
            {
                isBeginInObj = false;
            }
            if (!CanDrawLine() && !isBeginInObj)
            {
                if(!m_Tip.GetComponent<Animation>().isPlaying)
                    m_Tip.gameObject.SetActive(true);
                return;
            }
            m_Png.transform.position = temp - offsetPos;
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
        if (Input.GetMouseButton(0) && isBeginDraw && CanDrawLine() && mayDrawLine)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            m_Png.transform.position = temp;
            if (JudgeInObj(temp + offsetPos))
            {
                if (!isBeginInObj)
                {
                    mayDrawLine = false;
                    Vector3 tempPos = (allMousePoint[allMousePoint.Count - 1] + temp) / 2f;
                    //allMousePoint.Add(tempPos + offsetPos);
                }
                m_Png.SetActive(false);
            }
            else
            {
                if (!allMousePoint.Contains(temp + offsetPos))
                    allMousePoint.Add(temp + offsetPos);
                m_Png.SetActive(true);
            }
            
            DrawBezierCurve();
        }
        if (Input.GetMouseButtonUp(0) && isBeginDraw)
        {
            m_Png.SetActive(false);
            isBeginDraw = false;
            mayDrawLine = true;
            //DeleteUnUsePoints();
            //DrawBezierCurve();
            ProduceLines();
        }
    }
    
    //private void DeleteUnUsePoints()
    //{
    //    currentLength = oldLength;
    //    m_Pigment.SetImagePigment(SetPigmentImage());
    //    allVertices = new List<Vector2>();
    //    if (allMousePoint.Count>0)
    //    {
    //        if(JudgeInObj(allMousePoint[0]))
    //        {
    //            for(int i = 0;i <allMousePoint.Count;i++)
    //            {
    //                if(!JudgeInObj(allMousePoint[i]))
    //                {
    //                    Vector3 tempPos = (allMousePoint[i - 1] + allMousePoint[i]) / 2f;
    //                    allMousePoint.RemoveRange(0, i);
    //                    allMousePoint.Insert(0, tempPos);
    //                    return;
    //                }
    //            }
    //            allMousePoint.Clear();
    //        }
    //        else
    //        {
    //            for (int i = 0; i < allMousePoint.Count; i++)
    //            {
    //                if (JudgeInObj(allMousePoint[i]))
    //                {
    //                    Vector3 tempPos = (allMousePoint[i - 1] + allMousePoint[i]) / 2f;
    //                    allMousePoint.RemoveRange(i, allMousePoint.Count - i);
    //                    allMousePoint.Add(tempPos);
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}

    private void ProduceLines()
    {
        if (allVertices == null || allVertices.Count <= 2)
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
        allVertices = new List<Vector2>();
        current = null;
    }
    
    private void DrawBezierCurve()
    {
        if (allMousePoint.Count < 2) return;
        lineRenderer.positionCount = allMousePoint.Count;
        for (int i = 0; i < allMousePoint.Count; i++)
        {
            Vector3 temp = allMousePoint[i];
            lineRenderer.SetPosition(i, temp);
            Vector2 pos = new Vector2(temp.x, temp.y);
            if (!allVertices.Contains(pos))
                allVertices.Add(pos);
        }
        float tempLength = oldLength + GetDrawLineDistance(allVertices);
        currentLength = Mathf.Clamp(tempLength, 0, maxPigmentLength);
        m_Pigment.SetImageValue(SetPigmentImage());
    }
    
    private bool JudgeInObj(Vector3 pos)
    {
        if(allSpriteRegions.Length >0)
        {
            for(int i =0; i< allSpriteRegions.Length;i++)
            {
                if (allSpriteRegions[i].IsInCup(pos))
                    return true;
            }
        }
        if(allUIRegions.Length >0)
        {
            for(int i=0;i<allUIRegions.Length;i++)
            {
                if (allUIRegions[i].IsInUIWindow(pos))
                    return true;
            }
        }
        return false;
    }

    public float SetPigmentImage()
    {
        return (1 - currentLength / maxPigmentLength);
    }
    
    public bool CanDrawLine()
    {
        if (currentLength < maxPigmentLength) return true;
        else
        {
            m_Png.SetActive(false);
            return false;
        }
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
            m_Pigment.SetImageValue(1);
            return;
        }
        GameObject obj = m_AllLines[m_AllLines.Count - 1];
        currentLength -= m_AllLinesAndLength[obj];
        currentLength = oldLength = Mathf.Clamp(currentLength, 0, maxPigmentLength);
        m_Pigment.SetImageValue(SetPigmentImage());
        m_AllLinesAndLength.Remove(obj);
        m_AllLines.Remove(obj);
        Destroy(obj);
        if (m_AllLines.Count == 0)
        {
            currentLength = oldLength = 0;
            m_Pigment.SetImageValue(1);
        }
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
        m_Pigment.SetImageValue(1);
        m_AllLines.Clear();
        m_AllLinesAndLength.Clear();
    }
    
}
