using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Lose : MonoBehaviour {

    private Button m_home;

    private void Awake()
    {
        m_home = transform.GetChild(transform.childCount - 1).GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {
        m_home.onClick.AddListener(OnClickHome);
    }
    void OnClickHome()
    {
        Loadscene.Instance.LoadScene("Begin");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
