using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeCanWin : MonoBehaviour {
    public GameObject windowWin;
    public GameObject windowLose;
    public SetWaterValue water;
    public GameObject waterLevel;
    public GameObject winLine;
    public float waterHeigth;
    private List<float> allSphereY;
    private List<Transform> allSpheres = new List<Transform>();
    private List<Transform> tempLst;
    private List<Vector3> allSpheresPos;
    private float myWinLine;
    private bool IsBegin;
    private GetSpriteVertexs m_Vertexs;
    private DrawLines m_Draw;
    private int starCount;

    void Awake()
    {
        water.Init(this);
        waterHeigth = water.GetComponent<SpriteRenderer>().sprite.rect.height / 100;
        m_Vertexs = GetComponent<GetSpriteVertexs>();
    }

    void OnEnable()
    {
        IsBegin = true;
        allSphereY = new List<float>();
    }
    
	// Update is called once per frame
	void Update () {
        JudgeWin();
    }

    public void InitDraw(DrawLines m_Draw)
    {
        this.m_Draw = m_Draw;
    }

    public void InitData(List<SphereMager> lst,float winline)
    {
        allSpheresPos = new List<Vector3>();
        List<Transform> temp = new List<Transform>();
        for(int i =0;i<lst.Count;i++)
        {
            temp.Add(lst[i].transform);
            allSpheresPos.Add(lst[i].transform.position);
        }
        allSpheres = temp;
        myWinLine = winline;
    }

    public void InitData(List<Transform> lst)
    {
        allSpheresPos = new List<Vector3>();
        for (int i = 0; i < lst.Count; i++)
        {
            allSpheresPos.Add(lst[i].transform.position);
        }
        allSpheres = lst;
        myWinLine = winLine.transform.position.y;
    }

    public void ReSetAllData()
    {
        water.InitData();
        for (int i = 0; i < allSpheres.Count; i++)
        {
            if (allSpheres[i] == null) return;
            allSpheres[i].transform.position = allSpheresPos[i];
            allSpheres[i].transform.rotation = Quaternion.identity;
            allSpheres[i].GetComponent<WhetherSphereStatic>().SetRigidbody2D(RigidbodyType2D.Static);
            allSpheres[i].GetComponent<WhetherSphereStatic>().enabled = false;
            allSpheres[i].gameObject.SetActive(true);
        }
        windowWin.SetActive(false);
        windowLose.SetActive(false);
        this.enabled = false;
    }
    
    private float MaxSphereHeigth()
    {
        allSphereY = new List<float>();
        for(int i =0;i< tempLst.Count; i++)
        {
            if(m_Vertexs.IsInCup(tempLst[i].transform.position))
            {
                float radius = tempLst[i].transform.lossyScale.x * tempLst[i].transform.GetComponent<CircleCollider2D>().radius;
                allSphereY.Add(tempLst[i].transform.position.y + radius);
            }
        }
        if(allSphereY.Count >0)
        {
            allSphereY.Sort();
            return allSphereY[allSphereY.Count - 1];
        }
        return 0;
    }

    private bool IsAllActiv()
    {
        tempLst = new List<Transform>();
        if (allSpheres.Count == 0) return false;
        for (int i = 0; i < allSpheres.Count; i++)
        {
            if (allSpheres[i].gameObject.activeSelf)
            {
                tempLst.Add(allSpheres[i]);
            }
        }
        if (tempLst.Count > 0) return true;
        return false;
    }

    private bool IsAllStatic()
    {
        for (int i = 0; i < tempLst.Count; i++)
        {
            if (!tempLst[i].transform.GetComponent<WhetherSphereStatic>().IsStatic)
                return false;
        }
        return true;
    }

    private void JudgeWin()
    {
        if(IsBegin)
        {
            if (!IsAllActiv())
            {
                windowLose.SetActive(true);
                IsBegin = false;
            }
            else if (IsAllStatic())
            {
                float temp = MaxSphereHeigth();
                Debug.Log(temp);
                if (temp != 0)
                {
                    if (temp >= myWinLine)
                    {
                        JudgeStarsNum();
                        SetStarsNum();
                        windowWin.SetActive(true);
                    }                     
                    else
                        windowLose.SetActive(true);
                    water.MoveToWaterLevel(temp);
                }
                else
                    windowLose.SetActive(true);
                IsBegin = false;
            }
        }
    }

    private void JudgeStarsNum()
    {
        Window_Win tempWin = windowWin.GetComponent<Window_Win>();
        if (tempWin == null) return;
        
        if (allSpheres.Count == 1)
        {
            if(m_Draw.SetPigmentImage() >= 1f/6f)
            {
                starCount = 3;
                tempWin.midStar.SetActive(true);
                tempWin.rightStar.SetActive(true);
            }
            else
            {
                starCount = 2;
                tempWin.midStar.SetActive(true);
                tempWin.rightStar.SetActive(false);
            }
            return;
        }
        if(allSphereY.Count == allSpheres.Count)
        {
            starCount = 3;
            tempWin.midStar.SetActive(true);
            tempWin.rightStar.SetActive(true);
            return;
        }
        else
        {
            starCount = 2;
            tempWin.midStar.SetActive(true);
            tempWin.rightStar.SetActive(false);
            return;
        }
    }

    private void SetStarsNum()
    {
        int myLevIndex;
        int tempCount;
        if (!CAllFixedLev.isFixedLev)
        {
            myLevIndex = PlayerPrefs.GetInt("CurrentLev");
            tempCount = PlayerPrefs.GetInt("CurrentLev" + myLevIndex);
            if (starCount > tempCount)
                PlayerPrefs.SetInt("CurrentLev" + myLevIndex, starCount);
        }
        else
        {
            myLevIndex = PlayerPrefs.GetInt("CurrentFixLev");
            tempCount = PlayerPrefs.GetInt("CurrentFixLev" + myLevIndex);
            if (starCount > tempCount)
                PlayerPrefs.SetInt("CurrentFixLev" + myLevIndex, starCount);
        }
    }
}
