using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class MobAdsInterstitial : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    string interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";

    private void OnEnable()
    {
        RequestInter();
    }
    private void RequestInter()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    { 
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        RequestInter();
    }
}
