using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
	private const float COL_GAP = 0.6f;
	private const float ROW_GAP = 0.5f;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private static Window_Creat _instance;
	private List<Element> m_lstBackElement;//所有元素的集合
	private List<List<Element>> m_lstDelLine;
	private Element[,] m_arrElement;
	private int m_rowCount;
	private int m_colCount;
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
		m_rowCount = mapTab.GetLength(0);
        m_colCount = mapTab.GetLength(1);
        m_arrElement = new Element[m_rowCount, m_colCount];
        //根据列表实例化出来棋盘
        for (int x = 0; x < m_rowCount; x++) {
            for (int y = 0; y < m_colCount; y++) {
                if (mapTab[x, y] != 0) {
                    GameObject goBack = Instantiate(prefabBG);
                    goBack.transform.parent = transform;
					goBack.transform.localPosition = GetWorldPos(x, y);
					Element element = goBack.GetComponent<Element>();
					element.ResetColor();
					element.pos = new Pos2Int(x, y);
                    GameObject effect = Instantiate(m_Effect);
                    effect.transform.parent = goBack.transform;
					effect.transform.localPosition = Vector3.zero;
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
		Pos2Int pos = target.pos;
		List<Element> lstDelElement = new List<Element>();
		for (int rowNum = 0; rowNum < m_rowCount; rowNum++) {
			int colNum = pos.y + rowNum / 2 - pos.x / 2;
            if (colNum >= 0 && colNum < m_colCount) {
				Element element = m_arrElement[rowNum, colNum];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.isInDeleteList) {
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
		Pos2Int pos = target.pos;
		List<Element> lstDelElement = new List<Element>();
		for (int rowNum = 0; rowNum < m_rowCount; rowNum++) {
			int colNum = pos.y + (pos.x + 1) / 2 - (rowNum + 1) / 2;
            if (colNum >= 0 && colNum < m_colCount) {
				Element element = m_arrElement[rowNum, colNum];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.isInDeleteList) {
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
		for (int colNum = 0; colNum < m_colCount; colNum++) {
			Element element = m_arrElement[pos.x, colNum];
			if (element != null) {
				if (element.CheckIsEmpty()) {
					return;
				}
				listDelElement.Add(element);
				if (!element.isInDeleteList) {
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
	
    private Vector2 GetWorldPos(int rowNum, int colNum) {
		float offsetX = rowNum % 2 == 0 ? 0 : COL_GAP / 2;
		return new Vector2((colNum - (m_colCount - 1) / 2.0f) * COL_GAP + offsetX, ((m_rowCount - 1) / 2.0f - rowNum) * ROW_GAP);
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
		Element[] current = GetCanPutElement(pos);
		m_lstOldElement.Clear();
		if (current != null) {
			for (int i = 0; i < current.Length; i++) {
				current[i].SetDarkColor(currenttype);
				m_old = tran;
				m_lstOldElement.Add(current[i]);
			}
		}
	}

	private void OnMouseUp(Transform[] tran) {
		Vector3[] poss = new Vector3[tran.Length];
		for (int i = 0; i < tran.Length; i++) {
			poss[i] = tran[i].position;
		}
		Element[] current = GetCanPutElement(poss);
		if (current == null) {
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
			Element element = m_lstOldElement[i];
			element.ApplyColor();
			TryDelCenter(element);
			TryDelLeft(element);
			TryDelRight(element);
			tf.Add(element);
		}
		if (m_lstDelLine.Count == 0) {
			m_lstDelLine.Add(tf);
		}
		EventMgr.Delete(m_lstDelLine);
		m_lstOldElement.Clear();
	}
    
	private Element[] GetCanPutElement(Vector3[] arrPos) {
		Element[] arrElement = new Element[arrPos.Length];
		for (int a = 0; a < arrPos.Length; a++) {
			for (int i = 0; i < m_lstBackElement.Count; i++) {
				Element element = m_lstBackElement[i];
				float offset = Math.Abs(Vector3.Distance(element.position, arrPos[a]));
				if (offset < 0.25f) {
					if (element.CheckIsEmpty()) {
						arrElement[a] = element;
					}
					break;
				}
			}
			if (arrElement[a] == null) {
				return null;
			}
		}
		return arrElement;
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
				Element[] trans = GetCanPutElement(DicPos);
				if (trans != null) {
					return true;
				}
			}
		}
		return false;
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
