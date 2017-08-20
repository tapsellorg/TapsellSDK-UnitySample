using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using TapsellSDK;
using ArabicSupport;

public class Test : MonoBehaviour {
	
	public static bool available = false;
	public static string ad = null;
	public static TapsellNativeBannerResult nativeAd = null;

	void Start() {
		// Use your tapsell key for initialization
		Tapsell.initialize ("mpkdstpefkoalikkgfslakdspdhikdiddkkgbfpstnaqmkqmgtasdmgtcmitlenscamnik");
		Debug.Log("Tapsell Version: "+Tapsell.getVersion());
		Tapsell.setDebugMode (true);
		Tapsell.setPermissionHandlerConfig (Tapsell.PERMISSION_HANDLER_AUTO);
		Tapsell.setRewardListener (
			(TapsellAdFinishedResult result) => 
			{
				// onFinished, you may give rewards to user if result.completed and result.rewarded are both True
				Debug.Log ("onFinished, adId:"+result.adId+", zoneId:"+result.zoneId+", completed:"+result.completed+", rewarded:"+result.rewarded);
			}
		);
	}


	private void requestAd(string zone,bool cached)
	{
		Tapsell.requestAd(zone,cached,
			(TapsellResult result) => {
				// onAdAvailable
				Debug.Log("Action: onAdAvailable");
				Test.available = true;
				Test.ad = result.adId;
			},

			(string zoneId) => {
				// onNoAdAvailable
				Debug.Log("No Ad Available");
			},

			(TapsellError error) => {
				// onError
				Debug.Log(error.error);
			},

			(string zoneId) => {
				// onNoNetwork
				Debug.Log("No Network: "+zoneId);
			},

			(TapsellResult result) => {
				//onExpiring
				Debug.Log("Expiring");
				Test.available=false;
				Test.ad=null;
				requestAd(result.zoneId,false);
			}

		);
	}


	void OnGUI()
	{
		if(Test.available)
		{
			if(GUI.Button(new Rect(150, 50, 100, 100), "Show Ad")){
				Test.available = false;
				TapsellShowOptions options = new TapsellShowOptions ();
				options.backDisabled = false;
				options.immersiveMode = false;
				options.rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE;
				options.showDialog = true;
				Tapsell.showAd(ad,options);
			}
		}
		if(GUI.Button(new Rect(50, 50, 100, 100), "Request Video Ad")){
			requestAd ("5873510bbc5c28f9d90ce98d",false);
		}

		#if UNITY_ANDROID && !UNITY_EDITOR

		if(Test.nativeAd==null)
		{
			if(GUI.Button(new Rect(50, 150, 100, 100), "Request Banner Ad")){
				requestNativeBannerAd ("598eadad468465085986d07e");
			}
		}
		if(Test.nativeAd!=null)
		{
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.alignment = TextAnchor.UpperRight;
			GUI.Label (new Rect (50, 250, 450, 30), ArabicFixer.Fix(Test.nativeAd.getTitle (),true), titleStyle);
			
			GUIStyle descriptionStyle = new GUIStyle ();
			descriptionStyle.richText = true;
			descriptionStyle.alignment = TextAnchor.MiddleRight;
			GUI.Label (new Rect (50, 280, 450, 20), ArabicFixer.Fix(Test.nativeAd.getDescription (),true), descriptionStyle);
			GUI.DrawTexture (new Rect(500, 250, 50, 50), Test.nativeAd.getIcon() );
			Rect callToActionRect;
			if(Test.nativeAd.getLandscapeBannerImage()!=null)
			{
				GUI.DrawTexture (new Rect(50, 300, 500, 280), Test.nativeAd.getLandscapeBannerImage() );
				callToActionRect = new Rect(50, 580, 500, 50);
			}
			else if(Test.nativeAd.getPortraitBannerImage()!=null)
			{
				GUI.DrawTexture (new Rect(50, 300, 500, 280), Test.nativeAd.getPortraitBannerImage() );
				callToActionRect = new Rect(50, 580, 500, 50);
			}
			else
			{
				callToActionRect = new Rect(50, 300, 500, 50);
			}
			if(GUI.Button (callToActionRect, ArabicFixer.Fix(Test.nativeAd.getCallToAction (),true) ))
			{
				Tapsell.onNativeBannerAdClicked (Test.nativeAd.adId);
			}
		}
		#endif

	}

	private void requestNativeBannerAd(string zone)
	{
		Tapsell.requestNativeBannerAd(this, zone, 
			(TapsellNativeBannerResult result) => {
				// onAdAvailable
				Debug.Log("Action: onNativeRequestFilled");

				Test.nativeAd = result;

			},

			(string zoneId) => {
				// onNoAdAvailable
				Debug.Log("No Ad Available");
			},

			(TapsellError error) => {
				// onError
				Debug.Log(error.error);
			},

			(string zoneId) => {
				// onNoNetwork
				Debug.Log("No Network: "+zoneId);
			}
		);
	}

}

