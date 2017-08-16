using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdModSet : MonoBehaviour
{
    public string adUnitid = "";

    private BannerView banner;

    // Use this for initialization
    void Start()
    {
        banner = new BannerView(adUnitid, AdSize.SmartBanner, AdPosition.Bottom);
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
