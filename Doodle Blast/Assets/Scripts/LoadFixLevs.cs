using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFixLevs : MonoBehaviour
{
    private const int MAXCOUNTLEVS = 15;
    public GameObject rootButton;
    public LevButtonWindow prefabButton;
    public Text m_CurrentPage;
    public Text m_AllPages;
    public Button m_LeftButton;
    public Button m_RightButton;
    private int count;
    private int currentLev;
    private List<LevButtonWindow> allLevButtons;
    private int currentPage;

    void Awake()
    {
        allLevButtons = new List<LevButtonWindow>();
        currentPage = 1;
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
        SetButtonText();
    }

    void OnEnable()
    {
        CAllFixedLev.isFixedLev = true;
        currentLev = PlayerPrefs.GetInt("MaxFixLev") - 1;
        for (int i = 0; i < count; i++)
        {
            if (i <= currentLev || i == 0)
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

    void Start()
    {
        m_LeftButton.onClick.AddListener(OnClickLeft);
        m_RightButton.onClick.AddListener(OnClickRight);
    }

    private void OnClickLeft()
    {
        if (currentPage > 1)
        {
            currentPage--;
            SetButtonText();
        }
        else
            currentPage = 1;
    }

    private void OnClickRight()
    {
        int max = count / MAXCOUNTLEVS + 1;
        if (currentPage < max)
        {
            currentPage++;
            SetButtonText();
        }
        else
            currentPage = max;
    }
    
    private void SetButtonText()
    {
        m_CurrentPage.text = currentPage.ToString();
        int temp = count / MAXCOUNTLEVS;
        m_AllPages.text = (temp + 1).ToString();
        for (int i = 0; i < MAXCOUNTLEVS; i++)
        {
            int index = i + (currentPage - 1) * MAXCOUNTLEVS;
            LevButtonWindow lbw = allLevButtons[i];
            if (index < count)
            {
                lbw.gameObject.SetActive(true);
            }
            else
                lbw.gameObject.SetActive(false);
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
