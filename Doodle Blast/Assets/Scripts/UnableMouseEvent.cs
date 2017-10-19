using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnableMouseEvent : MonoBehaviour
{
    public DrawLines m_Draw;
    public GameObject m_Top;
    public JudgeCanWin m_Cup;
    private bool canEnable = true;
    
    public void SetDraw(bool istrue)
    {
        m_Draw.enabled = istrue;
    }
    
}
