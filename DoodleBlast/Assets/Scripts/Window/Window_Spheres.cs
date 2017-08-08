using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Spheres : MonoBehaviour {

    public bool IsBegin;

    public GameObject water;

    public GameObject window_win,window_Lose;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (Mathf.Abs( transform.GetChild(i).position.x) >= Screen.width /200.0f || Mathf.Abs(transform.GetChild(i).position.y) >= Screen.height /200.0f)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(IsBegin)
        {
            Activechild();
            if(!IsAllactiv())
            {
                window_Lose.SetActive(true);

                IsBegin = false;
            }
            else if (IsAllstatic())
            {
                float a = Cup.Instance.IsWin(transform);

                if (a != 0)
                {
                    if(Cup.Instance.Istrue(transform))
                    {
                        window_win.SetActive(true);
                    }
                    else
                    {
                        window_Lose.SetActive(true);
                    }

                    water.SetActive(true);

                    water.GetComponent<Water>().Move(a);

                }
                else
                {
                    window_Lose.SetActive(true);
                }
                IsBegin = false;
            }
        }
     
    }

    void Activechild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Sphere>().enabled = true;
        }
    }
    bool IsAllactiv()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    bool IsAllstatic()
    {
        if (!IsAllactiv()) return false;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                if(!transform.GetChild(i).GetComponent<Sphere>().GetStatic)
                {
                    return false;
                }
            }
        }

        return true;
    }
    public void SetRigidbody()
    {
        for(int i = 0;i < transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody2D>().simulated = true;
        }
    }
    
}
