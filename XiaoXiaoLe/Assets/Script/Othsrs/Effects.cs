using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    Color colorA;

    float timer;

    public  float dex;

    private Animator m_anima;

	// Use this for initialization
	void Start () {

        m_anima = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        wait();

        if (dex <= 1.0f)
        {
            m_anima.SetBool("IsEffect", true);
        }
        if (dex < 0f)
        {
            Dis();
        }
    }

    public void Dis()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        colorA = new Color(1, 1, 1, 1);
        timer = Time.realtimeSinceStartup;
    }
    
    void wait()
    {
        if(Time.realtimeSinceStartup - timer > 0.5f)
        {
            timer = Time.realtimeSinceStartup;
            dex--;
            Debug.Log(dex);
        }
    }
}
