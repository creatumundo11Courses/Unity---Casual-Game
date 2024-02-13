using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    private string _appKey = "1d3e2f05d";
    [SerializeField]
    private bool _showBannerStart;

    private bool _isInterstitialTimerPassed = false;
    private float _interstitialTimer;
    private float _minDelayBetweenInterstitial = 30.0f;

    private bool _isAdsEnabled;
    public const string KEY_PP_ADS = "ADS";

    public bool IsAdsEnabled     
    { 
       get => _isAdsEnabled;

       private set 
       { 
         _isAdsEnabled = value;
         if (!_isAdsEnabled)
         {
            DestroyBanner();
         }
         OnAdsEnabledChanged?.Invoke(_isAdsEnabled);
       }
    }
    public event Action<bool> OnAdsEnabledChanged;
    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        //Add AdInfo Rewarded Video Events
        InitializeRewardedVideo();
        InitializeInterstitialVideo();
        InitializeBannerVideo();
    }

   
    private void OnDisable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;
        //Add AdInfo Rewarded Video Events
        TerminateRewardedVideo();
        TerminateInterstitialVideo();
        TerminateBannerVideo();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        IsAdsEnabled = PlayerPrefs.GetInt(KEY_PP_ADS, 1) == 1; // 1 active : 0 inactive
        if (!IsAdsEnabled) return;

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(_appKey);
        if (_showBannerStart)
            LoadBanner();

        _interstitialTimer = _minDelayBetweenInterstitial;
    }
    private void Update()
    {
        if (!_isInterstitialTimerPassed && Time.time > _interstitialTimer)
        {
            _isInterstitialTimerPassed = true;
        }
    }
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void SdkInitializationCompletedEvent() { }

    #region Reward

    public void ShowRewardVideo()
    {
        if (!IsAdsEnabled) return;

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
        }
    }
    private void InitializeRewardedVideo()
    {
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    private void TerminateRewardedVideo()
    {
        IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;

    }
    /************* RewardedVideo AdInfo Delegates *************/
    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }
    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        GameMode.Instance.Grant30Characters();
    }
    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
    }



    #endregion

    #region Interstitial

    private void ShowInterstitial()
    {
        IronSource.Agent.showInterstitial();
    }
    public void ShowInterstitialVideo()
    {
        if (!IsAdsEnabled) return;

        if (_isInterstitialTimerPassed)
        {
            IronSource.Agent.loadInterstitial();
            _isInterstitialTimerPassed = false;
            _interstitialTimer = Time.time + _minDelayBetweenInterstitial;
        }

    }
    private void InitializeInterstitialVideo()
    {
        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
    }

    private void TerminateInterstitialVideo()
    {
        IronSourceInterstitialEvents.onAdReadyEvent -= InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent -= InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent -= InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent -= InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent -= InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent -= InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent -= InterstitialOnAdClosedEvent;
    }

    /************* Interstitial AdInfo Delegates *************/
    // Invoked when the interstitial ad was loaded succesfully.
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
        ShowInterstitial();
    }
    // Invoked when the initialization process has failed.
    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    {
    }
    // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the ad failed to show.
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the interstitial ad closed and the user went back to the application screen.
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
    // This callback is not supported by all networks, and we recommend using it only if  
    // it's supported by all networks you included in your build. 
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    {
    }

    #endregion

    #region Banner

    public void LoadBanner()
    {
        if (!IsAdsEnabled) return;

        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER,IronSourceBannerPosition.BOTTOM);
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    private void InitializeBannerVideo()
    {
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
    }

    private void TerminateBannerVideo()
    {
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent -= BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent -= BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent -= BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent -= BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent -= BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent -= BannerOnAdLeftApplicationEvent;
    }

    /************* Banner AdInfo Delegates *************/
    //Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
    }
    // Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
    }

    public void DisableAds()
    {
        IsAdsEnabled = false;
        PlayerPrefs.SetInt(KEY_PP_ADS,0);
        PlayerPrefs.Save();
    }

    public void EnableAds()
    {
        IsAdsEnabled = true;
        PlayerPrefs.SetInt(KEY_PP_ADS, 1);
        PlayerPrefs.Save();
    }


    #endregion
}
