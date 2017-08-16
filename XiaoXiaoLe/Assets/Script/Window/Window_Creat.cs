using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
    public float offsetx, offsetY;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private static Window_Creat _instance;
	private List<Element> AllElement;//所有元素的集合
	private List<Element[]> m_AllDelete;
	private Element[,] ArraryEle;
	private int xDim, yDim;
	private List<Element> OldElement = new List<Element>();
	private Transform[] old;

	void Awake() {
		_instance = this;
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		AllElement = new List<Element>();
    }
	
    void Start() {
		int[,] mapTab = ConfigMapsMgr.instance.GetMapsData();
		xDim = mapTab.GetLength(0);
        yDim = mapTab.GetLength(1);
        ArraryEle = new Element[xDim, yDim];
        //根据列表实例化出来棋盘
        for (int x = 0; x < xDim; x++) {
            for (int y = 0; y < yDim; y++) {
                if (mapTab[x, y] != 0) {
                    GameObject background = Instantiate(prefabBG, GetWorldPos(x, y), Quaternion.identity);
                    background.transform.parent = transform;
					Element ele = background.GetComponent<Element>();
					ele.ResetColor();
					ele.pos = new Pos2Int(x, y);
                    GameObject effect = Instantiate(m_Effect, GetWorldPos(x, y), Quaternion.identity);
                    effect.transform.parent = background.transform;
                    effect.SetActive(false);
					AllElement.Add(ele);
					ArraryEle[x, y] = ele;
				} else {
                    ArraryEle[x, y] = null;
                }
            }
        }
    }

	//可以删除左边的列，返回可以删除的该行的元素
	private bool CanDeletLeft(Element target) {
		bool bFlag = false;
        int i = 0;
		Pos2Int pos = target.pos;
        for (int aa = 0; aa < xDim; aa++) {
            i = pos.y - pos.x + aa;
            if (i >= 0 && i < yDim) {
				Element element = ArraryEle[aa, i];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return false;
					}
					if (!element.IsInDeleteList()) {
						bFlag = true;
					}
                }
            }
        }
        return bFlag;
    }

	//可以删除右边的列，返回可以删除的该行的元素
	private bool CanDeletRight(Element target) {
		bool bFlag = false;
		int i = 0;
		Pos2Int pos = target.pos;
        for (int aa = 0; aa < xDim; aa++) {
            i = pos.y + pos.x - aa;
            if (i >= 0 && i < yDim) {
				Element element = ArraryEle[aa, i];
				if (element != null) {
					if (element.CheckIsEmpty()) {
						return false;
					}
					if (!element.IsInDeleteList()) {
						bFlag = true;
					}
				}
            }
        }
        return bFlag;
    }

	//可以删除该行的列，返回可以删除的该行的元素
	private bool CanDeletLine(Element target) {
		bool bFlag = false;
		Pos2Int pos = target.pos;
        for (int aa = 0; aa < yDim; aa++) {
			Element element = ArraryEle[pos.x, aa];
			if (element != null) {
				if (element.CheckIsEmpty()) {
					return false;
				}
				if (!element.IsInDeleteList()) {
					bFlag = true;
				}
			}
        }
        return bFlag;
    }

	//删除该行的列
	private void DeleLine(Element target) {
		Pos2Int pos = target.pos;
        List<Element> CanDelete = new List<Element>();
        for (int aa = 0; aa < yDim; aa++) {
			Element element = ArraryEle[pos.x, aa];
			if (element != null) {
				element.SetInDeleteList();
				CanDelete.Add(element);
            }
        }
		Element[] tf = new Element[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
		m_AllDelete.Add(tf);
	}

	//删除该行的列，把该行的元素改为空的
	private void DeleLeft(Element target) {
		Pos2Int pos = target.pos;
        int ii = 0;
        List<Element> CanDelete = new List<Element>();
        for (int aa = 0; aa < xDim; aa++) {
            ii = pos.y - pos.x + aa;
            if (ii >= 0 && ii < yDim) {
				Element element = ArraryEle[aa, ii];
				if (element != null) {
					element.SetInDeleteList();
					CanDelete.Add(element);
                }
            }
        }
		Element[] tf = new Element[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
		m_AllDelete.Add(tf);
	}

	//删除该行的列，把该行的元素改为空的
	private void DeleRight(Element target) {
		Pos2Int pos = target.pos;
        int ii = 0;
        List<Element> CanDelete = new List<Element>();
        for (int aa = 0; aa < xDim; aa++) {
            ii = pos.y + pos.x - aa;
            if (ii >= 0 && ii < yDim) {
				Element element = ArraryEle[aa, ii];
				if (element != null) {
					element.SetInDeleteList();
					CanDelete.Add(ArraryEle[aa, ii]);
				}
            }
        }
		Element[] tf = new Element[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
		m_AllDelete.Add(tf);
	}
	
    private Vector2 GetWorldPos(int x, int y) {
        return new Vector2((y - (yDim - 1) / 2.0f) * offsetx + transform.position.x, ((xDim - 1) / 2.0f - x) * offsetY + transform.position.y);
    }
	
	private void OnMouseDown(Transform[] tran) {
		if (tran == null || tran == old)
			return;
		Vector3[] pos = new Vector3[tran.Length];
		ElementType currenttype = tran[0].GetComponent<Element>().colorType;
		for (int i = 0; i < tran.Length; i++) {
			pos[i] = tran[i].position;
		}
		for (int i = 0; i < OldElement.Count; i++) {
			OldElement[i].ResetColor();
		}
		Element[] current = ComPos(pos);
		OldElement.Clear();
		if (current != null && IsVer(current, currenttype)) {
			for (int i = 0; i < current.Length; i++) {
				current[i].colorType = currenttype + 1;
				old = tran;
				OldElement.Add(current[i]);
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
			OldElement.Clear();
			return;
		}
		Destroy(tran[0].parent.gameObject);
		EventMgr.MouseUpDelete(tran[0].parent);
		EventMgr.MouseUpCreateByIndex();
		m_AllDelete = new List<Element[]>();
		Element[] tf = new Element[OldElement.Count];
		for (int i = 0; i < OldElement.Count; i++) {
			Element trans = OldElement[i];
			trans.colorType -= 1;
			if (CanDeletLine(trans)) {
				DeleLine(trans);
			}
			if (CanDeletLeft(trans)) {
				DeleLeft(trans);
			}
			if (CanDeletRight(trans)) {
				DeleRight(trans);
			}
			tf[i] = trans;
		}
		if (m_AllDelete.Count == 0) {
			m_AllDelete.Add(tf);
		}
		EventMgr.Delete(m_AllDelete);
		OldElement.Clear();
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
			for (int i = 0; i < AllElement.Count; i++) {
				float offset = Math.Abs(Vector3.Distance(AllElement[i].position, pos[a]));
				if (offset < 0.25f) {
					current[a] = AllElement[i];
				}
			}
		}
		return current;
    }

	public bool CheckCanContinue(Vector3[] posPre) {
		if (posPre == null)
			return true;
		for (int i = 0; i < AllElement.Count; i++) {
			if (AllElement[i].CheckIsEmpty()) {
				Vector3[] DicPos = new Vector3[posPre.Length];
				for (int j = 0; j < posPre.Length; j++) {
					DicPos[j] = AllElement[i].position + posPre[j];
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
