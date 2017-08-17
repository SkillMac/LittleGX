using UnityEngine;

public class PrefabsFactory {
	private const string PRE_BLOCK = "Prefabs/Pre_Block";

	public static GameObject CreateBlock() {
		GameObject goPre = Resources.Load<GameObject>(PRE_BLOCK);
		GameObject go = Object.Instantiate(goPre);
		return go;
	}
}
