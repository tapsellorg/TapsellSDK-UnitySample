using System;
using UnityEngine;
using SimpleJSON;

public class TapsellMessageHandler : MonoBehaviour{
	
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
		TapsellResult result = new TapsellResult();
		debugLog("adId = " + node ["adId"].Value);
		result.adId = node ["adId"].Value;
		debugLog("adId = " + result.adId);
		result.zoneId = node ["zoneId"].Value;
		debugLog("zondeId = " + result.zoneId);
		Tapsell.onAdAvailable (result);
	}

	public void NotifyNoAdAvailable(String zone){
		debugLog("NotifyNoAdAvailable:"+zone);
		Tapsell.onNoAdAvailable (zone);
	}

	public void NotifyExpiring(String str){
		debugLog("NotifyExpiring:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellResult result = new TapsellResult();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onExpiring (result);
	}

	public void NotifyNoNetwork(String zone){
		debugLog("NotifyNoNetwork:"+zone);
		Tapsell.onNoNetwork (zone);
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

