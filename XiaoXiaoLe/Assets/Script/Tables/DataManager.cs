using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{

    public static Dictionary<TableName, Tables> tables;

    void Awake()
    {
        tables = new Dictionary<TableName, Tables>();

        tables.Add(TableName.prefabtype, new Tables("Tab/prefabtype"));
        tables.Add(TableName.maps, new Tables("Tab/Maps"));
        tables.Add(TableName.Score, new Tables("Tab/Score"));
    }
}
