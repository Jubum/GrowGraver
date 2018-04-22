using System.Collections;

using System.Collections.Generic;

using UnityEngine;



using GoogleMobileAds.Api;



public class AdMobManager : MonoBehaviour
{



    static bool isAdsBannerSet = false;



    // Use this for initialization

    void Start()
    {



        if (!isAdsBannerSet)

            RequestBanner();

    }





    private void RequestBanner()

    {

#if UNITY_ANDROID

        string AdUnitID = "ca-app-pub-9141553685549733/5724312975";

#else

        string AdUnitID = "unDefind";

#endif

        BannerView banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Bottom);



        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);

        isAdsBannerSet = true;

    }

}