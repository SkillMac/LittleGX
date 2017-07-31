using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

    public Transform sun;
    public Transform gold;

    public float yoffset;

    public bool IsMoved;
    private bool canmove;

    public float speed;

    public Vector3 target;

    private Vector3 MousePos;

    int count;

    public int startAni;//开始闪烁的时间
    public int Disable;//物体消失的时间

    float timer;
    Vector3 firstPos;
    Animator ani;

    // Use this for initialization
    void Start () {

        ani = GetComponent<Animator>();
        speed = 5.0f;
        firstPos = new Vector3(transform.position.x, yoffset, 0);
    }
	
	// Update is called once per frame
	void Update () {

        if(!canmove)
        {
            MoveToPos(firstPos);
        }
       
        if(transform.position == firstPos)
        {
            waittime();
            if(startAni == 0)
            {
                ani.SetBool("BeginAni", true);
            }
            if(Disable == 0)
            {
                Destroy(gameObject);
            }
        }

        if(IsMoved)
        {
            sun.gameObject.SetActive(false);
            ani.SetBool("BeginAni", false);
            canmove = true;

            MoveToPos(target);
        }

        if(transform.position == target)
        {
            if(count == 0)
            {
                MessageCenter.ReceiveMassage(new MessageData(MessageType.UI_ADDGOLD, null));
            }
            count++;

            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
            gold.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
            Destroy(gameObject, 0.2f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            MousePos = (Input.mousePosition - new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)) / 100.0f;
            
                if (Mathf.Abs(Vector3.Distance(transform.position, MousePos)) < 1.0f)
                {
                    IsMoved = true;
                    MessageCenter.ReceiveMassage(new MessageData(MessageType.UI_ADDSCORE, transform.position));
                }
        }
    }

    void MoveToPos(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }


    void waittime()
    {
        if(Time.realtimeSinceStartup - timer >1.0f)
        {
            timer = Time.realtimeSinceStartup;
            startAni--;
            Disable--;
        }
    }

}
