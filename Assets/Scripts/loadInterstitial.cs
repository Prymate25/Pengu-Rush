using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class loadInterstitial : MonoBehaviour,IUnityAdsLoadListener,IUnityAdsShowListener
{   public string androidAdUnitId;
    public string iosAdUnitId;
    string adUnitId;

    void Awake(){
#if UNITY_IOS
        adUnitId=iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId=androidAdUnitId;
#endif
    }
    public void LoadAd(){
        print("Loading Interstitial!");
        Advertisement.Load(adUnitId,this);
    }
    
    public void OnUnityAdsAdLoaded(string placementId){
        print("Interstitial Loaded!");
        ShowAd();
    }
    public void OnUnityAdsFailedToLoad(string placementId,UnityAdsLoadError error, string message){
        print("Interstitial failed!");
    }

    public void ShowAd(){
        print("showing Ad!");
        Advertisement.Show(adUnitId,this);
    }
    public void OnUnityAdsShowClick(string placementId){
        print("Interstitial Clicked!");
    }
    public void OnUnityAdsShowComplete(string placementId,UnityAdsShowCompletionState showCompletionState){
        print("Interstitial show complete!");
    }
    public void OnUnityAdsShowFailure(string placementId,UnityAdsShowError error, string message){
        print("Interstitial show failure!");
    }
    public void OnUnityAdsShowStart(string placementId){
        print("Interstitial show start!");
    }
}
