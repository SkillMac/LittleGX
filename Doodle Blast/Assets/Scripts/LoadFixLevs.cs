using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFixLevs : MonoBehaviour {
    public GameObject rootButton;
    public LevButtonWindow prefabButton;
    private int count;
    private int currentLev;
    private List<LevButtonWindow> allLevButtons;

    void Awake()
    {
        allLevButtons = new List<LevButtonWindow>();

        if (CDataMager.getInstance.allSphereSprite == null)
        {
            CDataMager.getInstance.allSphereSprite = Resources.LoadAll<Sprite>("Images/Spheres");
        }
        if (CDataMager.getInstance.allCubeSprite == null)
        {
            CDataMager.getInstance.allCubeSprite = Resources.LoadAll<Sprite>("Images/Cubes");
        }

        CreatButton();
        LoadAllFixLevs();
    }

    void OnEnable()
    {
        CAllFixedLev.isFixedLev = true;
        currentLev = PlayerPrefs.GetInt("MaxFixLev") - 1;
        for (int i = 0; i < count; i++)
        {
            if (i <= currentLev || i ==0)
            {
                allLevButtons[i].m_Lock.SetActive(false);
                allLevButtons[i].m_LevButton.m_Button.interactable = true;
            }
            else
            {
                allLevButtons[i].m_Lock.SetActive(true);
                allLevButtons[i].m_LevButton.m_Button.interactable = false;
            }
        }
    }

    private void CreatButton()
    {
        count = Resources.LoadAll("Data/EditorData").Length;
        for (int i = 0; i < count; i++)
        {
            LevButtonWindow lbw = Instantiate(prefabButton, rootButton.transform);
            lbw.m_Text.text = (i + 1).ToString();
            lbw.m_Delete.gameObject.SetActive(false);
            allLevButtons.Add(lbw);
        }
    }

    private void LoadAllFixLevs()
    {
        for(int i =0;i<count;i++)
        {
            TextAsset ta = Resources.Load<TextAsset>("Data/EditorData/" + (i+1));
            CLevel current = JsonUtility.FromJson<CLevel>(ta.ToString());
            if(!CAllFixedLev.GetInstance.allFixedLevs.ContainsKey(current.currentLevID))
                CAllFixedLev.GetInstance.allFixedLevs.Add(current.currentLevID, current);
            allLevButtons[i].m_Count = current.currentLevID;
        }
    }
}
