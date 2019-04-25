using System.Collections;
using System.Collections.Generic;
using TapsellSDK;
using UnityEngine;

public class InterstitialBannerScene : MonoBehaviour
{

    private string zoneId = "5a3f5063fca4f000014b70a8";
    private bool cached = false;
    private static TapsellAd ad;

    public void Request()
    {
        Tapsell.requestAd(this.zoneId, this.cached,
            (TapsellAd result) =>
            {
                // onAdAvailable
                Debug.Log("Action: onAdAvailable");
                InterstitialBannerScene.ad = result;
            },

            (string zoneId) =>
            {
                // onNoAdAvailable
                Debug.Log("No Ad Available");
            },

            (TapsellError error) =>
            {
                // onError
                Debug.Log(error.message);
            },

            (string zoneId) =>
            {
                // onNoNetwork
                Debug.Log("No Network");
            },

            (TapsellAd result) =>
            {
                // onExpiring
                Debug.Log("Expiring");
                // this ad is expired, you must download a new ad for this zone
            }
        );
    }

    public void Show()
    {
        TapsellShowOptions showOptions = new TapsellShowOptions();
        showOptions.backDisabled = false;
        showOptions.immersiveMode = false;
        showOptions.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
        showOptions.showDialog = true;
        Tapsell.showAd(InterstitialBannerScene.ad, showOptions);
    }
}
