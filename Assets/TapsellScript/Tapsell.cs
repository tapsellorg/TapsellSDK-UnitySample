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
	public static int ROTATION_LOCKED_REVERSED_PORTRAIT = 4;
	public static int ROTATION_LOCKED_REVERSED_LANDSCAPE = 5;

	public bool backDisabled=false;
	public bool immersiveMode=false;
	public int rotationMode=ROTATION_UNLOCKED;
	public bool showDialog = false;
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

	private static string defaultTapsellZone = "defaultTapsellZone";

	private static GameObject tapsellManager;

	public static void initialize(string key){
		tapsellManager = new GameObject ("TapsellManager");
		UnityEngine.Object.DontDestroyOnLoad (tapsellManager);
		tapsellManager.AddComponent<TapsellMessageHandler>();
		#if UNITY_ANDROID
		setJavaObject();
		tapsell.CallStatic<Boolean>("initialize", key);
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

	public static void setAutoHandlePermissions(bool autoHandle)
	{
		#if UNITY_ANDROID
		tapsell.CallStatic("setAutoHandlePermissions",autoHandle);
		#endif
	}

	public static void setMaxAllowedBandwidthUsage(int maxBpsSpeed)
	{
		#if UNITY_ANDROID
		tapsell.CallStatic("setMaxAllowedBandwidthUsage",maxBpsSpeed);
		#endif
	}

	public static void setMaxAllowedBandwidthUsagePercentage(int maxPercentage)
	{
		#if UNITY_ANDROID
		tapsell.CallStatic("setMaxAllowedBandwidthUsagePercentage",maxPercentage);
		#endif
	}

	public static void clearBandwidthUsageConstrains()
	{
		#if UNITY_ANDROID
		tapsell.CallStatic("clearBandwidthUsageConstrains");
		#endif
	}

	public static bool requestAd(string zoneId, Boolean isCached, Action<TapsellResult> onAdAvailableAction, Action<string> onNoAdAvailableAction,
		Action<TapsellError> onErrorAction, Action<string> onNoNetworkAction, Action<TapsellResult> onExpiringAction)
	{
		#if UNITY_ANDROID
		string zone = zoneId;
		if(String.IsNullOrEmpty(zoneId))
		{
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

		return tapsell.CallStatic<Boolean>("requestAd",zone,isCached);
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

		tapsell.CallStatic("showAd",adId,showOptions.backDisabled,showOptions.immersiveMode,showOptions.rotationMode,showOptions.showDialog);

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
		}
	}

	public static void onAdAvailable(TapsellResult result)
	{
		Debug.Log ("onAdAvailable");
		string zone = result.zoneId;
		Debug.Log ("zone = "+zone);
		if(String.IsNullOrEmpty(zone))
		{
			zone=defaultTapsellZone;
		}
		if (requestAdAvailablePool.ContainsKey (zone))
		{
			requestAdAvailablePool [zone](result);
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
		}
	}

	public static void onAdShowFinished(TapsellAdFinishedResult result)
	{
		if (adFinishedPool.ContainsKey (result.adId))
		{
			adFinishedPool [result.adId] (result);
		}
	}


}

