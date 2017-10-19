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
        m_LevName.text = PlayerPrefs.GetInt("CurrentLev").ToString();
        m_UIMager.m_Draw.gameObject.SetActive(false);
        m_Start.onClick.AddListener(OnClickButton);
    }

    void OnEnable()
    {
        m_UIMager.m_Draw.gameObject.SetActive(false);
        WindowUIMager.hasStartGame = false;
    }

    private void OnClickButton()
    {
        gameObject.SetActive(false);
        m_UIMager.m_Draw.gameObject.SetActive(true);
    }

    public void Init(WindowUIMager m_UIMager)
    {
        this.m_UIMager = m_UIMager;
    }
}
