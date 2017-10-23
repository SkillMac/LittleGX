using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevButtonScript : MonoBehaviour {
    [HideInInspector]
    public Button m_Button;
    private Animation m_Animation;
    private LevButtonWindow m_ButtonWindow;

    // Use this for initialization
    void Awake ()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickButton);
        m_Animation = GetComponent<Animation>();
    }
	
    private void OnClickButton()
    {
        if (m_ButtonWindow.m_Window != null)
            PlayerPrefs.SetInt("CurrentLev", m_ButtonWindow.m_Count);
        else
            PlayerPrefs.SetInt("CurrentFixLev", m_ButtonWindow.m_Count);
        SceneManager.LoadScene("PlayEditor");
    }

    public void Init(LevButtonWindow m_ButtonWindow)
    {
        this.m_ButtonWindow = m_ButtonWindow;
    }

    public void PlayAnimation()
    {
        m_Animation.Play("Button");
    }

    public void StopAnimation()
    {
        transform.localRotation = Quaternion.identity;
        m_Animation.Stop();
    }
}
