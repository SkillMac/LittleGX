using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{
    public static Dictionary<TableName, Tables> tables;

    void Awake()
    {
        tables = new Dictionary<TableName, Tables>();

        tables.Add(TableName.begin, new Tables("Tab/Begin"));
        tables.Add(TableName.creat, new Tables("Tab/Creat"));
    }
}
