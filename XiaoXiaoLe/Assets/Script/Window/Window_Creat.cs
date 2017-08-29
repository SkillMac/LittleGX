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
					element.InitPos(x, y);
					GameObject effect = Instantiate(m_Effect);
                    effect.transform.parent = goBack.transform;
					effect.transform.localPosition = Vector3.zero;
					effect.SetActive(false);
					m_lstBackElement.Add(element);
					m_arrElement[x, y] = element;
				}
            }
        }
    }
	
	private void TryDelLeft(Element target) {
		bool bFlag = false;
		Pos2Int pos = target.pos;
		List<Element> lstDelElement = new List<Element>();
		for (int rowNum = 0; rowNum < m_rowCount; rowNum++) {
			int colNum = pos.y + rowNum - pos.x;
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
			int colNum = pos.y + pos.x - rowNum;
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
		return new Vector2((colNum - (m_colCount - 1) / 2.0f) * COL_GAP / 2.0f, ((m_rowCount - 1) / 2.0f - rowNum) * ROW_GAP);
    }
	
	private void OnMouseDown(TestDraw shape) {
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			m_lstOldElement[i].ResetColor();
		}
		m_lstOldElement.Clear();
		List<Element> current = GetCanPutElement(shape);
		if (current != null) {
			m_lstOldElement = current;
			for (int i = 0; i < current.Count; i++) {
				current[i].SetDarkColor(shape.f_eColorType);
			}
		}
	}

	private void OnMouseUp(TestDraw shape) {
		List<Element> current = GetCanPutElement(shape);
		if (current == null) {
			shape.ReturnStart();
			m_lstOldElement.Clear();
			return;
		}
		Destroy(shape.gameObject);
		EventMgr.MouseUpDelete(shape.transform);
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
    
	private List<Element> GetCanPutElement(TestDraw shape) {
		List<Element> lstRetElement = null;
		List<Element> lstElement = shape.GetAllElement();
		Element eleFirst = lstElement[0];
		for (int i = 0; i < m_lstBackElement.Count; i++) {
			Element element = m_lstBackElement[i];
			if (!element.CheckIsEmpty()) {
				continue;
			}
			float offset = Math.Abs(Vector3.Distance(element.transform.position, eleFirst.transform.position));
			if (offset < 0.25f) {
				lstRetElement = CanPutShape(lstElement, element.f_uRow, element.f_uCol);
			}
		}
		return lstRetElement;
	}

	private List<Element> CanPutShape(List<Element> lstElement, int uFirstRow, int uFirstCol) {
		List<Element> lstRetElement = new List<Element>();
		lstRetElement.Add(m_arrElement[uFirstRow, uFirstCol]);
		Element eleFirst = lstElement[0];
		for (int i = 1; i < lstElement.Count; i++) {
			Element eleOther = lstElement[i];
			int uRow = uFirstRow + eleOther.f_uRow - eleFirst.f_uRow;
			int uCol = uFirstCol + eleOther.f_uCol - eleFirst.f_uCol;
			if (uRow < 0 || uRow >= m_rowCount || uCol < 0 || uCol >= m_colCount) {
				return null;
			}
			Element eleBack = m_arrElement[uRow, uCol];
			if (eleBack == null || !eleBack.CheckIsEmpty()) {
				return null;
			}
			lstRetElement.Add(eleBack);
		}
		return lstRetElement;
	}

	public bool CheckCanContinue(TestDraw shape) {
		List<Element> lstElement = shape.GetAllElement();
		if (lstElement.Count <= 1) {
			return true;
		}
		for (int row = 0; row < m_arrElement.GetLength(0); row++) {
			for (int col = 0; col < m_arrElement.GetLength(1); col++) {
				if (m_arrElement[row, col] != null && m_arrElement[row, col].CheckIsEmpty()) {
					if (CanPutShape(lstElement, row, col) != null) {
						return true;
					}
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
