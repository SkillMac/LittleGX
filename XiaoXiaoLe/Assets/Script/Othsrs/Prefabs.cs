using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prefabs : MonoBehaviour,IMessageHandler {
    
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
    
    private void Awake()
    {
        MessageCenter.Registed(this.GetHashCode(), this);

        type = (PrefabsType)index;

        PrefabsDic = new Dictionary<PrefabsType, GameObject>();

        for(int i =0;i < Allprefabs.Length;i++)
        {
            if(!PrefabsDic.ContainsKey(Allprefabs[i].type))
            {
                PrefabsDic.Add(Allprefabs[i].type, Allprefabs[i].prefab);
            }
        }


        for(int i =0;i < Roots.Length;i++)
        {
            GameObject obj = Instantiate(PrefabsDic[(PrefabsType)i], transform);
            obj.transform.position = Roots[i];
            index++;
            
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (window_gv.activeSelf) return;

        if(Input.GetMouseButtonDown(0))
        {
            MousePos = (Input.mousePosition - new Vector3(Screen.width/2.0f,Screen.height/2.0f,0))/100.0f;
            
            for (int i = 0; i < Roots.Length; i++)
            {
                if (transform.GetChild(i) == null) return;

                transform.GetChild(i).GetComponent<TestDraw>().enabled = false;

                if (Mathf.Abs(Vector3.Distance(Roots[i] + new Vector3(0,0.5f,0), MousePos)) < 1.0f)
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

    public void MassageHandler(uint type, object data)
    {
        if(type == MessageType.MOUSE_UP_CREAT)
        {
            if(data is int )
            {
                int id = (int)data;

                Tables prefabsTab = DataManager.tables[TableName.prefabtype];

                int enumtype = prefabsTab.GetDataWithIDAndIndex<int>(id.ToString(), 1);

                if(PrefabsDic.ContainsKey((PrefabsType)enumtype))
                {
                    IsMove = true;
                    creattype = (PrefabsType)enumtype;
                    

                }
            }

            if(data is Transform)
            {
                Transform tf = (Transform)data;

                dexcode = tf.GetHashCode();

                //根据类型判断是否游戏可以继续

                if (!CanContinue())
                {
                    window_gv.SetActive(true);
                    //SceneManager.LoadScene("GameOver");
                    Debug.Log("GameOver!!");
                }

                GameObject obj = Instantiate(PrefabsDic[creattype], transform);

                obj.transform.position = Roots[Roots.Length - 1];
            }
        }
    }

    Vector3[] posPre;
    Vector3[] DicPos;

    bool CanContinue()
    {
        for(int a =0;a< transform.childCount; a++)
        {
            Debug.Log(transform.childCount);
            //包含了想要删除的物体
            if(transform.GetChild(a).GetHashCode() != dexcode)
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

    private void OnDestroy()
    {
        MessageCenter.Cancel(this.GetHashCode());
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
