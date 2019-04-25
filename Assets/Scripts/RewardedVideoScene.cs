using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TapsellSDK;
using ArabicSupport;

public class RewardedVideoScene : MonoBehaviour
{

    private string zoneId = "5a5dbd5cc21bf000010d1686";
    private bool cached = true;
    private static TapsellAd ad;

    public void Request()
    {
        Tapsell.requestAd(this.zoneId, this.cached,
            (TapsellAd result) =>
            {
                // onAdAvailable
                Debug.Log("Action: onAdAvailable");
                RewardedVideoScene.ad = result;
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
        if (RewardedVideoScene.ad != null)
        {
            TapsellShowOptions showOptions = new TapsellShowOptions();
            showOptions.backDisabled = false;
            showOptions.immersiveMode = false;
            showOptions.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
            showOptions.showDialog = true;
            Tapsell.showAd(RewardedVideoScene.ad, showOptions);
        }
    }
}
