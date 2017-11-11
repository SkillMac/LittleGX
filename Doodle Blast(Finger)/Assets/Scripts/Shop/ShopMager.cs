using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMager : MonoBehaviour {

    void OnEnable()
    {
        CDataMager.canDraw = false;
    }

    void OnDisable()
    {
        CDataMager.canDraw = true;
    }
}
