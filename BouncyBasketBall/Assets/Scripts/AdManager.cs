using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {


    // Use this for initialization
    InterstitialAd interstitial;
    // Update is called once per frame
    SinglePlayerController singlePlayer;
    TournamentController tournamentController;
    private void Start()
    {
        string adID = "ca-app-pub-1841696530009518/7951898017";
#if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        RequestInterstitialAds();
        if (FindObjectOfType<SinglePlayerController>() != null)
        {
            singlePlayer = FindObjectOfType<SinglePlayerController>();
        }

        if (FindObjectOfType<TournamentController>() !=null )
        {
            tournamentController = FindObjectOfType<TournamentController>();
        }
    }

    void Update () {
        if (singlePlayer != null)
        {

            if (singlePlayer.matchEnded)
            {
                showInterstitialAd();
            }
        }
        if (tournamentController != null)
        {

            if (tournamentController.matchEnded)
            {
                showInterstitialAd();
            }
        }
    }

    public void showInterstitialAd()
    {
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();

            //Stop Sound
            //

            Debug.Log("SHOW AD XXX");
        }

    }
 
    private void RequestInterstitialAds()
    {

        AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        Debug.Log("AD LOADED XXX");

    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        //Resume Play Sound

    }
}
