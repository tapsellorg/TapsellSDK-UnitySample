using System;
using TapsellSDK;
using UnityEngine;

public class TapsellMessageHandler : MonoBehaviour {

	public void notifyAdAvailable (String body) {
		TapsellAd result = new TapsellAd ();
		result = JsonUtility.FromJson<TapsellAd> (body);
		Debug.Log ("notifyAdAvailable:" + result.zoneId + ":" + result.adId);
		Tapsell.onAdAvailable (result);
	}

	public void notifyBannerFilled (String zoneId) {
		Debug.Log ("notifyBannerFilled:" + zoneId);
		Tapsell.onBannerRequestFilled (zoneId);
	}

	public void notifyNativeBannerFilled (String body) {
		TapsellNativeBannerAd result = new TapsellNativeBannerAd ();
		result = JsonUtility.FromJson<TapsellNativeBannerAd> (body);
		Debug.Log ("notifyNativeBannerFilled:" + result.zoneId + ":" + result.adId);
		Tapsell.onNativeBannerFilled (result);
	}

	public void notifyError (String body) {
		TapsellError error = new TapsellError ();
		error = JsonUtility.FromJson<TapsellError> (body);
		Debug.Log ("notifyError:" + error.zoneId + ":" + error.message);
		Tapsell.onError (error);
	}

	public void notifyNoAdAvailable (String zoneId) {
		Debug.Log ("notifyNoAdAvailable:" + zoneId);
		Tapsell.onNoAdAvailable (zoneId);
	}

	public void notifyExpiring (String body) {
		TapsellAd result = new TapsellAd ();
		result = JsonUtility.FromJson<TapsellAd> (body);
		Debug.Log ("notifyExpiring:" + result.zoneId + ":" + result.adId);
		Tapsell.onExpiring (result);
	}

	public void notifyNoNetwork (String zoneId) {
		Debug.Log ("notifyNoNetwork:" + zoneId);
		Tapsell.onNoNetwork (zoneId);
	}

	public void notifyHideBanner (String zoneId) {
		Debug.Log ("notifyHideBanner:" + zoneId);
		Tapsell.onHideBanner (zoneId);
	}

	public void notifyOpened (String body) {
		TapsellAd result = new TapsellAd ();
		result = JsonUtility.FromJson<TapsellAd> (body);
		Debug.Log ("notifyOpened:" + result.zoneId + ":" + result.adId);
		Tapsell.onOpened (result);
	}

	public void notifyClosed (String body) {
		TapsellAd result = new TapsellAd ();
		result = JsonUtility.FromJson<TapsellAd> (body);
		Debug.Log ("notifyClosed:" + result.zoneId + ":" + result.adId);
		Tapsell.onClosed (result);
	}

	public void notifyShowFinished (String body) {
		TapsellAdFinishedResult result = new TapsellAdFinishedResult ();
		result = JsonUtility.FromJson<TapsellAdFinishedResult> (body);
		Debug.Log ("notifyShowFinished:" + result.zoneId + ":" + result.adId + ":" + result.rewarded);
		Tapsell.onAdShowFinished (result);
	}

}