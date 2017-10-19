using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHomeControl : MonoBehaviour {
    private Button m_Button;
    // Use this for initialization
    void Start()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        SceneManager.LoadScene("Begin");
    }
}
