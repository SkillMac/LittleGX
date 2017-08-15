﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prefabs : MonoBehaviour {
    
    public int index;
    
    [System.Serializable]
    public struct PreType
    {
        public PrefabsType type;
        public GameObject prefab;
    }

    public PreType[] Allprefabs;
    public PrefabsType type;

    private Dictionary<PrefabsType, GameObject> PrefabsDic;

    private Vector3 MousePos;

    public Vector3[] Roots;

    private float speed = 15.0f;

    private bool IsMove ;

    public float offsetY = 0.2f;//Y轴坐标的偏移量

    public GameObject window_gv;

    private bool IsActive;
    private float timer;
    //存储随机数的列表
    List<int> AllNums;
    //随机数列的列数
    private int ListDex;

    int old;

    private void Awake() {
		EventMgr.MouseUpCreateByIndexEvent += OnMouseUpCreateByIndex;
		EventMgr.MouseUpCreateByTransEvent += OnMouseUpCreateByTrans;
        type = (PrefabsType)index;


        PrefabsDic = new Dictionary<PrefabsType, GameObject>();

        for(int i =0;i < Allprefabs.Length;i++)
        {
            if(!PrefabsDic.ContainsKey(Allprefabs[i].type))
            {
                PrefabsDic.Add(Allprefabs[i].type, Allprefabs[i].prefab);
            }
        }

    }
    // Use this for initialization
    void Start () {
        
        SetList(1);

        for (int i = 0; i < Roots.Length; i++)
        {
            int enumtype;

            if (i == 0)
            {
                int id = Random.Range(0, 999);
                enumtype = AllNums[id];

                old = enumtype;
            }
            else
            {
                enumtype = Get(old);
            }
            
            GameObject obj = Instantiate(PrefabsDic[(PrefabsType)enumtype], transform);

            obj.transform.position = Roots[i];

            index++;
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (window_gv.activeSelf) return;

        if(Input.GetMouseButtonDown(0))
        {
            MousePos = (Input.mousePosition - new Vector3(Screen.width/2.0f,Screen.height/2.0f,0))/100.0f;
            
            for (int i = 0; i < Roots.Length; i++)
            {
                if (i > transform.childCount -1) return;

                if (transform.GetChild(i) == null) return;

                transform.GetChild(i).GetComponent<TestDraw>().enabled = false;

                if (Mathf.Abs(Vector3.Distance(Roots[i], MousePos)) < 1.2f)
                {
                    transform.GetChild(i).GetComponent<TestDraw>().enabled = true;
                }
            }
        }
        
        if(IsMove)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                MoveWithIndex(transform.GetChild(i), i);
                
                if (transform.GetChild(i).position.y != Roots[i].y)
                {
                    IsMove = false;
                }
            }
        }
        
        if(IsActive)
        {
            if (Time.realtimeSinceStartup - timer > 1.0f)
            {
                window_gv.SetActive(true);
                IsActive = false;
            }
        }

    }
    //设置随机数的列表
    void SetList(int dexd)
    {
        ListDex = dexd;
        
        Tables tab = DataManager.tables[TableName.prefabtype];

        AllNums = new List<int>();

        for (int i = 0; i < 25; i++)
        {
            int Count = tab.GetDataWithIDAndIndex<int>(i.ToString(), dexd);

            for (int j = 0; j < Count; j++)
            {
                AllNums.Add(i);
            }
        }
    }

    int Get(int dex)
    {
        int id = Random.Range(0, 999);

        int xx = AllNums[id];

        if (dex == 0 || xx == 0)
            return xx;
        if (dex >= 1 && dex <= 6)
        {
            if (xx >= 1 && xx <= 6)
            {
                xx = Get(dex);
            }
        }
        if (dex >= 7 && dex <= 12)
        {
            if (xx >= 7 && xx <= 12)
            {
                xx = Get(dex);
            }
        }
        if (dex >= 13 && dex <= 18)
        {
            if (xx >= 13 && xx <= 18)
            {
                xx = Get(dex);
            }
        }
        if (dex >= 19 && dex <= 21)
        {
            if (xx >= 19 && xx <= 21)
            {
                xx = Get(dex);
            }
        }
        if (dex >= 22 && dex <= 24)
        {
            if (xx >= 22 && xx <= 24)
            {
                xx = Get(dex);
            }
        }

        old = xx;

        return xx;
    }

    void MoveWithIndex(Transform trans,int index)
    {
        if(index >=0 && index < Roots.Length)
        {
            Vector3 dir = Roots[index];
            
            trans.position = Vector3.MoveTowards(trans.position, dir, speed * Time.deltaTime);

            if(trans.GetComponent<TestDraw>() != null)
            {
                trans.GetComponent<TestDraw>().startpos = dir;
            }
        }
    }
    PrefabsType creattype;
    int dexcode;
	
	private void OnMouseUpCreateByTrans(Transform tf) {
		dexcode = tf.GetHashCode();
		//根据类型判断是否游戏可以继续
		if (!CanContinue()) {
			IsActive = true;
			timer = Time.realtimeSinceStartup;
		}
		GameObject obj = Instantiate(PrefabsDic[creattype], transform);
		obj.transform.position = Roots[Roots.Length - 1];
	}

	private void OnMouseUpCreateByIndex() {
		index++;
		if (index >= 30 && index < 60) {
			if (ListDex != 2)
				SetList(2);
		}
		if (index >= 60) {
			if (ListDex != 3)
				SetList(3);
		}
		int enumtype = Get(old);
		if (PrefabsDic.ContainsKey((PrefabsType)enumtype)) {
			IsMove = true;
			creattype = (PrefabsType)enumtype;
		}
	}

    Vector3[] posPre;
    Vector3[] DicPos;


    bool CanContinue()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            Debug.Log(transform.childCount);
            //包含了想要删除的物体
            if (transform.GetChild(a).GetHashCode() != dexcode)
            {
                posPre = transform.GetChild(a).GetComponent<TestDraw>().GetAbsPos();

                if (posPre == null) return true;

                if (posPre.Length > 1)
                {
                    for (int i = 0; i < Window_Creat.AllElement.Count; i++)
                    {
                        if (Window_Creat.AllElement[i].GetComponent<Element>().Color == ElementType.Empty)
                        {
                            DicPos = new Vector3[posPre.Length];

                            for (int j = 0; j < posPre.Length; j++)
                            {
                                DicPos[j] = Window_Creat.AllElement[i].position + posPre[j];
                            }

                            Transform[] trans = Window_Creat.ComPos(DicPos);

                            if (HasType(trans))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    bool HasType(Transform[] trans)
    {
        if (trans == null) return false;

        if (trans != null)
        {
            for (int j = 0; j < trans.Length; j++)
            {
                if (trans[j] == null) return false;

                if (trans[j] != null)
                {
                    Debug.Log(trans[j].GetComponent<Element>().Color);

                    if (trans[j].GetComponent<Element>().Color != ElementType.Empty)
                        return false;
                }
            }
        }
        return true;
    }

    private void OnDestroy() {
		EventMgr.MouseUpCreateByIndexEvent -= OnMouseUpCreateByIndex;
		EventMgr.MouseUpCreateByTransEvent -= OnMouseUpCreateByTrans;
	}

    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
    }
}

public enum PrefabsType
{
    m_01,
    m_02,
    m_03,
    m_04,
    m_05,
    m_06,
    m_07,
    m_08,
    m_09,
    m_010,
    m_011,
    m_012,
    m_013,
    m_014,
    m_015,
    m_016,
    m_017,
    m_018,
    m_019,
    m_020,
    m_021,
    m_022,
    m_023,
    m_024,
    m_025,
}
