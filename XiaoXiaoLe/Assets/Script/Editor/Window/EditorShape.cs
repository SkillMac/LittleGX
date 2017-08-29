using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorShape : EditorWindow {
	private const int MAX_BIT = 32;
	private const string TXT_PATH = "/Resources/Tab/shapeConfig.txt";
	private ShapeListVO m_shapeListVO;
	private List<bool> m_lstItemFlag;
	private Vector2 m_vec2Pos;
	private GameObject m_goShow;
	private List<Pos2Int> m_lstChildPos = new List<Pos2Int>();
	private int m_uLastShowIndex = int.MaxValue;

	[MenuItem("Hummer/Shape Editor")]
	static void ShapeEditor() {
		EditorShape window = (EditorShape)GetWindow(typeof(EditorShape));
		window.Show();
	}

	void OnEnable() {
		StreamReader sr = new StreamReader(Application.dataPath + TXT_PATH);
		string str = sr.ReadLine();
		sr.Close();
		sr.Dispose();
		m_shapeListVO = JsonUtility.FromJson<ShapeListVO>(str);
		m_lstItemFlag = new List<bool>();
		for (int i = 0; i < m_shapeListVO.lstItem.Count; i++) {
			m_lstItemFlag.Add(false);
		}
		m_goShow = new GameObject("ShowObject" + DateTime.Now.Ticks);
	}

	void OnDestroy() {
		DestroyImmediate(m_goShow);
	}

	void OnGUI() {
		RefreshHead();
		m_vec2Pos = EditorGUILayout.BeginScrollView(m_vec2Pos);
		RefreshItem();
		EditorGUILayout.EndScrollView();
		RefreshBottom();
	}

	private void RefreshBottom() {
		EditorGUILayout.Space();
		if (GUILayout.Button("Add New Shape")) {
			AddNewItem();
		}
		EditorGUILayout.Space();
	}

	private void RefreshHead() {
		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Row", GUILayout.MaxWidth(50));
		m_shapeListVO.rowCount = EditorGUILayout.IntField(m_shapeListVO.rowCount, GUILayout.MaxWidth(50));
		EditorGUILayout.LabelField("Col", GUILayout.MaxWidth(50));
		m_shapeListVO.colCount = EditorGUILayout.IntField(m_shapeListVO.colCount, GUILayout.MaxWidth(50));
		EditorGUILayout.EndHorizontal();
		EditorGUI.EndDisabledGroup();
		EditorGUILayout.LabelField("Item Count:", m_shapeListVO.lstItem.Count.ToString());
		EditorGUILayout.Space();
		if (GUILayout.Button("Update & Save")) {
			SaveData();
		}
		EditorGUILayout.Space();
	}

	private void RefreshItem() {
		for (int i = 0; i < m_shapeListVO.lstItem.Count; i++) {
			EditorGUILayout.BeginHorizontal();
			m_lstItemFlag[i] = EditorGUILayout.Foldout(m_lstItemFlag[i], "Shape Item " + (i + 1));
			if (GUILayout.Button("Show", GUILayout.Width(80f))) {
				m_lstItemFlag[i] = true;
			}
			if (GUILayout.Button("X", GUILayout.Width(22f))) {
				RemoveItem(i);
				continue;
			}
			EditorGUILayout.EndHorizontal();
			if (m_lstItemFlag[i]) {
				if (i != m_uLastShowIndex) {
					ResetShowItem(i);
				}
				CloseOther(i);
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(15f);
				EColorType oldType = m_shapeListVO.lstItem[i].colorType;
				m_shapeListVO.lstItem[i].colorType = (EColorType)EditorGUILayout.EnumPopup("ColorType", m_shapeListVO.lstItem[i].colorType);
				if (oldType != m_shapeListVO.lstItem[i].colorType) {
					ResetItemColor();
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(15f);
				m_shapeListVO.lstItem[i].preType = (PrefabsType)EditorGUILayout.EnumPopup("PreType", m_shapeListVO.lstItem[i].preType);
				EditorGUILayout.EndHorizontal();
				RefreshMap(i);
			}
		}
	}

	private void RefreshMap(int index) {
		for (int row = 0; row < m_shapeListVO.rowCount; row++) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(15f);
			if (row % 2 == 1) {
				GUILayout.Space(12f);
			}
			for (int col = 0; col < m_shapeListVO.colCount; col++) {
				bool bOldFlag = m_shapeListVO.lstItem[index][row, col];
				m_shapeListVO.lstItem[index][row, col] = EditorGUILayout.Toggle(m_shapeListVO.lstItem[index][row, col], GUILayout.MaxWidth(20));
				if (bOldFlag != m_shapeListVO.lstItem[index][row, col]) {
					ChangeChild(row, col, !bOldFlag);
				}
			}
			EditorGUILayout.EndHorizontal();
		}
	}

	private void CloseOther(int index) {
		for (int i = 0; i < m_lstItemFlag.Count; i++) {
			if (index != i) {
				m_lstItemFlag[i] = false;
			}
		}
	}

	private void AddNewItem() {
		m_shapeListVO.lstItem.Add(new ShapeItemVO(m_shapeListVO.rowCount, m_shapeListVO.colCount));
		CloseOther(int.MaxValue);
		m_lstItemFlag.Add(true);
	}

	private void RemoveItem(int index) {
		m_shapeListVO.lstItem.RemoveAt(index);
		m_lstItemFlag.RemoveAt(index);
	}

	private void SaveData() {
		string str = JsonUtility.ToJson(m_shapeListVO);
		StreamWriter sw = new StreamWriter(Application.dataPath + TXT_PATH);
		sw.Write(str);
		sw.Close();
		sw.Dispose();
		ShowNotification(new GUIContent("Save Success!!!"));
	}
	
	private void ResetItemColor() {
		for (int i = 0; i < m_goShow.transform.childCount; i++) {
			SetBlockColor(m_goShow.transform.GetChild(i), m_shapeListVO.lstItem[m_uLastShowIndex].colorType);
		}
	}

	private void ResetShowItem(int uIndex) {
		m_uLastShowIndex = uIndex;
		for (int i = m_goShow.transform.childCount - 1; i >= 0; i--) {
			DestroyImmediate(m_goShow.transform.GetChild(i).gameObject);
		}
		for (int row = 0; row < m_shapeListVO.rowCount; row++) {
			for (int col = 0; col < m_shapeListVO.colCount; col++) {
				if (m_shapeListVO.lstItem[uIndex][row, col]) {
					ChangeChild(row, col, true);
				}
			}
		}
	}

	private void ChangeChild(int row, int col, bool bFlag) {
		string childName = "block_" + row + "_" + col;
		if (bFlag) {
			Transform child = m_goShow.transform.Find(childName);
			if (child != null) {
				return;
			}
			GameObject go = PrefabsFactory.CreateBlock();
			go.transform.parent = m_goShow.transform;
			go.transform.localScale = Vector3.one * 0.15f;
			go.name = childName;
			SetBlockColor(go.transform, m_shapeListVO.lstItem[m_uLastShowIndex].colorType);
			m_lstChildPos.Add(new Pos2Int(col, row));
		} else {
			Transform child = m_goShow.transform.Find(childName);
			if (child != null) {
				DestroyImmediate(child.gameObject);
			}
			m_lstChildPos.Remove(new Pos2Int(col, row));
		}
		Vector3 offsetPos = GetOffsetPos();
		for (int l_row = 0; l_row < m_shapeListVO.rowCount; l_row++) {
			for (int l_col = 0; l_col < m_shapeListVO.colCount; l_col++) {
				string l_childName = "block_" + l_row + "_" + l_col;
				Transform trans = m_goShow.transform.Find(l_childName);
				if (trans != null) {
					ResetPosition(trans, l_row, l_col, offsetPos);
				}
			}
		}
	}
	
	private Vector3 GetOffsetPos() {
		int left = int.MaxValue, right = 0, top = int.MaxValue, bottom = 0;
		for (int i = 0; i < m_lstChildPos.Count; i++) {
			Pos2Int pos = m_lstChildPos[i];
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

	private static void ResetPosition(Transform trans, int row, int col, Vector3 offsetPos) {
		float offset = 0f;
		if (row % 2 == 1) {
			offset = Element.ELEMENT_WIDTH / 2;
		}
		Vector3 vec3Pos = new Vector3(col * Element.ELEMENT_WIDTH + offset, -row * Element.ELEMENT_HEIGHT) + offsetPos;
		trans.localPosition = vec3Pos;
	}

	private static void SetBlockColor(Transform trans, EColorType color) {
		SpriteRenderer spriteRenderer = trans.GetComponentInChildren<SpriteRenderer>();
		spriteRenderer.sprite = HummerRes.LoadElementSprite((int)color);
	}
}
