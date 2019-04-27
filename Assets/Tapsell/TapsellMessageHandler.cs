using System;
using UnityEngine;
using TapsellSDK;

public class TapsellMessageHandler : MonoBehaviour
{

    // rewarded video, interstitial video/web

    public void NotifyError(String body)
    {
        // debugLog("NotifyError:" + body);
        TapsellError error = new TapsellError();
        error = JsonUtility.FromJson<TapsellError>(body);
        Debug.Log("notifyError: " + error.zoneId);
        Tapsell.onError(error);
    }

    public void NotifyAdAvailable(String body)
    {
        TapsellAd ad = new TapsellAd();
        ad = JsonUtility.FromJson<TapsellAd>(body);
        Debug.Log("notifyAdAvailable: " + ad.zoneId);
        Tapsell.onAdAvailable(ad);
    }

    public void NotifyNoAdAvailable(String zoneId)
    {
        Debug.Log("notifyNoAdAvailable:" + zoneId);
        Tapsell.onNoAdAvailable(zoneId);
    }

    public void NotifyExpiring(String body)
    {
        TapsellAd ad = new TapsellAd();
        ad = JsonUtility.FromJson<TapsellAd>(body);
        Debug.Log("notifyExpiring: " + ad.zoneId);
        Tapsell.onExpiring(ad);
    }

    public void NotifyNoNetwork(String zoneId)
    {
        Debug.Log("notifyNoNetwork: " + zoneId);
        Tapsell.onNoNetwork(zoneId);
    }

    // display banner

    public void NotifyBannerError(String body)
    {
        TapsellError error = new TapsellError();
        error = JsonUtility.FromJson<TapsellError>(body);
        Debug.Log("notifyBannerError: " + error.zoneId);
        Tapsell.onBannerError(error);
    }

    public void NotifyBannerRequestFilled(String zoneId)
    {
        Debug.Log("notifyBannerRequestFilled: " + zoneId);
        Tapsell.onBannerRequestFilled(zoneId);
    }

    public void NotifyBannerNoAdAvailable(String zoneId)
    {
        Debug.Log("notifyBannerNoAdAvailable: " + zoneId);
        Tapsell.onBannerNoAdAvailable(zoneId);
    }

    public void NotifyBannerNoNetwork(String zoneId)
    {
        Debug.Log("notifyBannerNoNetwork: " + zoneId);
        Tapsell.onBannerNoNetwork(zoneId);
    }

    public void NotifyHideBanner(String zoneId)
    {
        Debug.Log("notifyHideBanner:" + zoneId);
        Tapsell.onHideBanner(zoneId);
    }

    // native banner

    public void NotifyNativeBannerError(String body)
    {
        TapsellError error = new TapsellError();
        error = JsonUtility.FromJson<TapsellError>(body);
        Debug.Log("notifyNativeBannerError: " + error.zoneId);
        Tapsell.onNativeBannerError(error);
    }

    public void NotifyNativeBannerRequestFilled(String body)
    {
        TapsellNativeBannerAd bannerAd = new TapsellNativeBannerAd();
        bannerAd = JsonUtility.FromJson<TapsellNativeBannerAd>(body);
        Debug.Log("notifyNativeBannerRequestFilled: " + bannerAd.zoneId);
        Tapsell.onNativeBannerRequestFilled(bannerAd);
    }

    public void NotifyNativeBannerNoAdAvailable(String zoneId)
    {
        Debug.Log("notifyNativeBannerNoAdAvailable:" + zoneId);
        Tapsell.onNativeBannerNoAdAvailable(zoneId);
    }

    public void NotifyNativeBannerNoNetwork(String zoneId)
    {
        Debug.Log("notifyNativeBannerNoNetwork:" + zoneId);
        Tapsell.onNativeBannerNoNetwork(zoneId);
    }

    // ad open, close and reward

    public void NotifyOpened(String body)
    {
        TapsellAd ad = new TapsellAd();
        ad = JsonUtility.FromJson<TapsellAd>(body);
        Debug.Log("notifyOpened: " + ad.zoneId);
        Tapsell.onOpened(ad);
    }

    public void NotifyClosed(String body)
    {
        TapsellAd ad = new TapsellAd();
        ad = JsonUtility.FromJson<TapsellAd>(body);
        Debug.Log("notifyClosed: " + ad.zoneId);
        Tapsell.onClosed(ad);
    }

    public void NotifyAdShowFinished(String body)
    {
        TapsellAdFinishedResult ad = new TapsellAdFinishedResult();
        ad = JsonUtility.FromJson<TapsellAdFinishedResult>(body);
        Debug.Log("notifyAdShowFinished: " + ad.zoneId);
        Tapsell.onAdShowFinished(ad);
    }

}

