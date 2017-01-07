using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TapsellError
{
	public string error;
	public string zoneId;
}

[Serializable]
public class TapsellResult
{
	public string adId;
	public string zoneId;
}

[Serializable]
public class TapsellAdFinishedResult
{
	public string adId;
	public string zoneId;
	public bool completed;
	public bool rewarded;
}

public class TapsellShowOptions
{
	public static int ROTATION_LOCKED_PORTRAIT = 1;
	public static int ROTATION_LOCKED_LANDSCAPE = 2;
	public static int ROTATION_UNLOCKED = 3;

	public bool backDisabled=false;
	public bool immersiveMode=false;
	public int rotationMode=ROTATION_UNLOCKED;
}

public class Tapsell
{
	
	private static AndroidJavaClass tapsell;

	private static Dictionary<string,Action<TapsellError>> requestErrorPool = new Dictionary<string,Action<TapsellError>>();
	private static Dictionary<string,Action<TapsellResult>> requestAdAvailablePool = new Dictionary<string,Action<TapsellResult>>();
	private static Dictionary<string,Action<string>> requestNoAdAvailablePool = new Dictionary<string,Action<string>>();
	private static Dictionary<string,Action<string>> requestNoNetworkPool = new Dictionary<string,Action<string>>();
	private static Dictionary<string,Action<TapsellResult>> requestExpiringPool = new Dictionary<string,Action<TapsellResult>>();
	private static Dictionary<string,Action<TapsellAdFinishedResult>> adFinishedPool = new Dictionary<string,Action<TapsellAdFinishedResult>>();

	//private static Dictionary<string, TapsellAdRequestListener> requestListenerPool = new Dictionary<string, TapsellAdRequestListener>();
	//private static Dictionary<string, TapsellAdShowListener> showListenerPool = new Dictionary<string, TapsellAdShowListener>();

	private static string defaultTapsellZone = "defaultTapsellZone";

	private static GameObject tapsellManager;

	public static void initialize(string key){
		tapsellManager = new GameObject ("TapsellManager");
		UnityEngine.Object.DontDestroyOnLoad (tapsellManager);
		tapsellManager.AddComponent<TapsellMessageHandler>();
		#if UNITY_ANDROID
		setJavaObject();
		bool a= tapsell.CallStatic<Boolean>("initialize", key);
		//GuiTextDebug.debug("tapsell initialize: "+a);
		#endif
	}

	private static void setJavaObject(){
		#if UNITY_ANDROID
		tapsell = new AndroidJavaClass("ir.tapsell.sdk.TapsellUnity");
		#endif
	}

	public static void setDebugMode(bool debug){
		#if UNITY_ANDROID
		tapsell.CallStatic("setDebugMode",debug);
		#endif
	}

	public static bool isAdReadyToShow(String zoneId)
	{
		#if UNITY_ANDROID
		return tapsell.CallStatic<Boolean>("isAdReadyToShow",zoneId);
		#else
		return false;
		#endif
	}

	public static bool isDebugMode()
	{
		#if UNITY_ANDROID
		return tapsell.CallStatic<Boolean>("isDebugMode");
		#else
		return false;
		#endif

	}

	public static void setAppUserId(string appUserId)
	{
		#if UNITY_ANDROID
		tapsell.CallStatic("setAppUserId",appUserId);
		#endif
	}

	public static string getAppUserId()
	{
		#if UNITY_ANDROID
		return tapsell.CallStatic<String>("getAppUserId");
		#else
		return null;
		#endif
	}

	public static bool requestAd(string zoneId, Action<TapsellResult> onAdAvailableAction, Action<string> onNoAdAvailableAction,
		Action<TapsellError> onErrorAction, Action<string> onNoNetworkAction, Action<TapsellResult> onExpiringAction)
	{
		#if UNITY_ANDROID
		string zone = zoneId;
		//GuiTextDebug.debug("requestAd zone= "+zone);
		if(String.IsNullOrEmpty(zoneId))
		{
			//GuiTextDebug.debug("requestAd zone is null");
			zoneId="defaultTapsellZone";
		}
		if( requestAdAvailablePool.ContainsKey (zoneId) )
		{
			requestAdAvailablePool.Remove(zoneId);
		}
		requestAdAvailablePool.Add(zoneId,onAdAvailableAction);

		if( requestErrorPool.ContainsKey (zoneId) )
		{
			requestErrorPool.Remove(zoneId);
		}
		requestErrorPool.Add(zoneId,onErrorAction);

		if( requestNoAdAvailablePool.ContainsKey(zoneId) )
		{
			requestNoAdAvailablePool.Remove(zoneId);
		}
		requestNoAdAvailablePool.Add(zoneId,onNoAdAvailableAction);

		if( requestNoNetworkPool.ContainsKey(zoneId) )
		{
			requestNoNetworkPool.Remove(zoneId);
		}
		requestNoNetworkPool.Add(zoneId,onNoNetworkAction);

		if( requestExpiringPool.ContainsKey (zoneId) )
		{
			requestExpiringPool.Remove(zoneId);
		}
		requestExpiringPool.Add(zoneId,onExpiringAction);

		return tapsell.CallStatic<Boolean>("requestAd",zone);
		#else
		return false;
		#endif
	}

	public static void showAd(String adId, TapsellShowOptions showOptions,Action<TapsellAdFinishedResult> onFinishedAction)
	{
		#if UNITY_ANDROID
		if(object.ReferenceEquals(showOptions,null))
		{
			showOptions = new TapsellShowOptions();
		}

		if(adFinishedPool.ContainsKey(adId))
		{
			adFinishedPool.Remove(adId);
		}
		adFinishedPool.Add(adId,onFinishedAction);

		tapsell.CallStatic("showAd",adId,showOptions.backDisabled,showOptions.immersiveMode,showOptions.rotationMode);

		#endif
	}

	public static void onError(TapsellError error)
	{
		string zoneId = error.zoneId;
		if(String.IsNullOrEmpty(zoneId))
		{
			zoneId=defaultTapsellZone;
		}
		if (requestErrorPool.ContainsKey (zoneId))
		{
			requestErrorPool [zoneId](error);
			//requestErrorPool.Remove (zoneId);
		}
	}

	public static void onNoAdAvailable(String zone)
	{
		string zoneId = zone;
		if(String.IsNullOrEmpty(zoneId))
		{
			zoneId=defaultTapsellZone;
		}
		if (requestNoAdAvailablePool.ContainsKey (zoneId))
		{
			requestNoAdAvailablePool [zoneId](zone);
			//requestNoAdAvailablePool.Remove (zoneId);
		}
	}

	public static void onNoNetwork(String zone)
	{
		string zoneId = zone;
		if(String.IsNullOrEmpty(zoneId))
		{
			zoneId=defaultTapsellZone;
		}
		if (requestNoNetworkPool.ContainsKey (zoneId))
		{
			requestNoNetworkPool [zoneId] (zone);
			//requestNoNetworkPool.Remove (zoneId);
		}
	}

	public static void onAdAvailable(TapsellResult result)
	{
		////GuiTextDebug.debug ("Tapsell.cs onAdAvailable, result null? : "+object.ReferenceEquals(null,result));
		////GuiTextDebug.debug ("Tapsell.cs onAdAvailable, zoneId null? : "+String.IsNullOrEmpty(result.zoneId));
		////GuiTextDebug.debug ("Tapsell.cs onAdAvailable, adId  : "+result.adId);
		string zone = result.zoneId;
		if(String.IsNullOrEmpty(zone))
		{
			////GuiTextDebug.debug ("Tapsell.cs onAdAvailable, zoneId is null");
			zone=defaultTapsellZone;
		}
		if (requestAdAvailablePool.ContainsKey (zone))
		{
			////GuiTextDebug.debug ("Tapsell.cs onAdAvailable, request pull contains zone ");
			requestAdAvailablePool [zone](result);
			//requestAdAvailablePool.Remove (zone);
		}
	}

	public static void onExpiring(TapsellResult result)
	{
		string zone = result.zoneId;
		if(String.IsNullOrEmpty(zone))
		{
			zone=defaultTapsellZone;
		}
		if (requestExpiringPool.ContainsKey (zone))
		{
			requestExpiringPool [zone] (result);
			//requestExpiringPool.Remove (zone);
		}
	}

	public static void onAdShowFinished(TapsellAdFinishedResult result)
	{
		if (adFinishedPool.ContainsKey (result.adId))
		{
			adFinishedPool [result.adId] (result);
			//adFinishedPool.Remove (result.adId);
		}
	}


}

