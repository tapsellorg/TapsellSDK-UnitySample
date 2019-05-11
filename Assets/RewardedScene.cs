using System.Collections;
using System.Collections.Generic;
using TapsellSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardedScene : MonoBehaviour {
  private readonly string ZONE_ID = "5a5dbd5cc21bf000010d1686";
  public static TapsellAd ad;

  public void Request () {

    Tapsell.requestAd (ZONE_ID, true,
      (TapsellAd result) => {
        // onAdAvailable
        Debug.Log ("on Ad Available");
        ad = result;
      },

      (string zoneId) => {
        // onNoAdAvailable
        Debug.Log ("no Ad Available");
      },

      (TapsellError error) => {
        // onError
        Debug.Log (error.message);
      },

      (string zoneId) => {
        // onNoNetwork
        Debug.Log ("no Network");
      },

      (TapsellAd result) => {
        //onExpiring
        Debug.Log ("Expiring");
      }

    );
  }

  public void Show () {
        Debug.Log ("ShowShowShowShowShowShow");
    Tapsell.showAd (ad, new TapsellShowOptions ());
  }

}