using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Creat : MonoBehaviour {
    public float offsetx, offsetY;
    public GameObject prefabBG;
    public GameObject m_Effect;
	private static Window_Creat _instance;
	private List<Transform> AllElement;//所有元素的集合
	private List<Transform[]> m_AllDelete;
	private Transform[,] ArraryEle;
	private int xDim, yDim;
	private List<Transform> OldElement = new List<Transform>();
	private Transform[] old;

	void Awake() {
		_instance = this;
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		AllElement = new List<Transform>();
    }
	
    void Start() {
		int[,] mapTab = ConfigMapsMgr.instance.GetMapsData();
		xDim = mapTab.GetLength(0);
        yDim = mapTab.GetLength(1);
        ArraryEle = new Transform[xDim, yDim];
        //根据列表实例化出来棋盘
        for (int x = 0; x < xDim; x++) {
            for (int y = 0; y < yDim; y++) {
                if (mapTab[x, y] != 0) {
                    GameObject background = Instantiate(prefabBG, GetWorldPos(x, y), Quaternion.identity);
                    background.transform.parent = transform;
                    background.transform.GetComponent<Element>().Color = ElementType.Empty;
                    background.GetComponent<Element>().GetPosition = new Vector2(x, y);
                    GameObject effect = Instantiate(m_Effect, GetWorldPos(x, y), Quaternion.identity);
                    effect.transform.parent = background.transform;
                    effect.SetActive(false);
					AllElement.Add(background.transform);
					ArraryEle[x, y] = background.transform;
				} else {
                    ArraryEle[x, y] = null;
                }
            }
        }
    }

	//可以删除左边的列，返回可以删除的该行的元素
	private bool CanDeletLeft(Transform target) {
        int i = 0;
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        for (int aa = 0; aa < xDim; aa++) {
            i = (int)(pos.y - pos.x) + aa;
            if (i >= 0 && i < yDim) {
                if (ArraryEle[aa, i] != null) {
                    if (ArraryEle[aa, i].GetComponent<Element>().Color == ElementType.Empty)
                        return false;
                }
            }
        }
        return true;
    }

	//可以删除右边的列，返回可以删除的该行的元素
	private bool CanDeletRight(Transform target) {
        int i = 0;
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        for (int aa = 0; aa < xDim; aa++) {
            i = (int)(pos.y + pos.x) - aa;
            if (i >= 0 && i < yDim) {
                if (ArraryEle[aa, i] != null) {
                    if (ArraryEle[aa, i].GetComponent<Element>().Color == ElementType.Empty)
                        return false;
                }
            }
        }
        return true;
    }

	//可以删除该行的列，返回可以删除的该行的元素
	private bool CanDeletLine(Transform target) {
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        for (int aa = 0; aa < yDim; aa++) {
            if (ArraryEle[(int)pos.x, aa] != null) {
                if (ArraryEle[(int)pos.x, aa].GetComponent<Element>().Color == ElementType.Empty)
                    return false;
            }
        }
        return true;
    }

	//删除该行的列
	private void DeleLine(Transform target) {
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        List<Transform> CanDelete = new List<Transform>();
        for (int aa = 0; aa < yDim; aa++) {
            if (ArraryEle[(int)pos.x, aa] != null) {
                if(!CanDelete.Contains(ArraryEle[(int)pos.x, aa]))
                    CanDelete.Add(ArraryEle[(int)pos.x, aa]);
            }
        }
        Transform[] tf = new Transform[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
        if (!HasArray(m_AllDelete, tf)) {
            m_AllDelete.Add(tf);
        }
    }

	//删除该行的列，把该行的元素改为空的
	private void DeleLeft(Transform target) {
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        int ii = 0;
        List<Transform> CanDelete = new List<Transform>();
        for (int aa = 0; aa < xDim; aa++) {
            ii = (int)(pos.y - pos.x) + aa;
            if (ii >= 0 && ii < yDim) {
                if (ArraryEle[aa, ii] != null) {
                    if(!CanDelete.Contains(ArraryEle[aa, ii]))
                        CanDelete.Add(ArraryEle[aa, ii]);
                }
            }
        }
        Transform[] tf = new Transform[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
        if (!HasArray(m_AllDelete, tf)) {
            m_AllDelete.Add(tf);
        }
    }

	//删除该行的列，把该行的元素改为空的
	private void DeleRight(Transform target) {
        Vector2 pos = target.GetComponent<Element>().GetPosition;
        int ii = 0;
        List<Transform> CanDelete = new List<Transform>();
        for (int aa = 0; aa < xDim; aa++) {
            ii = (int)(pos.y + pos.x) - aa;
            if (ii >= 0 && ii < yDim) {
                if (ArraryEle[aa, ii] != null) {
                    if (!CanDelete.Contains(ArraryEle[aa, ii]))
                        CanDelete.Add(ArraryEle[aa, ii]);
                }
            }
        }
        Transform[] tf = new Transform[CanDelete.Count];
        for (int i = 0; i < tf.Length; i++) {
            tf[i] = CanDelete[i];
        }
        if (!HasArray(m_AllDelete,tf)) {
            m_AllDelete.Add(tf);
        }
    }

	private bool HasArray(List<Transform[]> all,Transform[] tt) {
        if (all.Count == 0)
			return false;
        for (int j = 0; j < all.Count; j++) {
            if(IsEqu(all[j], tt)) {
                return true;
            }
        }
        return false;
    }

	private bool IsEqu(Transform[]a, Transform[] b) {
        if (a.Length != b.Length)
			return false;
        if (a.Length == b.Length) {
            for (int i = 0; i < a.Length; i++) {
                if (a[i] != b[i])
                    return false;
            }
        }
        return true;
    }
    
    private Vector2  GetWorldPos(int x, int y) {
        return new Vector2((y - (yDim - 1) / 2.0f) * offsetx + transform.position.x, ((xDim - 1) / 2.0f - x) * offsetY + transform.position.y);
    }
	
	private void OnMouseDown(Transform[] tran) {
		if (tran == null || tran == old)
			return;
		Vector3[] pos = new Vector3[tran.Length];
		ElementType currenttype = tran[0].GetComponent<Element>().Color;
		for (int i = 0; i < tran.Length; i++) {
			pos[i] = tran[i].position;
		}
		for (int i = 0; i < OldElement.Count; i++) {
			OldElement[i].GetComponent<Element>().Color = ElementType.Empty;
		}
		Transform[] current = ComPos(pos);
		OldElement.Clear();
		if (current != null && IsVer(current, currenttype)) {
			for (int i = 0; i < current.Length; i++) {
				current[i].GetComponent<Element>().Color = currenttype + 1;
				old = tran;
				OldElement.Add(current[i]);
			}
		}
	}

	private void OnMouseUp(Transform[] tran) {
		ElementType currenttype;
		currenttype = tran[0].GetComponent<Element>().Color;
		Vector3[] poss = new Vector3[tran.Length];
		for (int i = 0; i < tran.Length; i++) {
			poss[i] = tran[i].position;
		}
		Transform[] current = ComPos(poss);
		if (current == null || !IsVer(current, currenttype)) {
			tran[0].parent.GetComponent<TestDraw>().ReturnStart();
			OldElement.Clear();
			return;
		}
		Destroy(tran[0].parent.gameObject);
		EventMgr.MouseUpDelete(tran[0].parent);
		EventMgr.MouseUpCreateByIndex();
		m_AllDelete = new List<Transform[]>();
		Transform[] tf = new Transform[OldElement.Count];
		for (int i = 0; i < OldElement.Count; i++) {
			Transform trans = OldElement[i];
			trans.GetComponent<Element>().Color -= 1;
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
	private bool IsVer(Transform[] current, ElementType et) {
        for (int i = 0; i < current.Length; i++) {
            if (current[i] == null)
				return false;
            if (current[i].GetComponent<Element>().Color > ElementType.Empty && current[i].GetComponent<Element>().Color != et + 1) {
                return false;
            }
        }
        return true;
    }

    //根据坐标获取当前的元素
	private Transform[] ComPos(Vector3[] pos) {
        Transform[] current = new Transform[pos.Length];
        if (AllElement != null) {
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
        return null;
    }

	public bool CheckCanContinue(Vector3[] posPre) {
		if (posPre == null)
			return true;
		if (posPre.Length > 1) {
			for (int i = 0; i < AllElement.Count; i++) {
				if (AllElement[i].GetComponent<Element>().Color == ElementType.Empty) {
					Vector3[] DicPos = new Vector3[posPre.Length];
					for (int j = 0; j < posPre.Length; j++) {
						DicPos[j] = AllElement[i].position + posPre[j];
					}
					Transform[] trans = ComPos(DicPos);
					if (HasType(trans)) {
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool HasType(Transform[] trans) {
		if (trans == null)
			return false;
		if (trans != null) {
			for (int j = 0; j < trans.Length; j++) {
				if (trans[j] == null)
					return false;
				if (trans[j] != null) {
					if (trans[j].GetComponent<Element>().Color != ElementType.Empty)
						return false;
				}
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
