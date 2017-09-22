using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    void Awake()
    {
        LoadDataClass.Instace.Init();
    }
}
