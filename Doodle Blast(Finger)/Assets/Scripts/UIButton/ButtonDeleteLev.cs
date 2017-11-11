using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDeleteLev : MonoBehaviour {
    private Button m_Button;
    private LevButtonWindow m_ButtonWindow;

	// Use this for initialization
	void Start () {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickEvent);
	}
	
    private void OnClickEvent()
    {
        int tempCount = m_ButtonWindow.m_Count;
        CAllEditorLevs.GetInstance.allEditorLevs.Remove(tempCount);
        CAllEditorLevs.GetInstance.allLevID.Remove(tempCount);
        m_ButtonWindow.m_Window.SetButtonText();

        CAllLevsData cld = CFileMager.GetInstance.ReadFiles(Application.persistentDataPath + "/levelConfig.txt");
        for(int i =0;i<cld.allEditorLevs.Count;i++)
        {
            if (cld.allEditorLevs[i].currentLevID == tempCount)
                cld.allEditorLevs.Remove(cld.allEditorLevs[i]);
        }
        CFileMager.GetInstance.WriteFiles(cld, Application.persistentDataPath + "/levelConfig.txt");
    }

    public void Init(LevButtonWindow m_ButtonWindow)
    {
        this.m_ButtonWindow = m_ButtonWindow;
    }
}
