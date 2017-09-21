using UnityEngine;

public class ConfigMgr {
	public const string CONFIG_SHAPE = "Tab/shapeConfig";
	private const string CONFIG_MAPS = "Tab/Maps";
	private const string CONFIG_PREFABS = "Tab/prefabtype";
	private const string CONFIG_SCORE = "Tab/Score";

	public static void LoadConfig() {
		ConfigMapsMgr.instance.InitData(Load(CONFIG_MAPS));
		ConfigPrefabsMgr.instance.InitData(Load(CONFIG_PREFABS));
		ConfigScoreMgr.instance.InitData(Load(CONFIG_SCORE));
		ConfigShapeMgr.instance.InitData(LoadJson(CONFIG_SHAPE));
	}

	private static string[,] Load(string path) {
		TextAsset ta = Resources.Load<TextAsset>(path);
		string[,] datas = new string[1, 1];
		string[] buff = ta.text.Split('\n');
		for (int i = 0; i < buff.Length; i++) {
			string[] temp = buff[i].Split('\t');
			if (i == 0) {
				datas = new string[buff.Length, temp.Length];
			}
			for (int j = 0; j < temp.Length; j++) {
				datas[i, j] = temp[j];
			}
		}
		return datas;
	}

	private static string LoadJson(string path) {
		TextAsset ta = Resources.Load<TextAsset>(path);
		return ta.text;
	}
}
