using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
	private const float COL_GAP = 0.6f;
	private const float ROW_GAP = 0.5f;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private static Window_Creat _instance;
	private List<BackElement> m_lstBackElement;//所有元素的集合
	private List<List<BackElement>> m_lstDelLine;
	private BackElement[,] m_arrElement;
	private int m_rowCount;
	private int m_colCount;
	private List<BackElement> m_lstOldElement = new List<BackElement>();

	void Awake() {
		_instance = this;
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		m_lstBackElement = new List<BackElement>();
    }
	
    void Start() {
		int[,] mapTab = ConfigMapsMgr.instance.GetMapsData();
		m_rowCount = mapTab.GetLength(0);
        m_colCount = mapTab.GetLength(1);
        m_arrElement = new BackElement[m_rowCount, m_colCount];
        //根据列表实例化出来棋盘
        for (int x = 0; x < m_rowCount; x++) {
            for (int y = 0; y < m_colCount; y++) {
                if (mapTab[x, y] != 0) {
                    GameObject goBack = Instantiate(prefabBG);
                    goBack.transform.parent = transform;
					goBack.transform.localPosition = GetWorldPos(x, y);
					BackElement element = goBack.GetComponent<BackElement>();
					element.ResetColor();
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
	
	private void TryDelLeft(BackElement target) {
		bool bFlag = false;
		List<BackElement> lstDelElement = new List<BackElement>();
		for (int rowNum = 0; rowNum < m_rowCount; rowNum++) {
			int colNum = target.f_uCol + rowNum - target.f_uRow;
            if (colNum >= 0 && colNum < m_colCount) {
				BackElement element = m_arrElement[rowNum, colNum];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.f_bInDeleteList) {
						bFlag = true;
					}
                }
            }
        }
		if (bFlag) {
			AddToDelList(lstDelElement);
		}
	}
	
	private void TryDelRight(BackElement target) {
		bool bFlag = false;
		List<BackElement> lstDelElement = new List<BackElement>();
		for (int rowNum = 0; rowNum < m_rowCount; rowNum++) {
			int colNum = target.f_uCol + target.f_uRow - rowNum;
            if (colNum >= 0 && colNum < m_colCount) {
				BackElement element = m_arrElement[rowNum, colNum];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return;
					}
					lstDelElement.Add(element);
					if (!element.f_bInDeleteList) {
						bFlag = true;
					}
				}
            }
        }
		if (bFlag) {
			AddToDelList(lstDelElement);
		}
    }
	
	private void TryDelCenter(BackElement target) {
		bool bFlag = false;
		List<BackElement> listDelElement = new List<BackElement>();
		for (int colNum = 0; colNum < m_colCount; colNum++) {
			BackElement element = m_arrElement[target.f_uRow, colNum];
			if (element != null) {
				if (element.CheckIsEmpty()) {
					return;
				}
				listDelElement.Add(element);
				if (!element.f_bInDeleteList) {
					bFlag = true;
				}
			}
        }
		if (bFlag) {
			AddToDelList(listDelElement);
		}
    }

	private void AddToDelList(List<BackElement> listDelElement) {
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
		List<BackElement> current = GetCanPutElement(shape);
		if (current != null) {
			m_lstOldElement = current;
			for (int i = 0; i < current.Count; i++) {
				current[i].SetDarkColor(shape.f_eColorType);
			}
		}
	}

	private void OnMouseUp(TestDraw shape) {
		List<BackElement> current = GetCanPutElement(shape);
		if (current == null) {
			shape.ReturnStart();
			m_lstOldElement.Clear();
			return;
		}
		Destroy(shape.gameObject);
		EventMgr.MouseUpDelete(shape.transform);
		EventMgr.MouseUpCreateByIndex();
		m_lstDelLine = new List<List<BackElement>>();
		List<BackElement> tf = new List<BackElement>();
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			BackElement element = m_lstOldElement[i];
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
    
	private List<BackElement> GetCanPutElement(TestDraw shape) {
		List<BackElement> lstRetElement = null;
		List<ShapeElement> lstElement = shape.GetAllElement();
		ShapeElement eleFirst = lstElement[0];
		for (int i = 0; i < m_lstBackElement.Count; i++) {
			BackElement element = m_lstBackElement[i];
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

	private List<BackElement> CanPutShape(List<ShapeElement> lstElement, int uFirstRow, int uFirstCol) {
		List<BackElement> lstRetElement = new List<BackElement>();
		lstRetElement.Add(m_arrElement[uFirstRow, uFirstCol]);
		ShapeElement eleFirst = lstElement[0];
		for (int i = 1; i < lstElement.Count; i++) {
			ShapeElement eleOther = lstElement[i];
			int uRow = uFirstRow + eleOther.f_uRow - eleFirst.f_uRow;
			int uCol = uFirstCol + eleOther.f_uCol - eleFirst.f_uCol;
			if (uRow < 0 || uRow >= m_rowCount || uCol < 0 || uCol >= m_colCount) {
				return null;
			}
			BackElement eleBack = m_arrElement[uRow, uCol];
			if (eleBack == null || !eleBack.CheckIsEmpty()) {
				return null;
			}
			lstRetElement.Add(eleBack);
		}
		return lstRetElement;
	}

	public bool CheckCanContinue(TestDraw shape) {
		List<ShapeElement> lstElement = shape.GetAllElement();
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
