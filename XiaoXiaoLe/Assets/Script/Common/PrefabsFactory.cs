﻿using UnityEngine;

public class PrefabsFactory {
	private const string PRE_BLOCK = "Prefabs/Pre_Block";

	public static GameObject CreateBlock() {
		GameObject goPre = Resources.Load<GameObject>(PRE_BLOCK);
		GameObject go = Object.Instantiate(goPre);
		return go;
	}

	public static GameObject CreateShape(int uIndex, Transform trans) {
		GameObject go = new GameObject("Shape");
		go.transform.parent = trans;
		TestDraw shape = go.AddComponent<TestDraw>();
		ConfigShapeMgr shapeConf = ConfigShapeMgr.instance;
		ShapeItemVO shapeItemVO = shapeConf.GetShape(uIndex);
		for (int row = 0; row < shapeConf.rowCount; row++) {
			for (int col = 0; col < shapeConf.colCount * 2; col++) {
				if (shapeItemVO.GetFlag(row, col)) {
					GameObject goChild = CreateBlock();
					shape.AddChild(row, col, goChild.transform);
				}
			}
		}
		shape.InitShape(shapeItemVO.colorType);
		return go;
	}
}
