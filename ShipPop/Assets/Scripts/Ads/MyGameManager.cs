using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MyGameManager : MonoBehaviour
{
    private InterstitialAd ad;
    private string adUnitId;
    public static MyGameManager Instance;

    void Awake()
    {
        Instance = this;

        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-6250098546319109/3957695877";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-6250098546319109/2311153321";
        #else
            adUnitId = "unexpected_platform";
        #endif
    }
    // Use this for initialization
    void Start()
    {
        ad = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);

        ad.OnAdClosed += Ad_OnAdClosed;
    }

    private void Ad_OnAdClosed(object sender, System.EventArgs e)
    {
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    public void ShowInterAD()
    {
        int count = PlayerPrefs.GetInt("InterstitialAd");
        PlayerPrefs.SetInt("InterstitialAd", count + 1);
        Debug.Log(count);
        if (ad.IsLoaded() && (count%2 ==0))
        {
            ad.Show();
        }
    }

    private void OnDestroy()
    {
        ad.Destroy();
    }
}
