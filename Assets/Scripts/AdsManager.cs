using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour , IUnityAdsListener
{
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4009693", true);
    }

    public void ShowAd(string p)
    {
        Advertisement.Show(p);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished && placementId == "Rewarded_Android")
        {
            this.GetComponent<MainMenuScript>().adsWatched++;

            // get the reward
        }
        else if (showResult == ShowResult.Finished)
        {
            // rip
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }
}
