using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestDraw))]
public class ShapeInspector : Editor {
	private const int MAX_ROW = 4;
	private const int MAX_COL = 4;
	private int row = MAX_ROW;
	private int col = MAX_COL;
	private TestDraw shape;

	void OnEnable() {
		shape = (TestDraw)target;
		InitArr();
	}

	private void InitArr() {
		if (shape._arrShapeMap == null || shape._arrShapeMap.Length < MAX_ROW) {
			shape._arrShapeMap = new TestDraw.Row[MAX_ROW];
		}
		for (int i = 0; i < shape._arrShapeMap.Length; i++) {
			if (shape._arrShapeMap[i].arr == null || shape._arrShapeMap[i].arr.Length < MAX_COL) {
				shape._arrShapeMap[i].arr = new bool[MAX_COL];
			}
		}
	}

	public override void OnInspectorGUI() {
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Row", GUILayout.MaxWidth(50));
		row = Mathf.Clamp(EditorGUILayout.IntField(row, GUILayout.MaxWidth(50)), 1, MAX_ROW);
		EditorGUILayout.LabelField("Col", GUILayout.MaxWidth(50));
		col = Mathf.Clamp(EditorGUILayout.IntField(col, GUILayout.MaxWidth(50)), 1, MAX_COL);
		EditorGUILayout.EndHorizontal();
		for (int i = 0; i < row; i++) {
			EditorGUILayout.BeginHorizontal();
			if (i % 2 == 0) {
				EditorGUILayout.LabelField("", GUILayout.MaxWidth(8));
			}
			for (int j = 0; j < col; j++) {
				bool oldFlag = shape._arrShapeMap[i].arr[j];
				shape._arrShapeMap[i].arr[j] = EditorGUILayout.Toggle(shape._arrShapeMap[i].arr[j], GUILayout.MaxWidth(20));
				if (oldFlag != shape._arrShapeMap[i].arr[j]) {
					ChangeChild(i, j, !oldFlag);
				}
			}
			EditorGUILayout.EndHorizontal();
		}
		shape._elementType = (ElementType)EditorGUILayout.EnumPopup("ColorType", shape._elementType);
		EditorGUILayout.EndVertical();
	}

	private void ChangeChild(int row, int col, bool bFlag) {
		if (shape.name != "Pre_Shape") {
			return;
		}
		string childName = "block_" + row + "_" + col;
		if (bFlag) {
			Transform child = shape.transform.Find(childName);
			if (child != null) {
				return;
			}
			GameObject go = PrefabsFactory.CreateBlock();
			go.transform.parent = shape.transform;
			go.transform.localScale = Vector3.one * 0.15f;
			go.name = childName;
			Element element = go.GetComponent<Element>();
			element.Init(row, col, shape._elementType);
			shape.m_lstChildPos.Add(new Pos2Int(col, row));
		} else {
			Transform child = shape.transform.Find(childName);
			if (child != null) {
				DestroyImmediate(child.gameObject);
			}
			shape.m_lstChildPos.Remove(new Pos2Int(col, row));
		}
		Vector3 offsetPos = GetOffsetPos();
		for (int i = 0; i < shape.transform.childCount; i++) {
			Transform trans = shape.transform.GetChild(i);
			Element element = trans.GetComponent<Element>();
			element.ResetPosition(offsetPos);
		}
	}

	private Vector3 GetOffsetPos() {
		int left = int.MaxValue, right = 0, top = int.MaxValue, bottom = 0;
		for (int i = 0; i < shape.m_lstChildPos.Count; i++) {
			Pos2Int pos = shape.m_lstChildPos[i];
			int offset = pos.y % 2 == 0 ? 1 : 0;
			if (left > pos.x * 2 + offset) {
				left = pos.x * 2 + offset;
			}
			if (right < pos.x * 2 + offset) {
				right = pos.x * 2 + offset;
			}
			if (top > pos.y) {
				top = pos.y;
			}
			if (bottom < pos.y) {
				bottom = pos.y;
			}
		}
		float posX = ((right - left) * 0.5f + left) * Element.ELEMENT_WIDTH / 2;
		float posY = ((bottom - top) * 0.5f + top) * Element.ELEMENT_HEIGHT;
		Vector3 vec3Pos = new Vector3(-posX, posY);
		return vec3Pos;
	}
}
