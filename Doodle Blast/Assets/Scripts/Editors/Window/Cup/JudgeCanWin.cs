using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeCanWin : MonoBehaviour {
    public GameObject windowWin;
    public GameObject windowLose;
    public Transform leftUp;
    public Transform rightUp;
    public Transform leftDown;
    public SetWaterValue water;
    public GameObject waterLevel;
    public GameObject winLine;
    public float waterHeigth;
    private List<float> allSphereY;
    private List<Transform> allSpheres;
    private List<Transform> tempLst;
    private List<Vector3> allSpheresPos;
    private float myWinLine;
    private bool IsBegin;

    void Awake()
    {
        water.Init(this);
        waterHeigth = water.GetComponent<SpriteRenderer>().sprite.rect.height / 100;
        allSpheres = new List<Transform>();
        allSpheresPos = new List<Vector3>();
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

    public bool IsInCup(Vector3 pos)
    {
        if (pos.x >= leftUp.position.x && pos.x <= rightUp.position.x
            && pos.y >= leftDown.position.y && pos.y <= leftUp.position.y) return true;
        return false;
    }

    private float MaxSphereHeigth()
    {
        allSphereY = new List<float>();
        for(int i =0;i< tempLst.Count; i++)
        {
            if(IsInCup(tempLst[i].transform.position))
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
                        windowWin.SetActive(true);
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
}
