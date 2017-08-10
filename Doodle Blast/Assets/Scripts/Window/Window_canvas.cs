using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_canvas : MonoBehaviour {

    public GetAnim Ga;

    public Button b_Begin;

    public Image m_But;

    public Button m_restart;

    public Button m_undo;
    public Button m_clear;

    private Button m_home;

    public static float current_Lenght;//当前长度

    private void Awake()
    {
        m_home = transform.GetChild(transform.childCount - 1).GetComponent<Button>();
    }
    // Use this for initialization
    void Start () {

        b_Begin.onClick.AddListener(Onclick_Begin);
        m_home.onClick.AddListener(OnClickHome);
        m_But.fillAmount = 1.0f;

        current_Lenght = DrawLine.m_Lenght;
    }
	
    void OnClickHome()
    {
        Loadscene.Instance.LoadScene("Begin");
    }
    void Onclick_Begin()
    {
        Ga.WaitClear();

        m_undo.gameObject.SetActive(false);
        m_clear.gameObject.SetActive(false);
        m_restart.gameObject.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBut(float offset)
    {
        offset = Mathf.Clamp01(offset);

        m_But.fillAmount = 1 - offset;

        current_Lenght = DrawLine.m_Lenght;
    }


}
