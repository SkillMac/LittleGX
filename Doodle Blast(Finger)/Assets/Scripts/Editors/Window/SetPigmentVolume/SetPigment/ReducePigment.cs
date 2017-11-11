using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReducePigment : MonoBehaviour {
    private Button m_Button;
    private PigmentWindow m_Window;

    public void Init(PigmentWindow window)
    {
        m_Window = window;
    }
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Onclickbutton);
    }

    private void Onclickbutton()
    {
        if (CDataMager.getInstance.myPigmentVolume > 0 && m_Window.m_Mager.m_Draw.CanDrawLine())
        {
            m_Window.m_Mager.m_Draw.maxPigmentLength--;
            CDataMager.getInstance.myPigmentVolume--;
        }
        else if(CDataMager.getInstance.myPigmentVolume <= 0)
        {
            CDataMager.getInstance.myPigmentVolume = 0;
            m_Window.m_Mager.m_Draw.maxPigmentLength = 0;
        }
        m_Window.m_Volume.text = CDataMager.getInstance.myPigmentVolume.ToString();
        float temp = m_Window.m_Mager.m_Draw.SetPigmentImage();
        m_Window.m_Mager.m_Draw.m_Pigment.SetImageValue(temp);
    }
}
