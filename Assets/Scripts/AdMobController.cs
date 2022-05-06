using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobController : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    int gameBreak = 0;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        this.RequestInterstitial();
        this.RequestRewardedAd();
    }


    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3092179230402832/3719103561";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3092179230402832/2406021890";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void RequestRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3092179230402832/1842274481";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void BringRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void GameBreak()
    {

        
        gameBreak++;
        if(gameBreak == 10)
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            //Reklam
            gameBreak = 0;
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.RequestRewardedAd();
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Camera.main.SendMessage("RestartWithReward", SendMessageOptions.DontRequireReceiver);
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }
}
