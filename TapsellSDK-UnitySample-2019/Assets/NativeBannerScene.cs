using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArabicSupport;
using TapsellSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {

	private readonly string ZONE_ID = "5953bc774684652dd8fc937e";

	public static TapsellNativeBannerAd nativeAd = null;

	public void Request () {
		Tapsell.RequestNativeBannerAd (this, ZONE_ID,
			(TapsellNativeBannerAd result) => {
				// onAdAvailable
				Debug.Log ("on Ad Available");
				nativeAd = result;
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
			}
		);
	}

	void OnGUI () {
#if UNITY_ANDROID && !UNITY_EDITOR
		if (nativeAd != null) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			titleStyle.fontSize = 32;
			titleStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 500, 600, 50), ArabicFixer.Fix (nativeAd.GetTitle (), true), titleStyle);

			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			descriptionStyle.fontSize = 32;
			descriptionStyle.normal.textColor = Color.white;
			GUI.Label (new Rect (50, 550, 600, 50), ArabicFixer.Fix (nativeAd.GetDescription (), true), descriptionStyle);

			GUI.DrawTexture (new Rect (660, 500, 100, 100), nativeAd.GetIcon ());

			Rect callToActionRect;
			if (nativeAd.GetLandscapeBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 610, 710, 400), nativeAd.GetLandscapeBannerImage ());
				callToActionRect = new Rect (50, 1020, 710, 100);
			} else if (nativeAd.GetPortraitBannerImage () != null) {
				GUI.DrawTexture (new Rect (50, 300, 500, 280), nativeAd.GetPortraitBannerImage ());
				callToActionRect = new Rect (50, 580, 500, 50);
			} else {
				callToActionRect = new Rect (50, 300, 500, 50);
			}

			GUIStyle buttonStyle = new GUIStyle ("button");
			buttonStyle.fontSize = 32;
			if (GUI.Button (callToActionRect, ArabicFixer.Fix (nativeAd.GetCallToAction (), true), buttonStyle)) {
				nativeAd.Clicked ();
			}
		}
#endif

	}
}