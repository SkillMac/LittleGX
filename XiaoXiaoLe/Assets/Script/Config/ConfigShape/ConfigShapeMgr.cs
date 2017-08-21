using UnityEngine;

public class ConfigShapeMgr {
	private static ConfigShapeMgr _instance;
	private ShapeListVO m_shapeListVO;

	private ConfigShapeMgr() {

	}

	public static ConfigShapeMgr instance {
		get {
			if (_instance == null) {
				_instance = new ConfigShapeMgr();
			}
			return _instance;
		}
	}

	public void InitData(string sJsonData) {
		if (m_shapeListVO != null) {
			Debug.LogError("ConfigShapeMgr already Init");
			return;
		}
		m_shapeListVO = JsonUtility.FromJson<ShapeListVO>(sJsonData);
	}

	public ShapeItemVO GetShape(int uIndex) {
		return m_shapeListVO.lstItem[uIndex];
	}
	
	public int rowCount {
		get {
			return m_shapeListVO.rowCount;
		}
	}

	public int colCount {
		get {
			return m_shapeListVO.colCount;
		}
	}
}
