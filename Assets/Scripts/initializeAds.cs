using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class initializeAds : MonoBehaviour,IUnityAdsInitializationListener
{   public string androidGameId;
    public string iosGameId;

    public bool isTestingMode=true;
    string gameId;
    // Start is called before the first frame update
    void Awake()
    {
        InitializeAds();
    }

    void InitializeAds(){
#if UNITY_IOS
        gameId=iosGameId;
#elif UNITY_ANDROID
        gameId=androidGameId;
#elif UNITY_EDITOR
        gameId=androidGameId;//for testing
#endif
        if(!Advertisement.isInitialized && Advertisement.isSupported){
            Advertisement.Initialize(gameId,isTestingMode,this);

        }
    }
    public void OnInitializationComplete(){
       print("Ads Initialized!");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error,string message){
        print("Ads Failed!");
    }
}
