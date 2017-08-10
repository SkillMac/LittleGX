using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {
    
    float timer;

    public  int dex = 1;
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        wait();

        if (dex <=0)
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
        dex = 1;
        timer = Time.realtimeSinceStartup;
    }
    
    void wait()
    {
        if(Time.realtimeSinceStartup - timer > 1.0f)
        {
            timer = Time.realtimeSinceStartup;
            dex--;
            Debug.Log(dex);
        }
    }
}
