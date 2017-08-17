using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
    public float offsetx, offsetY;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private static Window_Creat _instance;
	private List<Element> m_lstBackElement;//所有元素的集合
	private List<List<Element>> m_lstDelLine;
	private Element[,] m_arrElement;
	private int m_xDim, m_yDim;
	private List<Element> m_lstOldElement = new List<Element>();
	private Transform[] m_old;

	void Awake() {
		_instance = this;
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		m_lstBackElement = new List<Element>();
    }
	
    void Start() {
		int[,] mapTab = ConfigMapsMgr.instance.GetMapsData();
		m_xDim = mapTab.GetLength(0);
        m_yDim = mapTab.GetLength(1);
        m_arrElement = new Element[m_xDim, m_yDim];
        //根据列表实例化出来棋盘
        for (int x = 0; x < m_xDim; x++) {
            for (int y = 0; y < m_yDim; y++) {
                if (mapTab[x, y] != 0) {
                    GameObject goBack = Instantiate(prefabBG, GetWorldPos(x, y), Quaternion.identity);
                    goBack.transform.parent = transform;
					Element element = goBack.GetComponent<Element>();
					element.ResetColor();
					element.pos = new Pos2Int(x, y);
                    GameObject effect = Instantiate(m_Effect, GetWorldPos(x, y), Quaternion.identity);
                    effect.transform.parent = goBack.transform;
                    effect.SetActive(false);
					m_lstBackElement.Add(element);
					m_arrElement[x, y] = element;
				} else {
                    m_arrElement[x, y] = null;
                }
            }
        }
    }
	
	private void TryDelLeft(Element target) {
		bool bFlag = false;
        int i = 0;
		Pos2Int pos = target.pos;
		List<Element> lstDelElement = new List<Element>();
		for (int aa = 0; aa < m_xDim; aa++) {
            i = pos.y - pos.x + aa;
            if (i >= 0 && i < m_yDim) {
				Element element = m_arrElement[aa, i];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.IsInDeleteList()) {
						bFlag = true;
					}
                }
            }
        }
		if (bFlag) {
			AddToDelList(lstDelElement);
		}
    }
	
	private void TryDelRight(Element target) {
		bool bFlag = false;
		int i = 0;
		Pos2Int pos = target.pos;
		List<Element> lstDelElement = new List<Element>();
		for (int aa = 0; aa < m_xDim; aa++) {
            i = pos.y + pos.x - aa;
            if (i >= 0 && i < m_yDim) {
				Element element = m_arrElement[aa, i];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.IsInDeleteList()) {
						bFlag = true;
					}
				}
            }
        }
		if (bFlag) {
			AddToDelList(lstDelElement);
		}
    }
	
	private void TryDelCenter(Element target) {
		bool bFlag = false;
		Pos2Int pos = target.pos;
		List<Element> listDelElement = new List<Element>();
		for (int aa = 0; aa < m_yDim; aa++) {
			Element element = m_arrElement[pos.x, aa];
			if (element != null) {
				if (element.CheckIsEmpty()) {
					return;
				}
				listDelElement.Add(element);
				if (!element.IsInDeleteList()) {
					bFlag = true;
				}
			}
        }
		if (bFlag) {
			AddToDelList(listDelElement);
		}
    }

	private void AddToDelList(List<Element> listDelElement) {
		for (int i = 0; i < listDelElement.Count; i++) {
			listDelElement[i].SetInDeleteList();
		}
		m_lstDelLine.Add(listDelElement);
	}
	
    private Vector2 GetWorldPos(int x, int y) {
        return new Vector2((y - (m_yDim - 1) / 2.0f) * offsetx + transform.position.x, ((m_xDim - 1) / 2.0f - x) * offsetY + transform.position.y);
    }
	
	private void OnMouseDown(Transform[] tran) {
		if (tran == null || tran == m_old)
			return;
		Vector3[] pos = new Vector3[tran.Length];
		ElementType currenttype = tran[0].GetComponent<Element>().colorType;
		for (int i = 0; i < tran.Length; i++) {
			pos[i] = tran[i].position;
		}
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			m_lstOldElement[i].ResetColor();
		}
		Element[] current = ComPos(pos);
		m_lstOldElement.Clear();
		if (current != null && IsVer(current, currenttype)) {
			for (int i = 0; i < current.Length; i++) {
				current[i].colorType = currenttype + 1;
				m_old = tran;
				m_lstOldElement.Add(current[i]);
			}
		}
	}

	private void OnMouseUp(Transform[] tran) {
		ElementType currenttype = tran[0].GetComponent<Element>().colorType;
		Vector3[] poss = new Vector3[tran.Length];
		for (int i = 0; i < tran.Length; i++) {
			poss[i] = tran[i].position;
		}
		Element[] current = ComPos(poss);
		if (current == null || !IsVer(current, currenttype)) {
			tran[0].parent.GetComponent<TestDraw>().ReturnStart();
			m_lstOldElement.Clear();
			return;
		}
		Destroy(tran[0].parent.gameObject);
		EventMgr.MouseUpDelete(tran[0].parent);
		EventMgr.MouseUpCreateByIndex();
		m_lstDelLine = new List<List<Element>>();
		List<Element> tf = new List<Element>();
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			Element trans = m_lstOldElement[i];
			trans.colorType -= 1;
			TryDelCenter(trans);
			TryDelLeft(trans);
			TryDelRight(trans);
			tf.Add(trans);
		}
		if (m_lstDelLine.Count == 0) {
			m_lstDelLine.Add(tf);
		}
		EventMgr.Delete(m_lstDelLine);
		m_lstOldElement.Clear();
	}
    
    //判断是否可以放进去
	private bool IsVer(Element[] current, ElementType et) {
        for (int i = 0; i < current.Length; i++) {
            if (current[i] == null) {
				return false;
			}
			if (!current[i].CheckIsEmpty() && current[i].colorType != et + 1) {
                return false;
            }
        }
        return true;
    }

    //根据坐标获取当前的元素
	private Element[] ComPos(Vector3[] pos) {
		Element[] current = new Element[pos.Length];
		for (int a = 0; a < pos.Length; a++) {
			for (int i = 0; i < m_lstBackElement.Count; i++) {
				float offset = Math.Abs(Vector3.Distance(m_lstBackElement[i].position, pos[a]));
				if (offset < 0.25f) {
					current[a] = m_lstBackElement[i];
				}
			}
		}
		return current;
    }

	public bool CheckCanContinue(Vector3[] posPre) {
		if (posPre == null)
			return true;
		for (int i = 0; i < m_lstBackElement.Count; i++) {
			if (m_lstBackElement[i].CheckIsEmpty()) {
				Vector3[] DicPos = new Vector3[posPre.Length];
				for (int j = 0; j < posPre.Length; j++) {
					DicPos[j] = m_lstBackElement[i].position + posPre[j];
				}
				Element[] trans = ComPos(DicPos);
				if (HasType(trans)) {
					return true;
				}
			}
		}
		return false;
	}

	private bool HasType(Element[] trans) {
		for (int j = 0; j < trans.Length; j++) {
			if (trans[j] == null || !trans[j].CheckIsEmpty()) {
				return false;
			}
		}
		return true;
	}

	void OnDestroy() {
		EventMgr.MouseUpEvent -= OnMouseUp;
		EventMgr.MouseDownEvent -= OnMouseDown;
	}

	public static Window_Creat instance {
		get {
			return _instance;
		}
	}
}
