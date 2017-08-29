
public class BackElement : Element {
	private EColorType m_eColorType;
	private bool m_bEmpty = true;
	private bool m_bInDeleteList = false;

	public void InitPos(int uRow, int uCol) {
		m_uRow = uRow;
		m_uCol = uCol;
	}

	public void ApplyColor() {
		ResetColor(m_eColorType - 1);
		m_bEmpty = false;
	}

	public void SetDarkColor(EColorType currColor) {
		ResetColor(currColor + 1);
	}

	public void ResetColor() {
		ResetColor(EColorType.Empty);
		m_bInDeleteList = false;
		m_bEmpty = true;
	}

	public void SetInDeleteList() {
		m_bInDeleteList = true;
	}

	public bool CheckIsEmpty() {
		return m_bEmpty;
	}

	private void ResetColor(EColorType eColorType) {
		m_eColorType = eColorType;
		SetSprite(eColorType);
	}

	public bool f_bInDeleteList {
		get {
			return m_bInDeleteList;
		}
	}
}
