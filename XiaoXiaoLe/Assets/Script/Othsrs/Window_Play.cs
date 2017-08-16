using System;
using System.Collections.Generic;
using UnityEngine;

public class Window_Play : MonoBehaviour {
    public List<Transform> AllElement;
    private List<Transform> OldElement = new List<Transform>();
    private Transform[] old;
	
	void Awake() {
		EventMgr.MouseUpEvent += OnMouseUp;
		EventMgr.MouseDownEvent += OnMouseDown;
		AllElement = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++) {
			Transform chlid = transform.GetChild(i);
			AllElement.Add(chlid);
		}
	}

	void Start() {
		for (int i = 0; i < AllElement.Count; i++) {
			AllElement[i].GetComponent<Element>().ResetColor();
		}
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
			if (OldElement.Count == 0)
				return;
			OldElement[i].GetComponent<Element>().ResetColor();
		}
		Transform[] current = ComPos(pos);
		if (current != null && IsVer(current)) {
			OldElement.Clear();
			for (int i = 0; i < current.Length; i++) {
				current[i].GetComponent<Element>().colorType = currenttype;
				old = tran;
				OldElement.Add(current[i]);
			}
		}
	}

	private void OnMouseUp(Transform[] arrTrans) {
		if (OldElement.Count == 0)
			return;
		for (int i = 0; i < OldElement.Count; i++) {
			AllElement.Add(OldElement[i]);
		}
		OldElement.Clear();
		EventMgr.MouseUpCreateByIndex();
	}

	//判断是否可以放进去
	private bool IsVer(Transform[] current) {
		for (int i = 0; i < current.Length; i++) {
			if (current[i] == null)
				return false;
			if (!current[i].GetComponent<Element>().CheckIsEmpty() && OldElement.Contains(current[i])) {
                return false;
            }
        }
        return true;
    }

	//根据坐标获取当前的元素
	private Transform[] ComPos(Vector3 []pos) {
        Transform[] current = new Transform[pos.Length];
		if (AllElement != null) {
			for (int a = 0; a < pos.Length; a++) {
				for (int i = 0; i < AllElement.Count; i++) {
                    float offset = Math.Abs(Vector3.Distance(AllElement[i].position, pos[a]));
					if (offset < 0.3f) {
                        current[a] = AllElement[i];
                    }
                }
            }
            return current;
        }
        return null;
    }

	void OnDestroy() {
		EventMgr.MouseUpEvent -= OnMouseUp;
		EventMgr.MouseDownEvent -= OnMouseDown;
	}
}
