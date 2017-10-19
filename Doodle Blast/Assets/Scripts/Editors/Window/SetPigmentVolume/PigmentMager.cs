using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigmentMager : MonoBehaviour {
    public DrawLines m_Draw;
    public PigmentWindow m_Window;
    public ButtonBottle m_Bottle;

    void Awake()
    {
        m_Bottle = GetComponent<ButtonBottle>();
        m_Draw.maxPigmentLength = CDataMager.getInstance.myPigmentVolume;
        m_Window.Init(this);
        CDataMager.getInstance.myPigment = this;
    }
   
    public void InitAllData()
    {
        ReSetAllLines();
        m_Window.m_Volume.text = "10";
    }

    public void ReSetAllLines()
    {
        m_Draw.DeleteAllLines();
    }
}
