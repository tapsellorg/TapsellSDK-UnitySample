using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class Test : MonoBehaviour {
	
	public static bool available = false;
	public static string ad = null;

	void Start() {
		// Use your tapsell key for initialization
		Tapsell.initialize ("mpkdstpefkoalikkgfslakdspdhikdiddkkgbfpstnaqmkqmgtasdmgtcmitlenscamnik");
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
				Debug.Log("No Network");
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
			if(GUI.Button(new Rect(50, 50, 100, 100), "Show Ad")){
				Test.available = false;
				TapsellShowOptions options = new TapsellShowOptions ();
				options.backDisabled = false;
				options.immersiveMode = false;
				options.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
				Tapsell.showAd(ad,options,
					(TapsellAdFinishedResult result) => 
					{
						// onFinished, you may give rewards to user if result.completed and result.rewarded are both True
						Debug.Log ("onFinished, adId:"+result.adId+", zoneId:"+result.zoneId+", completed:"+result.completed+", rewarded:"+result.rewarded);
					}

				);
			}
		}
		if(GUI.Button(new Rect(200, 50, 100, 100), "Request Ad")){
			requestAd ("5873510bbc5c28f9d90ce98d",false);
		}

	}
}

