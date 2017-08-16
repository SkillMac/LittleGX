
public struct Pos2Int {
	private int _x;
	private int _y;

	public Pos2Int(int posX, int posY) {
		_x = posX;
		_y = posY;
	}

	public int x {
		get {
			return _x;
		}
	}

	public int y {
		get {
			return _y;
		}
	}
}
