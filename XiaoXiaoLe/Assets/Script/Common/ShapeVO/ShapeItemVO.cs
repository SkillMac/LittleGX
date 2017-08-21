using System;
using UnityEngine;

[Serializable]
public class ShapeItemVO {
	public ElementType colorType;
	public PrefabsType preType;
	[SerializeField]
	private int m_flagMap = 0;
	[SerializeField]
	private int m_rowCount;
	[SerializeField]
	private int m_colCount;

	public ShapeItemVO(int rowCount, int colCount) {
		m_rowCount = rowCount;
		m_colCount = colCount;
	}

	public void UpdateCount(int rowCount, int colCount) {
		int newFlag = 0;
		for (int row = 0; row < m_rowCount && row < rowCount; row++) {
			for (int col = 0; col < m_colCount && col < colCount; col++) {
				bool bFlag = GetFlag(m_flagMap, m_colCount, row, col);
				if (bFlag) {
					SetFlag(ref newFlag, colCount, row, col, true);
				}
			}
		}
		m_rowCount = rowCount;
		m_colCount = colCount;
		m_flagMap = newFlag;
	}

	public bool GetFlag(int row, int col) {
		int index = row * m_colCount * 2 + col;
		return (m_flagMap & 1 << index) != 0;
	}

	public bool this[int row, int col] {
		get {
			return GetFlag(m_flagMap, m_colCount, row, col);
		}
		set {
			SetFlag(ref m_flagMap, m_colCount, row, col, value);
		}
	}

	private static void SetFlag(ref int intFlag, int colCount, int row, int col, bool bFlag) {
		int index = GetIndex(colCount, row, col);
		if (bFlag) {
			intFlag |= 1 << index;
		} else {
			intFlag &= ~(1 << index);
		}
	}

	private static bool GetFlag(int intFlag, int colCount, int row, int col) {
		int index = GetIndex(colCount, row, col);
		return (intFlag & 1 << index) != 0;
	}
	
	private static int GetIndex(int colCount, int row, int col) {
		return row * colCount * 2 + col * 2 + row % 2;
	}
}
