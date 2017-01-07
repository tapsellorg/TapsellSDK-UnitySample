using System;
using UnityEngine;
using SimpleJSON;

public class TapsellMessageHandler : MonoBehaviour{
	
	public void NotifyError(String str){
		//GuiTextDebugdebug ("NotifyError:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellError result = new TapsellError ();
		result.error = node ["error"].Value;
		result.zoneId = node ["zoneId"].Value;
		Tapsell.onError (result);
	}
	
	public void NotifyAdAvailable(String str){
		//GuiTextDebugdebug ("NotifyAdAvailable:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellResult result = new TapsellResult();
		//GuiTextDebugdebug ("adId = " + node ["adId"].Value);
		result.adId = node ["adId"].Value;
		//GuiTextDebugdebug ("adId = " + result.adId);
		result.zoneId = node ["zondeId"].Value;
		Tapsell.onAdAvailable (result);
	}

	public void NotifyNoAdAvailable(String zone){
		//GuiTextDebugdebug ("NotifyNoAdAvailable:"+zone);
		Tapsell.onNoAdAvailable (zone);
	}

	public void NotifyExpiring(String str){
		//GuiTextDebugdebug ("NotifyExpiring:"+str);
		JSONNode node = JSON.Parse (str);
		TapsellResult result = new TapsellResult();
		result.adId = node ["adId"].Value;
		result.zoneId = node ["zondeId"].Value;
		Tapsell.onExpiring (result);
	}

	public void NotifyNoNetwork(String zone){
		//GuiTextDebugdebug ("NotifyNoNetwork:"+zone);
		Tapsell.onNoNetwork (zone);
	}

	public void NotifyAdShowFinished(String str)
	{
		//GuiTextDebugdebug ("NotifyAdShowFinished:"+str);
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
		//GuiTextDebugdebug (str);
	}

}

