using System.Collections.Generic;
using UnityEngine;

public class Window_Delete : MonoBehaviour {
    public GameObject m_GoldMove;
    public GameObject m_Gold;
    public Window_Canvas m_canvas;
    public GameObject m_gold;//小金币
    public GameObject m_Sprite;
	private List<List<Element>> m_AllDelete;
	private bool BeginDelete;
	private Transform tf;
	private float deleteTime = 0f;
	private int rewardIndex = 0;

	private void Awake() {
		EventMgr.DeleteEvent += OnDelete;
		EventMgr.MouseUpDeleteEvent += OnMouseUpDelete;
    }
	
	void Update() {
        if (BeginDelete) {
            DleteEle();
        }
	}

	private void DleteEle() {
		if (m_AllDelete.Count <= 1) {
			if (m_AllDelete[0].Count <= 4) {
				GetScoreWithNum(0, m_AllDelete[0][m_AllDelete[0].Count / 2].position, m_GoldMove);
			} else {
				Effect(m_AllDelete[0], m_GoldMove);
			}
			m_GoldMove.SetActive(true);
			BeginDelete = false;
			EventMgr.MouseUpCreateByTrans(tf);
		} else {
			DeleMore();
		}
    }

	private void Effect(List<Element> element, GameObject obj) {
        GetScoreWithNum(element.Count, element[element.Count / 2].position, obj);
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
		if (index > 1) {
			CreatGold(m_AllDelete[index][m_AllDelete[1].Count / 2]);
		}
		if (!m_Gold.activeSelf) {
			m_Gold.SetActive(true);
		}
		Effect(m_AllDelete[index], m_Gold.transform.GetChild(0).gameObject);
		m_Gold.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
		m_Gold.transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsScale");
	}

	private void DeleMore() {
		deleteTime += Time.deltaTime;
		if (deleteTime * 2 > rewardIndex) {
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

	private int SortDelList(List<Element> arrA, List<Element> arrB) {
		if (arrA.Count > arrB.Count) {
			return -1;
		}
		if (arrA.Count < arrB.Count) {
			return 1;
		}
		return 0;
	}

	private void OnDelete(List<List<Element>> list) {
		list.Sort(SortDelList);
		m_AllDelete = list;
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
	private void CreatGold(Element pos) {
        GameObject obj = Instantiate(m_gold);
        obj.transform.position = pos.position;
    }
	
    private void OnDestroy() {
		EventMgr.DeleteEvent -= OnDelete;
		EventMgr.MouseUpDeleteEvent -= OnMouseUpDelete;
	}
}
