using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorShape : EditorWindow {
	private const int MAX_BIT = 32;
	private const string TXT_PATH = "/Resources/Tab/shapeConfig.txt";
	private ShapeListVO shapeListVO;
	private List<bool> lstItemFlag;
	private Vector2 pos;
	private bool bShowTips = false;

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
		shapeListVO = JsonUtility.FromJson<ShapeListVO>(str);
		Debug.Log(str);
		lstItemFlag = new List<bool>();
		for (int i = 0; i < shapeListVO.lstItem.Count; i++) {
			lstItemFlag.Add(false);
		}
	}

	void OnGUI() {
		RefreshHead();
		pos = EditorGUILayout.BeginScrollView(pos);
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
		int oldRowCount = shapeListVO.rowCount;
		shapeListVO.rowCount = EditorGUILayout.IntField(shapeListVO.rowCount, GUILayout.MaxWidth(50));
		if (oldRowCount != shapeListVO.rowCount) {
			if (shapeListVO.rowCount * shapeListVO.colCount * 2 > MAX_BIT) {
				bShowTips = true;
				shapeListVO.rowCount = MAX_BIT / (shapeListVO.colCount * 2);
			} else {
				bShowTips = false;
			}
			ResetAllItemRowAndCol();
		}
		EditorGUILayout.LabelField("Col", GUILayout.MaxWidth(50));
		int oldColCount = shapeListVO.colCount;
		shapeListVO.colCount = EditorGUILayout.IntField(shapeListVO.colCount, GUILayout.MaxWidth(50));
		if (oldColCount != shapeListVO.colCount) {
			if (shapeListVO.rowCount * shapeListVO.colCount * 2 > MAX_BIT) {
				bShowTips = true;
				shapeListVO.colCount = MAX_BIT / shapeListVO.rowCount / 2;
			} else {
				bShowTips = false;
			}
			ResetAllItemRowAndCol();
		}
		EditorGUILayout.EndHorizontal();
		EditorGUI.EndDisabledGroup();
		if (bShowTips) {
			EditorGUILayout.LabelField("Col * 2 * Row <= " + MAX_BIT);
		}
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Item Count:", shapeListVO.lstItem.Count.ToString());
		EditorGUILayout.Space();
		if (GUILayout.Button("Update & Save")) {
			SaveData();
		}
		EditorGUILayout.Space();
	}

	private void RefreshItem() {
		for (int i = 0; i < shapeListVO.lstItem.Count; i++) {
			EditorGUILayout.BeginHorizontal();
			lstItemFlag[i] = EditorGUILayout.Foldout(lstItemFlag[i], "Shape Item " + (i + 1));
			if (GUILayout.Button("Show", GUILayout.Width(80f))) {
				lstItemFlag[i] = true;
			}
			if (GUILayout.Button("X", GUILayout.Width(22f))) {
				RemoveItem(i);
				continue;
			}
			EditorGUILayout.EndHorizontal();
			if (lstItemFlag[i]) {
				CloseOther(i);
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(15f);
				shapeListVO.lstItem[i].colorType = (ElementType)EditorGUILayout.EnumPopup("ColorType", shapeListVO.lstItem[i].colorType);
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(15f);
				shapeListVO.lstItem[i].preType = (PrefabsType)EditorGUILayout.EnumPopup("PreType", shapeListVO.lstItem[i].preType);
				EditorGUILayout.EndHorizontal();
				RefreshMap(i);
			}
		}
	}

	private void RefreshMap(int index) {
		for (int row = 0; row < shapeListVO.rowCount; row++) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(15f);
			if (row % 2 == 1) {
				GUILayout.Space(12f);
			}
			for (int col = 0; col < shapeListVO.colCount; col++) {
				shapeListVO.lstItem[index][row, col] = EditorGUILayout.Toggle(shapeListVO.lstItem[index][row, col], GUILayout.MaxWidth(20));
			}
			EditorGUILayout.EndHorizontal();
		}
	}

	private void CloseOther(int index) {
		for (int i = 0; i < lstItemFlag.Count; i++) {
			if (index != i) {
				lstItemFlag[i] = false;
			}
		}
	}

	private void AddNewItem() {
		shapeListVO.lstItem.Add(new ShapeItemVO(shapeListVO.rowCount, shapeListVO.colCount));
		CloseOther(int.MaxValue);
		lstItemFlag.Add(true);
	}

	private void RemoveItem(int index) {
		shapeListVO.lstItem.RemoveAt(index);
		lstItemFlag.RemoveAt(index);
	}

	private void SaveData() {
		string str = JsonUtility.ToJson(shapeListVO);
		StreamWriter sw = new StreamWriter(Application.dataPath + TXT_PATH);
		sw.Write(str);
		sw.Close();
		sw.Dispose();
		Debug.Log(str);
	}

	private void ResetAllItemRowAndCol() {
		for (int i = 0; i < shapeListVO.lstItem.Count; i++) {
			shapeListVO.lstItem[i].UpdateCount(shapeListVO.rowCount, shapeListVO.colCount);
		}
	}
}
