using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Begin : MonoBehaviour {
    public GameObject m_Begin;
    public GameObject m_Levs;

    void OnEnable()
    {
        if (CDataMager.isFirstLoadGame)
        {
            m_Begin.SetActive(true);
            m_Levs.SetActive(false);
            CDataMager.isFirstLoadGame = false;
        }
        else
        {
            m_Begin.SetActive(false);
            m_Levs.SetActive(true);
        }
    }
}
