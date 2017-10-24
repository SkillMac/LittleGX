using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour {
    public Text m_LevName;
    public Button m_Start;
    private WindowUIMager m_UIMager;

    void Awake()
    {
        m_Start.onClick.AddListener(OnClickButton);
    }

    void Start()
    {
        m_LevName.text = m_UIMager.m_ObjMager.myLevIndex.ToString();
    }

    void OnEnable()
    {
        CDataMager.canDraw = false;
        WindowUIMager.hasStartGame = false;
    }

    private void OnClickButton()
    {
        gameObject.SetActive(false);
        CDataMager.canDraw = true;
    }

    public void Init(WindowUIMager m_UIMager)
    {
        this.m_UIMager = m_UIMager;
        gameObject.SetActive(true);
    }
}
