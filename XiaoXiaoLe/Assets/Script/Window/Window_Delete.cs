﻿using System.Collections.Generic;
using UnityEngine;

public class Window_Delete : MonoBehaviour {
    public GameObject m_GoldMove;
    public GameObject m_Gold;
    public Window_Canvas m_canvas;
    public GameObject m_gold;//小金币
    public GameObject m_Sprite;
	private List<Transform[]> m_AllDelete;
	private bool BeginDelete;
	private Transform tf;
	private float deleteTime = 0f;
	private int rewardIndex = 0;

	private void Awake() {
		EventMgr.UIDeleteEvent += OnUIDelete;
		EventMgr.MouseUpDeleteEvent += OnMouseUpDelete;
    }
	
	void Update() {
        if (BeginDelete) {
            DleteEle();
        }
	}

	private void DleteEle() {
		if (m_AllDelete.Count <= 1) {
			if (m_AllDelete[0].Length <= 4) {
				GetScoreWithNum(0, m_AllDelete[0][m_AllDelete[0].Length / 2].position, m_GoldMove);
			}
			if (m_AllDelete[0].Length > 4) {
				Effect(m_AllDelete[0], m_GoldMove);
			}
			m_GoldMove.SetActive(true);
			BeginDelete = false;
			EventMgr.MouseUpCreateByTrans(tf);
		} else {
			DeleMore();
		}
    }

	private void Effect(Transform[] trans, GameObject obj) {
        GetScoreWithNum(trans.Length, trans[trans.Length / 2].position, obj);
        for (int i = 0; i < trans.Length; i++) {
            trans[i].GetComponent<Element>().Color = ElementType.Empty;
            if (trans[i].GetChild(1).gameObject.activeSelf) {
                trans[i].GetChild(1).gameObject.SetActive(false);
            }
            trans[i].GetChild(1).gameObject.SetActive(true);
            trans[i].GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
        }
    }
	
	private void OnDeleteLine(int index) {
		if (index > 1) {
			CreatGold(m_AllDelete[index][m_AllDelete[1].Length / 2]);
		}
		Effect(m_AllDelete[index], m_Gold.transform.GetChild(0).gameObject);
		m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
		m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");
	}

	private void DeleMore() {
		deleteTime += Time.deltaTime;
		if (deleteTime * 2 > rewardIndex + 1) {
			if (rewardIndex < m_AllDelete.Count) {
				OnDeleteLine(rewardIndex);
				rewardIndex++;
			} else {
				SetSprite(m_AllDelete.Count - 2);
				m_Gold.SetActive(false);
				BeginDelete = false;
				EventMgr.MouseUpCreateByTrans(tf);
			}
		}
    }

	private void OnUIDelete() {
        m_AllDelete = DeleteList.GetList;
        m_AllDelete = Sort<Transform>(m_AllDelete);
		deleteTime = 0f;
		BeginDelete = true;
		rewardIndex = 0;
    }

	private void OnMouseUpDelete(Transform trans) {
		tf = trans;
	}

	private void SetSprite(int dx) {
        m_Sprite.SetActive(true);
        m_Sprite.transform.GetComponent<Disapper>().Enable(dx);
    }

	//按照元素长度升序排列冒泡排序
	private List<T[]> Sort<T>(List<T[]> temp) {
        if (temp.Count <= 1)
			return temp;
        for (int i = 0; i < temp.Count; i++) {
            for (int j = 0; j < temp.Count - i - 1; j++) {
                if (temp[j].Length > temp[j + 1].Length) {
                    T[] tp = new T[temp[j].Length];
                    tp = temp[j];
                    temp[j] = temp[j + 1];
                    temp[j + 1] = tp;
                }
            }
        }
        return temp;
    }

	//根据消除数量获得分数并且获取当前分数的Sprite
	private void GetScoreWithNum(int num, Vector3 pos, GameObject obj) {
        if (obj == null)
			return;
        if (obj.transform.parent == null)
            obj.transform.position = pos;
        else
            obj.transform.parent.position = pos;
		int scorenum = ConfigScoreMgr.instance.GetScoreNum(num.ToString());
		int path = ConfigScoreMgr.instance.GetPathNum(num.ToString());
		obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Num/" + path);
        m_canvas.AddScores(scorenum);
    }
	
	//创建小金币
	private void CreatGold(Transform pos) {
        GameObject obj = Instantiate(m_gold);
        obj.transform.position = pos.position;
    }
	
    private void OnDestroy() {
		EventMgr.UIDeleteEvent -= OnUIDelete;
		EventMgr.MouseUpDeleteEvent -= OnMouseUpDelete;
	}
}
