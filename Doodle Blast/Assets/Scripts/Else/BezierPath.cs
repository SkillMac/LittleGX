﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath  {

    public List<Vector3> pathPoints;
    private int segments;
    public int pointCount;

    public BezierPath()
    {
        pathPoints = new List<Vector3>();
        pointCount = 100;//最大点数  
    }

    public void DeletePath()
    {
        pathPoints.Clear();
    }
    //t就是两点之间几分之几的位置  
    Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float tt = t * t;
        float ttt = t * tt;
        float u = 1.0f - t;
        float uu = u * u;
        float uuu = u * uu;

        Vector3 B = new Vector3();
        B = uuu * p0;
        B += 3.0f * uu * t * p1;
        B += 3.0f * u * tt * p2;
        B += ttt * p3;

        return B;
    }

    public List<Vector3> CreateCurve(List<Vector3> controlPoints)
    {
        segments = controlPoints.Count / 3;//以3为间隔进行平滑 算出分段数量  
        pointCount = controlPoints.Count;//这里最大点数就是本身  
        for (int s = 0; s < controlPoints.Count - 3; s += 3)//以3为间隔遍历所有点  
        {
            Vector3 p0 = controlPoints[s];
            Vector3 p1 = controlPoints[s + 1];
            Vector3 p2 = controlPoints[s + 2];
            Vector3 p3 = controlPoints[s + 3];
            //第一个点的处理  
            if (s == 0)
            {
                pathPoints.Add(BezierPathCalculation(p0, p1, p2, p3, 0.0f));
            }
            //  
            for (int p = 0; p < (pointCount / segments); p++)
            {
                float t = (1.0f / (pointCount / segments)) * p;
                Vector3 point = new Vector3();
                point = BezierPathCalculation(p0, p1, p2, p3, t);

                pathPoints.Add(point);
            }
        }
        return pathPoints;
    }
}
