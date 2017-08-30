using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
	private const float COL_GAP = 0.6f;
	private const float ROW_GAP = 0.5f;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private List<BackElement> m_lstBackElement;//所有元素的集合
	private List<List<BackElement>> m_lstDelLine = new List<List<BackElement>>();
	private BackElement[,] m_arrElement;
	private int m_rowCount;
	private int m_colCount;
	private List<BackElement> m_lstOldElement = new List<BackElement>();

	void Awake() {
		GameMgr.instance.f_windowCreate = this;
		m_lstBackElement = new List<BackElement>();
    }
	
    void Start() {
		int[,] arrMapTab = ConfigMapsMgr.instance.GetMapsData();
		m_rowCount = arrMapTab.GetLength(0);
        m_colCount = arrMapTab.GetLength(1);
        m_arrElement = new BackElement[m_rowCount, m_colCount];
        //根据列表实例化出来棋盘
        for (int x = 0; x < m_rowCount; x++) {
            for (int y = 0; y < m_colCount; y++) {
                if (arrMapTab[x, y] != 0) {
                    GameObject goBack = Instantiate(prefabBG);
                    goBack.transform.parent = transform;
					goBack.transform.localPosition = GetWorldPos(x, y);
					BackElement element = goBack.GetComponent<BackElement>();
					element.ResetColor();
					element.InitPos(x, y);
					GameObject goEffect = Instantiate(m_Effect);
                    goEffect.transform.parent = goBack.transform;
					goEffect.transform.localPosition = Vector3.zero;
					goEffect.SetActive(false);
					m_lstBackElement.Add(element);
					m_arrElement[x, y] = element;
				}
            }
        }
    }
	
	private void TryDelLeft(BackElement eleTarget) {
		bool bFlag = false;
		List<BackElement> lstDelElement = new List<BackElement>();
		for (int uRow = 0; uRow < m_rowCount; uRow++) {
			int uCol = eleTarget.f_uCol + uRow - eleTarget.f_uRow;
            if (uCol >= 0 && uCol < m_colCount) {
				BackElement element = m_arrElement[uRow, uCol];
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
	
	private void TryDelRight(BackElement eleTarget) {
		bool bFlag = false;
		List<BackElement> lstDelElement = new List<BackElement>();
		for (int uRow = 0; uRow < m_rowCount; uRow++) {
			int uCol = eleTarget.f_uCol + eleTarget.f_uRow - uRow;
            if (uCol >= 0 && uCol < m_colCount) {
				BackElement element = m_arrElement[uRow, uCol];
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
	
	private void TryDelCenter(BackElement eleTarget) {
		bool bFlag = false;
		List<BackElement> listDelElement = new List<BackElement>();
		for (int uCol = 0; uCol < m_colCount; uCol++) {
			BackElement element = m_arrElement[eleTarget.f_uRow, uCol];
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
	
    private Vector2 GetWorldPos(int uRow, int uCol) {
		return new Vector2((uCol - (m_colCount - 1) / 2.0f) * COL_GAP / 2.0f, ((m_rowCount - 1) / 2.0f - uRow) * ROW_GAP);
    }
	
	public void OnMouseDown(TestDraw shape) {
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			m_lstOldElement[i].ResetColor();
		}
		m_lstOldElement.Clear();
		List<BackElement> lstCurrent = GetCanPutElement(shape);
		if (lstCurrent != null) {
			m_lstOldElement = lstCurrent;
			for (int i = 0; i < lstCurrent.Count; i++) {
				lstCurrent[i].SetDarkColor(shape.f_eColorType);
			}
		}
	}

	public void OnMouseUp(TestDraw shape) {
		List<BackElement> lstCurrent = GetCanPutElement(shape);
		if (lstCurrent == null) {
			shape.ReturnStart();
			m_lstOldElement.Clear();
			return;
		}
		shape.DestroySelf();
		GameMgr.instance.ShapeForward();
		m_lstDelLine.Clear();
		List<BackElement> lstDelElement = new List<BackElement>();
		for (int i = 0; i < m_lstOldElement.Count; i++) {
			BackElement element = m_lstOldElement[i];
			element.ApplyColor();
			TryDelCenter(element);
			TryDelLeft(element);
			TryDelRight(element);
			lstDelElement.Add(element);
		}
		if (m_lstDelLine.Count == 0) {
			m_lstDelLine.Add(lstDelElement);
		}
		GameMgr.instance.Delete(m_lstDelLine);
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
}
