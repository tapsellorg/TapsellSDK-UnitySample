using System;
using UnityEngine;
using TapsellSimpleJSON;
using TapsellSDK;

public class TapsellMessageHandler : MonoBehaviour{

	// rewarded video, interstitial video/web

	public void NotifyError(String str){
		debugLog("NotifyError:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellError result = new TapsellError ();
		result.error = node ["error"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onError (result);
	}
	
	public void NotifyAdAvailable(String str){
		debugLog("NotifyAdAvailable:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellAd result = new TapsellAd();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onAdAvailable (result);
	}

	public void NotifyNoAdAvailable(String str){
		debugLog("NotifyNoAdAvailable:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onNoAdAvailable (zone);
	}

	public void NotifyExpiring(String str){
		debugLog("NotifyExpiring:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellAd result = new TapsellAd();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onExpiring (result);
	}

	public void NotifyNoNetwork(String str){
		debugLog("NotifyNoNetwork:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onNoNetwork (zone);
	}


	// banner

	public void NotifyBannerError(String str){
		debugLog("NotifyBannerError:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellError result = new TapsellError ();
		result.error = node ["error"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onBannerError (result);
	}

	public void NotifyBannerRequestFilled(String str){
		debugLog("NotifyBannerRequestFilled:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onBannerRequestFilled (zone);
	}

	public void NotifyBannerNoAdAvailable(String str){
		debugLog("NotifyBannerNoAdAvailable:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onBannerNoAdAvailable (zone);
	}

	public void NotifyBannerNoNetwork(String str){
		debugLog("NotifyBannerNoNetwork:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onBannerNoNetwork (zone);
	}

	public void NotifyHideBanner(String str){
		debugLog("NotifyHideBanner:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onHideBanner (zone);
	}




	// native

	public void NotifyNativeBannerError(String str){
		debugLog("NotifyNativeBannerError:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellError result = new TapsellError ();
		result.error = node ["error"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onNativeBannerError (result);
	}

	public void NotifyNativeBannerRequestFilled(String str){
		debugLog("NotifyNativeBannerRequestFilled:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellNativeBannerAd result = new TapsellNativeBannerAd();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		result.title = node ["title"].Value;
		result.description = node ["description"].Value;
		result.iconUrl = node ["iconUrl"].Value;
		result.callToActionText = node ["callToActionText"].Value;
		result.portraitStaticImageUrl = node ["portraitStaticImageUrl"].Value;
		result.landscapeStaticImageUrl = node ["landscapeStaticImageUrl"].Value;
		Tapsell.onNativeBannerRequestFilled (result);
	}

	public void NotifyNativeBannerNoAdAvailable(String str){
		debugLog("NotifyNativeBannerNoAdAvailable:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onNativeBannerNoAdAvailable (zone);
	}

	public void NotifyNativeBannerNoNetwork(String str){
		debugLog("NotifyNativeBannerNoNetwork:"+str);
		JSONNode node = JSON.Parse (str);
		String zone = node ["zoneId"].Value;
		Tapsell.onNativeBannerNoNetwork (zone);
	}


	// ad open, close and reward

	public void NotifyOpened(String str){
		debugLog("NotifyOpened:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellAd result = new TapsellAd();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onOpened (result);
	}

	public void NotifyClosed(String str){
		debugLog("NotifyClosed:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellAd result = new TapsellAd();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onClosed (result);
	}

	public void NotifyAdShowFinished(String str)
	{
		debugLog("NotifyAdShowFinished:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellAdFinishedResult result = new TapsellAdFinishedResult ();
		result.adId=node["adId"].Value;
		result.zoneId=node["zoneId"].Value;
		result.completed=node["completed"].AsBool;
		result.rewarded=node["rewarded"].AsBool;
		Tapsell.onAdShowFinished (result);
	}

	public void debugLog(String str)
	{
		Debug.Log (str);
	}

}

