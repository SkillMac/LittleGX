using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdModSet : MonoBehaviour
{
    private BannerView banner;
    private string adUnitId;
    // Use this for initialization
    void Awake()
    {
        #if UNITY_ANDROID
             adUnitId = "ca-app-pub-6250098546319109/6286165159";
        #elif UNITY_IPHONE
             adUnitId = "ca-app-pub-6250098546319109/6361422432";
        #else
             adUnitId = "unexpected_platform";
        #endif

    }
    void Start()
    {
        banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);

        banner.OnAdClosed += Banner_OnAdClosed;
    }
    private void Banner_OnAdClosed(object sender, System.EventArgs e)
    {
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        banner.Destroy();
    }
}
