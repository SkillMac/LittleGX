using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceNum : MonoBehaviour {
    private SpriteRenderer sprite;
    public int m_Lev;
    public int GetCurrentLev
    {
        set{ m_Lev = value; }
        get{ return m_Lev; }
    }
    public PieceType type;
    public PieceType Type
    {
        set { type = value; }
        get { return type; }
    }

    private void Awake()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Setsprite(int lev, PieceType pt)
    {
        GetCurrentLev = lev;
        switch (pt)
        {
            case PieceType.My:
                sprite.sprite = LoadDataClass.lstAllMySprites[lev];
                break;
            case PieceType.Gold:
                sprite.sprite = LoadDataClass.lstAllGoldSprites[lev];
                break;
            case PieceType.Enemy:
                sprite.sprite = LoadDataClass.lstAllEnemySprites[lev];
                break;
        }
    }
}
