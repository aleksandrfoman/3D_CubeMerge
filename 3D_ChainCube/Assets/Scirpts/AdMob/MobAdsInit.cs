using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MobAdsInit : MonoBehaviour
{
    private const string appID = "ca-app-pub-3940256099942544~3347511713";
    private void Awake()
    {
        MobileAds.Initialize(InitializationStatus => { });
        //MobileAds.Initialize(appID);
    }
}
