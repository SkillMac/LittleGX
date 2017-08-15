using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Delete : MonoBehaviour {

    private List<Transform[]> m_AllDelete;
    private bool BeginDelete;

    public GameObject m_GoldMove;
    public GameObject m_Gold;
    public Window_Canvas m_canvas;

    public GameObject m_gold;//小金币
    public GameObject m_Sprite;

    float timer, timer1,timer2,timer3,timer4,timer5,timer6,timer7,timer8;

    private int dex, dex1,dex2,dex3,dex4, dex5, dex6, dex7, dex8;

    private bool IsScore, IsScore1, IsScore2, IsScore3, IsScore4, IsScore5, IsScore6, IsScore7, IsScore8;

    private void Awake() {
		EventMgr.UIDeleteEvent += OnUIDelete;
		EventMgr.MouseUpDeleteEvent += OnMouseUpDelete;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(BeginDelete)
        {
            DleteEle();
            DeleMore();
        }
	}

    void DleteEle()
    {
        if(m_AllDelete.Count <=1)
        {
            if (m_AllDelete[0].Length <= 4)
            {
                GetScoreWithNum(0, m_AllDelete[0][m_AllDelete[0].Length / 2].position, m_GoldMove);
            }
            if (m_AllDelete[0].Length > 4)
            {
                Effect(m_AllDelete[0], m_GoldMove);
            }
            //错点
            m_GoldMove.SetActive(true);
            BeginDelete = false;
			EventMgr.MouseUpCreateByTrans(tf);
		}
    }

    void Effect(Transform[] trans,GameObject obj)
    {
        GetScoreWithNum(trans.Length, trans[trans.Length/2].position, obj);

        for (int i = 0; i < trans.Length; i++)
        {
            trans[i].GetComponent<Element>().Color = ElementType.Empty;
            if(trans[i].GetChild(1).gameObject.activeSelf)
            {
                trans[i].GetChild(1).gameObject.SetActive(false);
            }

            trans[i].GetChild(1).gameObject.SetActive(true);
            trans[i].GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
        }
    }


    void Wait0()
    {
        wait();
        if (dex == 0 && IsScore)
        {
            Effect(m_AllDelete[0], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.SetActive(true);
            m_Gold.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");
            IsScore = false;
        }
    }
    void Wait1()
    {
        wait1();
        if (dex1 == 0 && IsScore1)
        {
            CreatGold(m_AllDelete[1][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[1], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore1 = false;
        }
    }
    void Wait2()
    {
        wait2();
        if (dex2 == 0 && IsScore2)
        {
            CreatGold(m_AllDelete[2][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[2], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");
            IsScore2 = false;
        }
    }
    void Wait3()
    {
        wait3();
        if (dex3 == 0 && IsScore3)
        {
            CreatGold(m_AllDelete[3][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[3], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore3 = false;
        }
    }
    void Wait4()
    {
        wait4();
        if (dex4 == 0 && IsScore4)
        {
            CreatGold(m_AllDelete[4][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[4], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore4 = false;
        }
    }

    void Wait5()
    {
        wait5();
        if (dex5 == 0 && IsScore5)
        {
            CreatGold(m_AllDelete[5][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[5], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore5 = false;
        }
    }

    void Wait6()
    {
        wait6();
        if (dex6 == 0 && IsScore6)
        {
            CreatGold(m_AllDelete[6][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[6], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore6 = false;
        }
    }

    void Wait7()
    {
        wait7();
        if (dex7 == 0 && IsScore7)
        {
            CreatGold(m_AllDelete[7][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[7], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore7 = false;
        }
    }

    void Wait8()
    {
        wait8();
        if (dex8 == 0 && IsScore8)
        {
            CreatGold(m_AllDelete[8][m_AllDelete[1].Length / 2]);

            Effect(m_AllDelete[8], m_Gold.transform.GetChild(0).gameObject);
            m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
            m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");

            IsScore8 = false;
        }
    }


    void DeleMore()
    {
        if(m_AllDelete.Count ==2)
        {
            Wait0();
            Wait1();
            if (dex1 <= -1)
            {
                SetSprite(0);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 3)
        {
            Wait0();
            Wait1();
            Wait2();
            if (dex2 <= -1)
            {
                SetSprite(1);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }

        if (m_AllDelete.Count == 4)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            if (dex3 <= -1)
            {
                SetSprite(2);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 5)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            Wait4();
            if (dex4 <= -1)
            {
                SetSprite(3);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 6)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            Wait4();
            Wait5();
            if (dex5 <= -1)
            {
                SetSprite(4);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 7)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            Wait4();
            Wait5();
            Wait6();
            if (dex6 <= -1)
            {
                SetSprite(5);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 8)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            Wait4();
            Wait5();
            Wait6();
            Wait7();
            if (dex7 <= -1)
            {
                SetSprite(6);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
        if (m_AllDelete.Count == 9)
        {
            Wait0();
            Wait1();
            Wait2();
            Wait3();
            Wait4();
            Wait5();
            Wait6();
            Wait7();
            Wait8();
            if (dex8 <= -1)
            {
                SetSprite(7);
                m_Gold.SetActive(false);
                BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
        }
    }

    Transform tf;

    private void OnUIDelete() {
        m_AllDelete = new List<Transform[]>();

        m_AllDelete = DeleteList.GetList;

        m_AllDelete = Sort<Transform>(m_AllDelete);

        BeginDelete = true;

        dex = 0;
        dex1 = dex + 1;
        dex2 = dex + 2;
        dex3 = dex + 3;
        dex4 = dex + 4;
        dex5 = dex + 5;
        dex6 = dex + 6;
        dex7 = dex + 7;
        dex8 = dex + 8;

        timer = Time.realtimeSinceStartup;
        timer1 = Time.realtimeSinceStartup;
        timer2 = Time.realtimeSinceStartup;
        timer3 = Time.realtimeSinceStartup;
        timer4 = Time.realtimeSinceStartup;
        timer5 = Time.realtimeSinceStartup;
        timer6 = Time.realtimeSinceStartup;
        timer7 = Time.realtimeSinceStartup;
        timer8 = Time.realtimeSinceStartup;
        
        IsScore = IsScore1 = IsScore2 = IsScore3 = IsScore4 = IsScore5 = IsScore6 = IsScore7 = IsScore8 = true;
    }

	private void OnMouseUpDelete(Transform trans) {
		tf = trans;
	}
	
    void SetSprite(int dx)
    {
        m_Sprite.SetActive(true);
        m_Sprite.transform.GetComponent<Disapper>().Enable(dx);
    }
    //按照元素长度升序排列冒泡排序
    List<T[]> Sort<T>(List<T[]> temp)
    {
        if (temp.Count <= 1) return temp;

        for(int i=0;i<temp.Count;i++)
        {
            for(int j =0;j<temp.Count- i -1;j++)
            {
                if (temp[j].Length > temp[j+1].Length)
                {
                    T[] tp = new T[temp[j].Length];
                    tp = temp[j];

                    temp[j] = temp[j+1];
                    temp[j+1] = tp;
                }
            }
        }

        return temp;
    }
    //根据消除数量获得分数并且获取当前分数的Sprite
    void GetScoreWithNum(int num, Vector3 pos, GameObject obj)
    {
        if (obj == null) return;

        if (obj.transform.parent == null)
            obj.transform.position = pos;
        else
            obj.transform.parent.position = pos;
		int scorenum = ConfigScoreMgr.instance.GetScoreNum(num.ToString());
		int path = ConfigScoreMgr.instance.GetPathNum(num.ToString());
		obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Num/" + path); 

        m_canvas.AddScores(scorenum);
    }

    void wait()
    {
        if (Time.realtimeSinceStartup - timer > 0.5f)
        {
            timer  = Time.realtimeSinceStartup;
            dex--;
        }
    }

    //创建小金币
    void CreatGold(Transform pos)
    {
        GameObject obj = Instantiate(m_gold);
        obj.transform.position = pos.position;
    }

    void wait1()
    {
        if (Time.realtimeSinceStartup - timer1 > 0.5f)
        {
            timer1= Time.realtimeSinceStartup;
            dex1--;
        }
    }
    void wait2()
    {
        if (Time.realtimeSinceStartup - timer2 > 0.5f)
        {
            timer2 = Time.realtimeSinceStartup;
            dex2--;
        }
    }
    void wait3()
    {
        if (Time.realtimeSinceStartup - timer3 > 0.5f)
        {
            timer3 = Time.realtimeSinceStartup;
            dex3--;
        }
    }
    void wait4()
    {
        if (Time.realtimeSinceStartup - timer4 > 0.5f)
        {
            timer4 = Time.realtimeSinceStartup;
            dex4--;
        }
    }
    void wait5()
    {
        if (Time.realtimeSinceStartup - timer5 > 0.5f)
        {
            timer5 = Time.realtimeSinceStartup;
            dex5--;
        }
    }
    void wait6()
    {
        if (Time.realtimeSinceStartup - timer6 > 0.5f)
        {
            timer6 = Time.realtimeSinceStartup;
            dex6--;
        }
    }
    void wait7()
    {
        if (Time.realtimeSinceStartup - timer7 > 0.5f)
        {
            timer7 = Time.realtimeSinceStartup;
            dex7--;
        }
    }
    void wait8()
    {
        if (Time.realtimeSinceStartup - timer8 > 0.5f)
        {
            timer8 = Time.realtimeSinceStartup;
            dex8--;
        }
    }
    private void OnDestroy() {
		EventMgr.UIDeleteEvent -= OnUIDelete;
		EventMgr.MouseUpDeleteEvent -= OnMouseUpDelete;
	}
}
