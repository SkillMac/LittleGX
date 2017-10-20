using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigmentWindow : MonoBehaviour {
    public AddPigment m_Add;
    public ReducePigment m_Reduce;
    public DeleteBeforeLine m_Before;
    public DeleteAllLines m_All;
    public ResetAll m_Reset;
    public BeginTest m_Test;
    public Text m_Volume;
    public SaveData m_SaveData;
    [HideInInspector]
    public PigmentMager m_Mager;
    private GetUIVertexs m_Vertexs;

    public void Init(PigmentMager mager)
    {
        m_Mager = mager;
    }
    
	void Start () {
        m_Vertexs = GetComponent<GetUIVertexs>();
        m_Add.Init(this);
        m_Reduce.Init(this);
        m_Before.Init(this);
        m_All.Init(this);
        m_Reset.Init(this);
        m_Test.Init(this);
        m_SaveData.Init(this);
    }
}
