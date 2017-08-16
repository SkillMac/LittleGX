using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MyGameManager : MonoBehaviour
{
    private InterstitialAd ad;
    public string path = "";
    public static MyGameManager Instance;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        Load();
    }
    private void Load()
    {
        ad = new InterstitialAd(path);
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }
    public void OnClickPause()
    {
        //播放广告的接口
        ShowInterAD();
    }
    private void ShowInterAD()
    {
        if (ad.IsLoaded())
        {
            ad.Show();
        }
    }

    private void OnDestroy()
    {
        ad.Destroy();
    }
}
