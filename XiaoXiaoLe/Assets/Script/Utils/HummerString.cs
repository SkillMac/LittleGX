
public class HummerString {
	public static string FormatNum(int num) {
		string str = string.Format("{0:N}", num);
		string[] temp = str.Split('.');
		return temp[0];
	}
}
