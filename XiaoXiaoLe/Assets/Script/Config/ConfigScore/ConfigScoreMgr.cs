using System.Collections.Generic;
using UnityEngine;

public class ConfigScoreMgr {
	private static ConfigScoreMgr _instance;
	private Dictionary<string, int[]> scoreData;

	private ConfigScoreMgr() {

	}

	public static ConfigScoreMgr instance {
		get {
			if (_instance == null) {
				_instance = new ConfigScoreMgr();
			}
			return _instance;
		}
	}

	public void InitData(string[,] datas) {
		if (scoreData != null) {
			Debug.LogError("ConfigScoreMgr already Init");
			return;
		}
		scoreData = new Dictionary<string, int[]>();
		int rowCount = datas.GetLength(0);
		int colCount = datas.GetLength(1);
		for (int i = 1; i < rowCount; i++) {
			string id = datas[i, 0];
			int[] rowData = new int[colCount - 1];
			for (int j = 1; j < colCount; j++) {
				rowData[j - 1] = int.Parse(datas[i, j]);
			}
			scoreData.Add(id, rowData);
		}
	}
	
	public int GetScoreNum(string id) {
		return scoreData[id][0];
	}

	public int GetPathNum(string id) {
		return scoreData[id][1];
	}
}
