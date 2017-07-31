using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{

    public static Dictionary<TableName, Tables> tables;

    void Awake()
    {
        Screen.SetResolution(725, 1280, true);
        Screen.fullScreen = false;

        tables = new Dictionary<TableName, Tables>();

        tables.Add(TableName.prefabtype, new Tables(Application.dataPath + "/Resources/Tab/prefabtype.txt"));
        tables.Add(TableName.maps, new Tables(Application.dataPath + "/Resources/Tab/Maps.txt"));
        tables.Add(TableName.Score, new Tables(Application.dataPath + "/Resources/Tab/Score.txt"));
    }
}
