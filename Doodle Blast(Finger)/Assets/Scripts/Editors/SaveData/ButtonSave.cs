using UnityEngine;
using UnityEngine.UI;

public class ButtonSave : MonoBehaviour {
    private Button m_Button;
    private SaveData m_Winodw;

    public void Init(SaveData m_Winodw)
    {
        this.m_Winodw = m_Winodw;
    }
    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(ClickButton);
        Debug.Log(CDataMager.getInstance.myPigmentVolume);
    }

    private void ClickButton()
    {
        m_Winodw.OnUnable();
        m_Winodw.m_Window.m_Reset.ResetAllSpheres();
        m_Winodw.m_Window.gameObject.SetActive(true);
        m_Winodw.m_Window.m_Mager.m_Bottle.transform.SetAsLastSibling();
        SaveDataToJson();
        CDataMager.getInstance.DestroyAllData();
        CDataMager.getInstance.Init();
    }

    private void SaveDataToJson()
    {
        CAllLevsData cld = CFileMager.GetInstance.ReadFiles(Application.persistentDataPath + "/levelConfig.txt");
        //int count = Resources.LoadAll("Data/EditorData").Length;

        CLevel lev = new CLevel();
        lev.currentLevID = cld.allEditorLevCount +1;
        //lev.currentLevID = count + 1;
        lev.m_pigment = new CPigment(CDataMager.getInstance.myPigmentVolume);
        Vector3 cupPos = new Vector3(CDataMager.getInstance.cupPositionX, CDataMager.cupPositionY, 0);
        Vector3 winLine = new Vector3(CDataMager.getInstance.cupPositionX, CDataMager.getInstance.winLinePositionY, 0);
        lev.m_cup = new CCup(cupPos, winLine);
        lev.m_spheres = new CSphere[CDataMager.getInstance.allSpheres.Count];
        if(lev.m_spheres.Length >0)
        {
            for(int i =0;i<lev.m_spheres.Length;i++)
            {
                Vector3 spherePos = CDataMager.getInstance.allSpheres[i].transform.position;
                Vector3 sphereScale = CDataMager.getInstance.allSpheres[i].transform.localScale;
                string spriteName = CDataMager.getInstance.allSpheres[i].transform.GetComponent<SpriteRenderer>().sprite.name;
                lev.m_spheres[i] = new CSphere(spherePos, sphereScale,spriteName);
            }
        }
        lev.m_barriers = new CBarrier[CDataMager.getInstance.allCubes.Count];
        if (lev.m_barriers.Length > 0)
        {
            for (int i = 0; i < lev.m_barriers.Length; i++)
            {
                Vector3 cubePos = CDataMager.getInstance.allCubes[i].transform.position;
                Vector3 cubeScale = CDataMager.getInstance.allCubes[i].transform.localScale;
                Quaternion cubeRotation = CDataMager.getInstance.allCubes[i].transform.localRotation;
                lev.m_barriers[i] = new CBarrier(cubePos, cubeScale,cubeRotation);
            }
        }
        CAllEditorLevs.GetInstance.allEditorLevs.Add(lev.currentLevID,lev);
        CAllEditorLevs.GetInstance.allLevID.Add(lev.currentLevID);
        cld.allEditorLevs.Add(lev);
        cld.allEditorLevCount++;
        CFileMager.GetInstance.WriteFiles(cld, Application.persistentDataPath + "/levelConfig.txt");
        //CFileMager.GetInstance.WriteFiles(lev, Application.dataPath + "/Resources/Data/EditorData/" + lev.currentLevID + ".txt");
        m_Winodw.m_Success.SetActive(true);
    }
}
