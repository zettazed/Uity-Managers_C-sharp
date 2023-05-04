using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexAdManager : MonoBehaviour
{

    public static YandexAdManager Instance;

    #region Ad Blocks
    private RewardedAd rewardedAd;
    private Interstitial interstitialAd;
    private YandexMobileAds.Banner bannerAd;
    #endregion

    #region
    [SerializeField] private string rewardedAdId;
    [SerializeField] private string interstitialAdId;
    [SerializeField] private string bannerAdId;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        RequestRewardedAd();
        RequestInterstitial();
        RequestBanner();
    }

    #region BannerAd
    private void RequestBanner()
    {
        bannerAd = new YandexMobileAds.Banner(bannerAdId, AdSize.BANNER_320x50, AdPosition.BottomCenter);
        //��������� id �����, ������ ������� � ��� �������.
    }

    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build(); //������ ������ �� ����� �������
        bannerAd.LoadAd(request); //���������� ������ �� ����� �������
        RequestBanner();
    }
    #endregion


    #region RewardedAd
    private void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdId); //��������� ���������� � ��������
        AdRequest request = new AdRequest.Builder().Build(); //������ ������ �� �������
        rewardedAd.LoadAd(request); // �������� ������ �� �������
        //�������� ����������� ������� �������
        rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        rewardedAd.OnImpression += this.HandleImpression;
        rewardedAd.OnRewarded += this.HandleRewarded;
    }
    //����� ��� ������ �������
    public void ShowRewardedAd()
    {
        //���� ������� ��������� � ���������� �
        if (this.rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        RequestRewardedAd();
    }
    //������ ����� ����� ������ ��� ������������ �������� ��������� � �������� (� ��������� � ��)
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //ShowRewardedAd(); //������� ��������� � ���������� �
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }
    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdShown event received");
    }
    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdDismissed event received");
    }
    public void HandleRewarded(object sender, Reward args)
    {
        Debug.Log("HandleRewarded event received: amout = " +args.amount + ", type = " +args.type);
    }
    #endregion



    #region InterstitialAd
    private void RequestInterstitial()
    {
        interstitialAd = new Interstitial(interstitialAdId); //��������� ���������� � ��������
        AdRequest request = new AdRequest.Builder().Build(); //������ ������ �� ����� �������
        interstitialAd.LoadAd(request); //���������� ������
                                      //������ ����� ����� ������ ��� ������������ �������� ��������� � �������� (� ��������� � ��)
        interstitialAd.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        interstitialAd.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        interstitialAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        interstitialAd.OnLeftApplication += this.HandleLeftApplication;
        interstitialAd.OnInterstitialShown += this.HandleInterstitialShown;
        interstitialAd.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        interstitialAd.OnImpression += this.HandleImpression;
    }
    //����� ��� ������ �������
    public void ShowInterstitial()
    {
        if (this.interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        RequestInterstitial();
    }
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        //ShowInterstitial(); //��� �������� ������� � ���������� �
    }
    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }
    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialShown event received");
    }
    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialDismissed event received");
    }
    #endregion



    #region Others Callbacks
    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleReturnedToApplication event received");
    }
    public void HandleLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleLeftApplication event received");
    }
    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        Debug.Log("HandleImpression event received with data: " + impressionData);
    }
    #endregion
}