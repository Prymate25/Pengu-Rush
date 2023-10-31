using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class loadBanner : MonoBehaviour
{
    public string androidAdUnitId;
    public string iosAdUnitId;

    string adUnitId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Start()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(bannerPosition);
    }

    public void LoadBanner() 
    {
        BannerLoadOptions options = new BannerLoadOptions { 
        loadCallback=OnBannerLoaded,
        errorCallback=OnBannerLoadError
        };
        Advertisement.Banner.Load(adUnitId, options);

    }
    void OnBannerLoaded() {
        print("Banner Loaded!!");
        showBannerAd();
    }
    void OnBannerLoadError(string error) {
        print("Banner failed to load "+error);
    }


    public void showBannerAd() 
    {
        BannerOptions options = new BannerOptions
        {
            showCallback=OnBannerShown,
            clickCallback=OnBannerClicked,
            hideCallback=OnBannerHidden
            
        };

        Advertisement.Banner.Show(adUnitId, options);
    }

    void OnBannerShown() { }
    void OnBannerClicked() { }
    void OnBannerHidden() { }

    public void HideBannerAd()//used to hide banner ads
    {
        Advertisement.Banner.Hide();
    }
}