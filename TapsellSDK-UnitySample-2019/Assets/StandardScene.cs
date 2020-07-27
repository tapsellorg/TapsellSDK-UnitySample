using System.Collections;
using System.Collections.Generic;
using TapsellSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandardScene : MonoBehaviour {

  private readonly string ZONE_ID = "5a28f47539086d0001670416";
  public static TapsellAd ad;

  public void RequestBannerAd () {
    Tapsell.RequestBannerAd (ZONE_ID, BannerType.BANNER_250x250, Gravity.BOTTOM, Gravity.CENTER,
      (string zoneId) => {
        Debug.Log ("on Ad Available");
      },
      (string zoneId) => {
        Debug.Log ("no Ad Available");
      },
      (TapsellError error) => {
        Debug.Log (error.message);
      },
      (string zoneId) => {
        Debug.Log ("no Network");
      },
      (string zoneId) => {
        Debug.Log ("Hide Banner");
      });
  }

  public void Hide () {
    Tapsell.HideBannerAd (ZONE_ID);
  }

  public void Show () {
    Tapsell.ShowBannerAd (ZONE_ID);
  }

}