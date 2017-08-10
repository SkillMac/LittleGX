using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{

    public static Dictionary<TableName, Tables> tables;

    void Awake()
    {
        //设置固定分辨率
        //Screen.SetResolution(800, 1280, true);
        //Screen.fullScreen = true;

        tables = new Dictionary<TableName, Tables>();

        tables.Add(TableName.begin, new Tables("Tab/Begin"));
        tables.Add(TableName.creat, new Tables("Tab/Creat"));
    }
}
