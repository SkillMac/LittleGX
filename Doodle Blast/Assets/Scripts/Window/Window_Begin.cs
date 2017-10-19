using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Begin : MonoBehaviour {
    public GameObject m_Begin;
    public GameObject m_EditorLevs;

    void OnEnable()
    {
        if(CDataMager.isFirstLoadGame)
        {
            m_Begin.SetActive(true);
            m_EditorLevs.SetActive(false);
            CDataMager.isFirstLoadGame = false;
        }
        else
        {
            m_Begin.SetActive(false);
            m_EditorLevs.SetActive(true);
        }
    }
}
