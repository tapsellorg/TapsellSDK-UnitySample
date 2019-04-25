using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;
using ArabicSupport;

public class NativeBannerScene : MonoBehaviour
{
    private string zoneId = "5953bc774684652dd8fc937e";

    private static TapsellNativeBannerAd nativeAd;

    public void Reuest()
    {
        Tapsell.requestNativeBannerAd(this, this.zoneId,
        (TapsellNativeBannerAd result) =>
        {
            // onRequestFilled
            Debug.Log("Request Filled");
            nativeAd = result; // store this to show the ad later
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
        }
    );
    }

    void OnGUI()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
		if (NativeBannerScene.nativeAd != null) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			titleStyle.fontSize = 32;
			titleStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 500, 600, 50), ArabicFixer.Fix (NativeBannerScene.nativeAd.getTitle (), true), titleStyle);

			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			descriptionStyle.fontSize = 32;
			descriptionStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 550, 600, 50), ArabicFixer.Fix (NativeBannerScene.nativeAd.getDescription (), true), descriptionStyle);

			GUI.DrawTexture (new Rect (660, 500, 100, 100), NativeBannerScene.nativeAd.getIcon ());

			Rect callToActionRect;
			if (NativeBannerScene.nativeAd.getLandscapeBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 610, 710, 400), NativeBannerScene.nativeAd.getLandscapeBannerImage ());
				callToActionRect = new Rect (50, 1020, 710, 100);
			} else if (NativeBannerScene.nativeAd.getPortraitBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 300, 500, 280), NativeBannerScene.nativeAd.getPortraitBannerImage ());
				callToActionRect = new Rect (50, 580, 500, 50);
			} else {
				callToActionRect = new Rect (50, 300, 500, 50);
			}

			GUIStyle buttonStyle = new GUIStyle ("button");
			buttonStyle.fontSize = 32;
			if (GUI.Button (callToActionRect, ArabicFixer.Fix (NativeBannerScene.nativeAd.getCallToAction (), true), buttonStyle)) {
				NativeBannerScene.nativeAd.onClicked();
			}
		}
        #endif
    }
}
