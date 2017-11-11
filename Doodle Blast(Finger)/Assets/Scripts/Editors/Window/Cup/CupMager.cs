using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupMager : MonoBehaviour {
    public EditorCup currentCup;
    public EditorCupWinLine currentCupWinLine;
    public JudgeCanWin m_JudgeMager;
    private Vector3 oldCupPos;
    private Vector3 oldWinLinePos;
    void Awake()
    {
        m_JudgeMager = GetComponent<JudgeCanWin>();
        oldCupPos = currentCup.transform.position;
        oldWinLinePos = currentCupWinLine.transform.position;
        CDataMager.getInstance.myCup = this;
    }

    public void InitAllCupData()
    {
        InitWaterData();
        currentCup.transform.position = oldCupPos;
        currentCupWinLine.transform.position = oldWinLinePos;
        CDataMager.getInstance.cupPositionX = oldCupPos.x;
        CDataMager.getInstance.winLinePositionY = oldWinLinePos.y;
    }

    public void InitWaterData()
    {
        m_JudgeMager.water.InitData();
    }
}
