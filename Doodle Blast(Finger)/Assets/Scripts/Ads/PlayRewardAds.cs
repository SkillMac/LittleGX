using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PlayRewardAds : MonoBehaviour
{
    public static PlayRewardAds Instance;
    private string adUnitId;
    private RewardBasedVideoAd rewardBaseVideo;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-6250098546319109/2445872933"; //"ca-app-pub-6250098546319109/4074934784";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-6250098546319109/8987585775";
        #else
            adUnitId = "unexpected_platform";
        #endif
    }
    // Use this for initialization
    void Start()
    {
        rewardBaseVideo = RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBaseVideo.LoadAd(request, adUnitId);
        
        rewardBaseVideo.OnAdFailedToLoad += RewardBaseVideo_OnAdFailedToLoad;
        rewardBaseVideo.OnAdRewarded += RewardBaseVideo_OnAdRewarded;
        rewardBaseVideo.OnAdClosed += RewardBaseVideo_OnAdClosed;
    }

    private void RewardBaseVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardBaseVideo.LoadAd(request, adUnitId);
    }

    private void RewardBaseVideo_OnAdClosed(object sender, System.EventArgs e)
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardBaseVideo.LoadAd(request, adUnitId);
    }

    private void RewardBaseVideo_OnAdRewarded(object sender, Reward e)
    {
        //string type = e.Type;
        double amont = e.Amount;
        //m_Diamond.text = (amont + Random.Range(0, 10)).ToString();
        //double temp = PlayerPrefs.GetInt("jb_2") + amont;
        //PlayerPrefs.SetInt("jb_2", (int)temp);
    }

    public void PlayRewardAd()
    {
        if (rewardBaseVideo.IsLoaded())
        {
            rewardBaseVideo.Show();
        }
    }
}
