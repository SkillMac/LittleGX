using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteList {

    private static List<Transform[]> list = new List<Transform[]>();

    public static List<Transform[]> GetList
    {
        set
        {
            list = value;
        }
        get
        {
            return list;
        }
    }
    
}
