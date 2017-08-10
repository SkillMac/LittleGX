using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Select : MonoBehaviour {

    public GameObject allButton;

    private void Awake()
    {
        GetVisbale();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //激活button，去掉锁
    void GetVisbale()
    {
        if (allButton == null) return;
        int lev = PlayerPrefs.GetInt("Level");

        if (lev > allButton.transform.childCount) return;

        for(int i =0;i <= lev;i++)
        {
            if(allButton.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
            {
                allButton.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                allButton.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }
}
