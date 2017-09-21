using UnityEngine;

public class ConfigMapsMgr {
	private static ConfigMapsMgr _instance;
	private int[,] mapsData;

	private ConfigMapsMgr() {

	}

	public static ConfigMapsMgr instance {
		get {
			if (_instance == null) {
				_instance = new ConfigMapsMgr();
			}
			return _instance;
		}
	}

	public void InitData(string[,] datas) {
		if (mapsData != null) {
			Debug.LogError("ConfigMapsMgr already Init");
			return;
		}
		int rowCount = datas.GetLength(0);
		int colCount = datas.GetLength(1);
		mapsData = new int[rowCount, colCount];
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < colCount; j++) {
				mapsData[i, j] = int.Parse(datas[i, j]);
			}
		}
	}

	public int[,] GetMapsData() {
		return mapsData;
	}
}
