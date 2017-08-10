using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    private int SelfIndex;//自身在父级下的编号

    private Button m_Button;
    
    private void Awake()
    {
        SelfIndex = transform.GetSiblingIndex();

        m_Button = GetComponent<Button>();
    }
    // Use this for initialization
    void Start () {

        m_Button.onClick.AddListener(OnClickButton);
	}
	
    void OnClickButton()
    {
        //设置需要加载的游戏场景信息
        PlayerPrefs.SetInt("CurrentLev", SelfIndex);

        SceneManager.LoadScene("Play");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
