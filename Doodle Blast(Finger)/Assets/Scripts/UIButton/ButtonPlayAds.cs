using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayAds : MonoBehaviour {
    private Button m_Button;
	// Use this for initialization
	void Start () {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnButtonClick);
	}
	
    private void OnButtonClick()
    {
        PlayRewardAds.Instance.PlayRewardAd();
    }
}
