using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MobAdsBanner : MonoBehaviour
{
    private BannerView bannerView;
    [SerializeField]
    private AdPosition adPos;
    private const string bannerUnitId = "ca-app-pub-3940256099942544/6300978111";

    private void OnEnable()
    {
        if (bannerView == null)
        {
            bannerView = new BannerView(bannerUnitId, AdSize.Banner, adPos);
            AdRequest adRequest = new AdRequest.Builder().Build();
            bannerView.LoadAd(adRequest);
            ShowBanner();
        }
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
}