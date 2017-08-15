using System.Collections.Generic;
using UnityEngine;

public class ConfigPrefabsMgr {
	private static ConfigPrefabsMgr _instance;
	private Dictionary<string, int[]> prefabsData;

	private ConfigPrefabsMgr() {

	}

	public static ConfigPrefabsMgr instance {
		get {
			if (_instance == null) {
				_instance = new ConfigPrefabsMgr();
			}
			return _instance;
		}
	}

	public void InitData(string[,] datas) {
		if (prefabsData != null) {
			Debug.LogError("ConfigPrefabsMgr already Init");
			return;
		}
		prefabsData = new Dictionary<string, int[]>();
		int rowCount = datas.GetLength(0);
		int colCount = datas.GetLength(1);
		for (int i = 0; i < rowCount; i++) {
			string id = datas[i, 0];
			int[] rowData = new int[colCount - 1];
			for (int j = 1; j < colCount; j++) {
				rowData[j - 1] = int.Parse(datas[i, j]);
			}
			prefabsData.Add(id, rowData);
		}
	}

	public int GetData(string id, int index) {
		return prefabsData[id][index];
	}
}
