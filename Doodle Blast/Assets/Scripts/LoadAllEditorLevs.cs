using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAllEditorLevs : MonoBehaviour {
    private const int MAXCOUNTLEVS = 18;
    public GameObject levButtonRoot;
    public LevButtonWindow levButtonPrefab;
    public Button m_Delete;
    public Button m_Back;
    public Text m_CurrentPage;
    public Text m_AllPages;
    public Button m_LeftButton;
    public Button m_RightButton;
    private List<LevButtonWindow> allButtons;
    private int lstCount;
    private int currentPage;
    void Awake()
    {
        allButtons = new List<LevButtonWindow>();
        CDataMager.canDraw = true;
        currentPage = 1;
        m_Delete.gameObject.SetActive(true);
        m_Back.gameObject.SetActive(false);

        LoadDataCreatLev();
        CreatLevButton();
    }
    // Use this for initialization
    void Start () {
        m_Back.onClick.AddListener(OnClickBack);
        m_Delete.onClick.AddListener(OnClickDelete);
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
        int max = lstCount / MAXCOUNTLEVS + 1;
        if (currentPage < max)
        {
            currentPage++;
            SetButtonText();
        }
        else
            currentPage = max;
    }

    private void OnClickBack()
    {
        m_Delete.gameObject.SetActive(true);
        m_Back.gameObject.SetActive(false);
        EnableDelete(false);
    }

    private void OnClickDelete()
    {
        m_Delete.gameObject.SetActive(false);
        m_Back.gameObject.SetActive(true);
        EnableDelete(true);
    }

    private void LoadDataCreatLev()
    {
        if (CAllEditorLevs.GetInstance.allEditorLevs.Count > 0) return;
        CAllLevsData cld = CFileMager.GetInstance.ReadFiles(Application.persistentDataPath + "/levelConfig.txt");
        if (cld == null) return;
        lstCount = cld.allEditorLevs.Count;
        if (lstCount == 0) return;
        
        for(int i =0; i< lstCount; i++)
        {
            CAllEditorLevs.GetInstance.allEditorLevs.Add(cld.allEditorLevs[i].currentLevID,cld.allEditorLevs[i]);
            CAllEditorLevs.GetInstance.allLevID.Add(cld.allEditorLevs[i].currentLevID);
        }
    }

    private void CreatLevButton()
    {
        for (int i =0;i< MAXCOUNTLEVS; i++)
        {
            LevButtonWindow lbw = Instantiate(levButtonPrefab, levButtonRoot.transform);
            lbw.gameObject.SetActive(false);
            allButtons.Add(lbw);
        }
        SetButtonText();
    }

    public void SetButtonText()
    {
        lstCount = CAllEditorLevs.GetInstance.allLevID.Count;
        m_CurrentPage.text = currentPage.ToString();
        int temp = lstCount / MAXCOUNTLEVS;
        m_AllPages.text = (temp + 1).ToString();
        for (int i = 0; i < MAXCOUNTLEVS; i++)
        {
            int index = i + (currentPage - 1) * MAXCOUNTLEVS;
            LevButtonWindow lbw = allButtons[i];
            if (index < lstCount)
            {
                lbw.gameObject.SetActive(true);
                lbw.m_Count = CAllEditorLevs.GetInstance.allLevID[index];
                lbw.m_Text.text = lbw.m_Count.ToString();
                lbw.Init(this);
            }
            else
                lbw.gameObject.SetActive(false);
        }
    }

    private void EnableDelete(bool istrue)
    {
        for(int i =0;i<allButtons.Count;i++)
        {
            if (!allButtons[i].gameObject.activeSelf) return;
            allButtons[i].m_Delete.gameObject.SetActive(istrue);
            if (istrue)
            {
                allButtons[i].m_LevButton.PlayAnimation();
                allButtons[i].m_LevButton.m_Button.interactable = false;
            }
            else
            {
                allButtons[i].m_LevButton.StopAnimation();
                allButtons[i].m_LevButton.m_Button.interactable = true;
            }
        }
    }
}
