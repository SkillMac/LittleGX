using UnityEngine;

public class Gold : MonoBehaviour {
    public Transform sun;
    public Transform gold;
    public float yoffset;
    public Vector3 target;
	public int startAni;//开始闪烁的时间
	public int Disable;//物体消失的时间
	private bool IsMoved = false;
	private float speed;
	private bool canmove = false;
	private Vector3 MousePos;
	private int count;
	private float timer;
	private Vector3 firstPos;
	private Animator ani;
	
	void Start() {
        ani = GetComponent<Animator>();
        speed = 5.0f;
		firstPos = new Vector3(transform.position.x, yoffset, 0);
    }
	
	void Update() {
		if (!canmove) {
            MoveToPos(firstPos);
        }
		if (transform.position == firstPos) {
            waittime();
			if (startAni == 0) {
                ani.SetBool("BeginAni", true);
            }
			if (Disable == 0) {
                Destroy(gameObject);
            }
        }
		if (IsMoved) {
            sun.gameObject.SetActive(false);
            ani.SetBool("BeginAni", false);
            canmove = true;
            MoveToPos(target);
        }
		if (transform.position == target) {
			if (count == 0) {
				GameMgr.instance.AddGold();
            }
            count++;
            ani.SetTrigger("IsColor");
            Destroy(gameObject, 0.2f);
        }
		if (Input.GetMouseButtonDown(0)) {
            MousePos = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) / 100.0f;
			if (Mathf.Abs(Vector3.Distance(transform.position, MousePos)) < 1.0f) {
                IsMoved = true;
				GameMgr.instance.AddScore(transform.position);
			}
        }
    }

	void MoveToPos(Vector3 pos) {
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }


	void waittime() {
		if(Time.realtimeSinceStartup - timer > 1.0f) {
            timer = Time.realtimeSinceStartup;
            startAni--;
            Disable--;
        }
    }
}
