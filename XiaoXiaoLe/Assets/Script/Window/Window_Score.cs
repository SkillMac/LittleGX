using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Score : MonoBehaviour {

    public float Speed;
    
    Vector3 pos;

    public float dex;
    float timer;
    private Animator m_anima;

    //IsMove
    // Use this for initialization
    void Start () {
        m_anima = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        wait();

        if(dex <=0)
        {
            m_anima.SetBool("IsMove", true);
        }
    }

  
    private void OnEnable()
    {
        StartCoroutine(Close());
        timer = Time.realtimeSinceStartup;
    }

    void wait()
    {
        if (Time.realtimeSinceStartup - timer > 0.5f)
        {
            timer = Time.realtimeSinceStartup;
            dex--;
            Debug.Log(dex);
        }
    }

    IEnumerator Close()
    {
        //yield return new WaitForSeconds(dex);

        pos = transform.position;
        
        for(int i =0;i<35;i++)
        {
            pos.y += 0.01f;
            transform.position = pos;

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.SetActive(false);
    }
}

public enum ScoreType
{
    m_0 = 0,
    m_1 = 1,
    m_5 = 5,
    m_6 = 6,
    m_7,
    m_8,
    m_9,
}
