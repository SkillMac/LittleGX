using UnityEngine;

public class ShapeElement : Element {

	public void InitShapeElement(int uRow, int uCol, EColorType eColorType, Vector3 vec3Offset) {
		m_uRow = uRow;
		m_uCol = uCol;
		SetSprite(eColorType);
		transform.localPosition = new Vector3(m_uCol * ELEMENT_WIDTH / 2, -m_uRow * ELEMENT_HEIGHT) + vec3Offset;
	}
}
