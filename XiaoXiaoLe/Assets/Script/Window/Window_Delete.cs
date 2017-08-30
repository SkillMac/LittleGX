using System.Collections.Generic;
using UnityEngine;

public class Window_Delete : MonoBehaviour {
    public GameObject m_GoldMove;
    public GameObject m_Gold;
    public Window_Canvas m_canvas;
    public GameObject m_gold;//小金币
    public GameObject m_Sprite;
	private List<List<BackElement>> m_listDelLine;
	private bool m_bBeginDelete;
	private float m_fDeleteTime = 0f;
	private int m_uRewardIndex = 0;

	private void Awake() {
		GameMgr.instance.f_windowDelete = this;
    }
	
	void Update() {
        if (m_bBeginDelete) {
            DeleteEle();
        }
	}

	private void DeleteEle() {
		if (m_listDelLine.Count <= 1) {
			if (m_listDelLine[0].Count <= 4) {
				GetScoreWithNum(0, m_listDelLine[0][m_listDelLine[0].Count / 2].transform.position, m_GoldMove);
			} else {
				Effect(m_listDelLine[0], m_GoldMove);
			}
			m_GoldMove.SetActive(true);
			m_bBeginDelete = false;
			GameMgr.instance.CheckEndAndCreateShape();
		} else {
			DeleMore();
		}
    }

	private void Effect(List<BackElement> element, GameObject obj) {
        GetScoreWithNum(element.Count, element[element.Count / 2].transform.position, obj);
        for (int i = 0; i < element.Count; i++) {
			element[i].ResetColor();
			GameObject child = element[i].transform.GetChild(1).gameObject;
			if (child.activeSelf) {
				child.SetActive(false);
            }
			child.SetActive(true);
			child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
        }
    }
	
	private void OnDeleteLine(int index) {
		if (index > 0) {
			CreatGold(m_listDelLine[index][m_listDelLine[1].Count / 2]);
		}
		if (!m_Gold.activeSelf) {
			m_Gold.SetActive(true);
		}
		Effect(m_listDelLine[index], m_Gold.transform.GetChild(0).gameObject);
		m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
		m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");
	}

	private void DeleMore() {
		m_fDeleteTime += Time.deltaTime;
		if (m_fDeleteTime * 2 > m_uRewardIndex) {
			if (m_uRewardIndex < m_listDelLine.Count) {
				OnDeleteLine(m_uRewardIndex);
				m_uRewardIndex++;
			} else {
				SetSprite(m_listDelLine.Count - 2);
				m_Gold.SetActive(false);
				m_bBeginDelete = false;
				GameMgr.instance.CheckEndAndCreateShape();
			}
		}
    }

	private int SortDelList(List<BackElement> arrA, List<BackElement> arrB) {
		if (arrA.Count > arrB.Count) {
			return -1;
		}
		if (arrA.Count < arrB.Count) {
			return 1;
		}
		return 0;
	}

	public void OnDelete(List<List<BackElement>> list) {
		list.Sort(SortDelList);
		m_listDelLine = list;
		m_fDeleteTime = 0f;
		m_bBeginDelete = true;
		m_uRewardIndex = 0;
    }
	
	private void SetSprite(int dx) {
        m_Sprite.SetActive(true);
        m_Sprite.transform.GetComponent<Disapper>().Enable(dx);
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
	private void CreatGold(BackElement pos) {
        GameObject obj = Instantiate(m_gold);
        obj.transform.position = pos.transform.position;
    }
}
