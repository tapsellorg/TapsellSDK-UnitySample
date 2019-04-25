using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;

public class StandardBannerScene : MonoBehaviour
{
    private string zoneId = "5a28f47539086d0001670416";

    public void Request()
    {
        Tapsell.requestBannerAd(this.zoneId, BannerType.BANNER_320x50, Gravity.BOTTOM, Gravity.CENTER,
        (string zoneId) =>
        {
            Debug.Log("onBannerRequestFilled: " + this.zoneId);
        },
        (string zoneId) =>
        {
            Debug.Log("Action: onNoBannerAdAvailableAction");
        },
        (TapsellError tapsellError) =>
        {
            Debug.Log("Action: onBannerAdErrorAction");
        },
        (string zoneId) =>
        {
            Debug.Log("Action: onNoNetworkAction");
        },
        (string zoneId) =>
        {
            Debug.Log("Action: onHideBannerAction");
        });
    }

    public void Show()
    {
        Tapsell.showBannerAd(this.zoneId);
    }

    public void Hide()
    {
        Tapsell.hideBannerAd(this.zoneId);
    }
}
