using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour, IAds
{
    #region Variables
    #region Others
    [HideInInspector] public static AdsManager Instance;
    public int AdRewardID = 0;
    private int skinID = 0;
    [SerializeField] private AnyWindow _adLoadingMenu;
    #endregion

    #region Ad Keys
    [Header("AdMob Keys")]
    [SerializeField] internal string _bannerAdKey = "";
    [SerializeField] internal string _interstitialAdKey = "";
    [SerializeField] internal string _rewardedAdKey = "";
    #endregion

    #region Ad
    private BannerView bannerView;
    private InterstitialAd _interstitialAd;
    public RewardedAd _rewardedAd;
    #endregion
    #endregion

    #region Unity Behaviour
    private void Awake() => Instance = this;

    private void Start() => MobileAds.Initialize(initStatus => { });
    #endregion

    #region Initialize Ad
    private void OnEnable() => InitAd();

    private void InitAd()
    {
        InitInterstitialAd();
        InitRewardedAd();
        InitAppOpenAd();
    }

    private void RequestBanner()
    {
        this.bannerView = new BannerView(_bannerAdKey, AdSize.Banner, AdPosition.Top);
        
        AdRequest request = new AdRequest.Builder().Build();
        
        this.bannerView.LoadAd(request);
        bannerView.Show();
    }

    public void InitInterstitialAd()
    {
        _interstitialAd = new InterstitialAd(_interstitialAdKey);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
        _interstitialAd.OnAdClosed += _interstitialAd_OnAdClosed;
    }

    public void InitRewardedAd()
    {
        _rewardedAd = new RewardedAd(_rewardedAdKey);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnAdClosed += _rewardedAd_OnAdClosed;
        _rewardedAd.OnUserEarnedReward += _rewardedAd_OnUserEarnedReward;
    }

    public void InitAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
            _appOpenAd = null;
        }
    
        Debug.Log("Loading the app open ad.");
    
        // Create our request used to load the ad.
        AdRequest adRequest = new AdRequest.Builder().Build();
    
        // send the request to load the ad.
        AppOpenAd.LoadAd(_appOpenAdKey, ScreenOrientation.Portrait, adRequest, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }
    
            // App open ad is loaded.
            _appOpenAd = appOpenAd;
        }));
    }

    public virtual void _interstitialAd_OnAdClosed(object sender, System.EventArgs e)
        => InitInterstitialAd();

    public virtual void _rewardedAd_OnAdClosed(object sender, System.EventArgs e)
        => InitRewardedAd();

    public virtual void _rewardedAd_OnUserEarnedReward(object sender, System.EventArgs e)
    {
        switch (AdRewardID)
        {
            case 1:
                GameManager.instance.Wallet += 100;
                break;
            case 2:
                Shop.Instance.SeeAdEarned(skinID);
                break;
            case 3:
                GameManager.instance.RevivePlayer();
                break;
        }
        InitRewardedAd();
    }
    #endregion

    #region Show Ad
    public void ShowInterstitialAd()
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
        else
            InitInterstitialAd();
    }

    public void ShowRewardedAd(int skinID)
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
        else
        {
            InitRewardedAd();
            OpenLoadingAdMenu();
        }
        this.skinID = skinID;
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
        else
        {
            InitRewardedAd();
            OpenLoadingAdMenu();
        }
    }

    public void ShowAppOpenAd()
    {
        if (_appOpenAd != null)
        {
            Debug.Log("Showing app open ad.");
            _appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
    }

    private void OpenLoadingAdMenu()
    {
        _adLoadingMenu.OpenMenu();
    }
    #endregion
}
