using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif


namespace TapsellSDK {

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
	public class TapsellNativeBannerResult
	{
		public string adId;
		public string zoneId;
		public string title;
		public string description;
		public string iconUrl;
		public string callToActionText;
		public string portraitStaticImageUrl;
		public string landscapeStaticImageUrl;

		public bool shownReported = false;

		public Texture2D portraitBannerImage;
		public Texture2D landscapeBannerImage;
		public Texture2D iconImage;

		public string getTitle()
		{
			return title;
		}

		public string getDescription()
		{
			return description;
		}

		public string getCallToAction()
		{
			return callToActionText;
		}

		public Texture2D getPortraitBannerImage()
		{
			return portraitBannerImage;
		}

		public Texture2D getLandscapeBannerImage()
		{
			return landscapeBannerImage;
		}

		public Texture2D getIcon()
		{
			return iconImage;
		}

		public void onShown()
		{
			if (!this.shownReported)
			{
				Tapsell.onNativeBannerAdShown (this.adId);
				this.shownReported = true;
			}
		}

		public void onClicked()
		{
			Tapsell.onNativeBannerAdClicked (this.adId);
		}
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

		public const int PERMISSION_HANDLER_DISABLED = 0;
		public const int PERMISSION_HANDLER_AUTO = 1;
		public const int PERMISSION_HANDLER_AUTO_INSIST = 2;

		#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern void _TSInitialize(string appkey);
		[DllImport ("__Internal")]
		private static extern void _TSRequestAdForZone(string zoneId, string cached);
		[DllImport ("__Internal")]
		private static extern void _TSShowAd(string adId, string backDisabled, int rotationMode, string showDialog);
		[DllImport ("__Internal")]
		private static extern string _TSGetVersion();
		[DllImport ("__Internal")]
		private static extern void _TSSetDebugMode(string debugMode);
		[DllImport ("__Internal")]
		private static extern string _TSIsDebugMode();
		[DllImport ("__Internal")]
		private static extern void _TSSetAppUserId(string appUserId);
		[DllImport ("__Internal")]
		private static extern string _TSGetAppUserId();
		#endif

		#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaClass tapsell;
		#endif

		private static Dictionary<string,Action<TapsellNativeBannerResult>> requestNativeBannerFilledPool = new Dictionary<string,Action<TapsellNativeBannerResult>>();
		private static Dictionary<string,Action<TapsellError>> requestNativeBannerErrorPool = new Dictionary<string,Action<TapsellError>>();
		private static Dictionary<string,Action<string>> requestNativeBannerNoAdAvailablePool = new Dictionary<string,Action<string>>();
		private static Dictionary<string,Action<string>> requestNativeBannerNoNetworkPool = new Dictionary<string,Action<string>>();

		private static Dictionary<string,Action<TapsellError>> requestErrorPool = new Dictionary<string,Action<TapsellError>>();
		private static Dictionary<string,Action<TapsellResult>> requestAdAvailablePool = new Dictionary<string,Action<TapsellResult>>();
		private static Dictionary<string,Action<string>> requestNoAdAvailablePool = new Dictionary<string,Action<string>>();
		private static Dictionary<string,Action<string>> requestNoNetworkPool = new Dictionary<string,Action<string>>();
		private static Dictionary<string,Action<TapsellResult>> requestExpiringPool = new Dictionary<string,Action<TapsellResult>>();
		private static Dictionary<string,Action<TapsellResult>> requestAdOpenedPool = new Dictionary<string,Action<TapsellResult>>();
		private static Dictionary<string,Action<TapsellResult>> requestAdClosedPool = new Dictionary<string,Action<TapsellResult>>();
		private static Action<TapsellAdFinishedResult> adFinishedAction = null;//new Action<TapsellAdFinishedResult>();

		#if UNITY_ANDROID && !UNITY_EDITOR
		private static MonoBehaviour mMonoBehaviour;
		#endif

		private static string defaultTapsellZone = "defaultTapsellZone";

		private static GameObject tapsellManager;

		/// <summary>
		/// Initializes Tapsell SDK with the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public static void initialize(string key){
			tapsellManager = new GameObject ("TapsellManager");
			UnityEngine.Object.DontDestroyOnLoad (tapsellManager);
			tapsellManager.AddComponent<TapsellMessageHandler>();
			#if UNITY_ANDROID && !UNITY_EDITOR
			setJavaObject();
			tapsell.CallStatic<Boolean>("initialize", key);
			#elif UNITY_IOS && !UNITY_EDITOR
			_TSInitialize(key);
			#endif
			//#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
			//setDebugMode(TapsellSettings.getDebugMode());
			//setPermissionHandlerConfig(TapsellSettings.getTapsellAndroidPermissionHandlingMode());
			//#endif
		}
			
		private static void setJavaObject(){
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell = new AndroidJavaClass("ir.tapsell.sdk.TapsellUnity");
			#endif
		}

		/// <summary>
		/// Sets the debug mode.
		/// </summary>
		/// <param name="debug">If set to <c>true</c> debug.</param>
		public static void setDebugMode(bool debug){
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("setDebugMode",debug);
			#elif UNITY_IOS && !UNITY_EDITOR
			string debugMode = "false";
			if(debug)
			{
				debugMode = "true";
			}
			_TSSetDebugMode(debugMode);
			#endif
		}

		[System.Obsolete("isAdReadyToShow is deprecated and doesn't work on iOS sdk.")]
		/// <summary>
		/// Is the ad ready to show. Only for Android SDK.
		/// </summary>
		/// <returns><c>true</c>, if ad ready to show was ised, <c>false</c> otherwise.</returns>
		/// <param name="zoneId">Zone identifier.</param>
		public static bool isAdReadyToShow(String zoneId)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return tapsell.CallStatic<Boolean>("isAdReadyToShow",zoneId);
			#elif UNITY_IOS && !UNITY_EDITOR
			return false;
			#else
			return false;
			#endif
		}

		public static bool isDebugMode()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return tapsell.CallStatic<Boolean>("isDebugMode");
			#elif UNITY_IOS && !UNITY_EDITOR
			return _TSIsDebugMode()=="true";
			#else
			return false;
			#endif

		}

		/// <summary>
		/// Sets the app user identifier.
		/// </summary>
		/// <param name="appUserId">App user identifier.</param>
		public static void setAppUserId(string appUserId)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("setAppUserId",appUserId);
			#elif UNITY_IOS && !UNITY_EDITOR
			_TSSetAppUserId(appUserId);
			#endif
		}
			
		/// <summary>
		/// Gets the app user identifier.
		/// </summary>
		/// <returns>The app user identifier.</returns>
		public static string getAppUserId()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return tapsell.CallStatic<String>("getAppUserId");
			#elif UNITY_IOS && !UNITY_EDITOR
			return _TSGetAppUserId();
			#else
			return null;
			#endif
		}

		/// <summary>
		/// Sets the auto handle permissions. Only for Android SDK;
		/// </summary>
		/// <param name="autoHandle">If set to <c>true</c> auto handle.</param>
		[System.Obsolete("setAutoHandlePermissions is deprecated. Please use setPermissionHandlerConfig instead.")]
		public static void setAutoHandlePermissions(bool autoHandle)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			setPermissionHandlerConfig(autoHandle ? Tapsell.PERMISSION_HANDLER_AUTO: Tapsell.PERMISSION_HANDLER_DISABLED );
			#elif UNITY_IOS && !UNITY_EDITOR
			// do nothing
			#endif
		}

		/// <summary>
		/// Sets the permission handler config. Passed parameter must be one of PERMISSION_HANDLER_DISABLED, PERMISSION_HANDLER_AUTO and PERMISSION_HANDLER_AUTO_INSIST
		/// </summary>
		/// <param name="config">Config must be one of PERMISSION_HANDLER_DISABLED, PERMISSION_HANDLER_AUTO and PERMISSION_HANDLER_AUTO_INSIST</param>
		public static void setPermissionHandlerConfig(int config)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("setPermissionHandlerConfig",config);
			#elif UNITY_IOS && !UNITY_EDITOR
			// do nothing
			#endif
		}

		/// <summary>
		/// Sets the max allowed bandwidth usage. Only available in Android SDK yet;
		/// </summary>
		/// <param name="maxBpsSpeed">Max bps speed.</param>
		public static void setMaxAllowedBandwidthUsage(int maxBpsSpeed)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("setMaxAllowedBandwidthUsage",maxBpsSpeed);
			#elif UNITY_IOS && !UNITY_EDITOR
			// do nothing
			#endif
		}

		/// <summary>
		/// Sets the max allowed bandwidth usage percentage.
		/// </summary>
		/// <param name="maxPercentage">Max percentage.</param>
		public static void setMaxAllowedBandwidthUsagePercentage(int maxPercentage)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("setMaxAllowedBandwidthUsagePercentage",maxPercentage);
			#elif UNITY_IOS && !UNITY_EDITOR
			// do nothing
			#endif
		}

		/// <summary>
		/// Clears the bandwidth usage constrains. Only available in Android SDK yet;
		/// </summary>
		public static void clearBandwidthUsageConstrains()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("clearBandwidthUsageConstrains");
			#elif UNITY_IOS && !UNITY_EDITOR
			// do nothing
			#endif
		}

		/// <summary>
		/// Requests an ad from Tapsell SDK
		/// </summary>
		/// <returns><c>true</c>, if ad was requested, <c>false</c> otherwise.</returns>
		/// <param name="zoneId">Zone identifier.</param>
		/// <param name="isCached">If set to <c>true</c> is cached.</param>
		/// <param name="onAdAvailableAction">On ad available action.</param>
		/// <param name="onNoAdAvailableAction">On no ad available action.</param>
		/// <param name="onErrorAction">On error action.</param>
		/// <param name="onNoNetworkAction">On no network action.</param>
		/// <param name="onExpiringAction">On expiring action.</param>
		/// <param name="onOpenedAction">On opened action.</param>
		/// <param name="onClosedAction">On closed action.</param>
		public static bool requestAd(string zoneId, Boolean isCached, Action<TapsellResult> onAdAvailableAction, Action<string> onNoAdAvailableAction,
			Action<TapsellError> onErrorAction, Action<string> onNoNetworkAction, Action<TapsellResult> onExpiringAction)
		{
			return requestAd (zoneId, isCached, onAdAvailableAction, onNoAdAvailableAction, onErrorAction, onNoNetworkAction, onExpiringAction, null, null);
		}

		/// <summary>
		/// Requests an ad from Tapsell SDK.
		/// </summary>
		/// <returns><c>true</c>, if ad was requested, <c>false</c> otherwise.</returns>
		/// <param name="zoneId">Zone identifier.</param>
		/// <param name="isCached">If set to <c>true</c> is cached, else streamed.</param>
		/// <param name="onAdAvailableAction">On ad available action.</param>
		/// <param name="onNoAdAvailableAction">On no ad available action.</param>
		/// <param name="onErrorAction">On error action.</param>
		/// <param name="onNoNetworkAction">On no network action.</param>
		/// <param name="onExpiringAction">On expiring action.</param>
		public static bool requestAd(string zoneId, Boolean isCached, Action<TapsellResult> onAdAvailableAction, Action<string> onNoAdAvailableAction,
			Action<TapsellError> onErrorAction, Action<string> onNoNetworkAction, Action<TapsellResult> onExpiringAction, Action<TapsellResult> onOpenedAction,
			Action<TapsellResult> onClosedAction)
		{
			#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
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

			if( requestAdOpenedPool.ContainsKey (zoneId) )
			{
				requestAdOpenedPool.Remove(zoneId);
			}
			if(onOpenedAction!=null)
			{
				requestAdOpenedPool.Add(zoneId,onOpenedAction);
			}

			if( requestAdClosedPool.ContainsKey (zoneId) )
			{
				requestAdClosedPool.Remove(zoneId);
			}
			if(onClosedAction!=null)
			{
				requestAdClosedPool.Add(zoneId,onClosedAction);
			}
			#endif
			#if UNITY_ANDROID && !UNITY_EDITOR
			return tapsell.CallStatic<Boolean>("requestAd",zone,isCached);
			#elif UNITY_IOS && !UNITY_EDITOR
			string cached = "false";
			if(isCached)
			{
				cached="true";
			}
			_TSRequestAdForZone(zone, cached);
			return true;
			#else
			TapsellError error = new TapsellError();
			error.zoneId = zoneId;
			error.error = "Tapsell ads are only available on Android and iOS platforms.";
			return false;
			#endif
		}

		public static void requestNativeBannerAd(MonoBehaviour monoBehaviour,string zoneId, Action<TapsellNativeBannerResult> onRequestFilled, Action<string> onNoAdAvailableAction,
			Action<TapsellError> onErrorAction, Action<string> onNoNetworkAction)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			mMonoBehaviour = monoBehaviour;
			if( requestNativeBannerFilledPool.ContainsKey (zoneId) )
			{
				requestNativeBannerFilledPool.Remove(zoneId);
			}
			if( requestNativeBannerErrorPool.ContainsKey (zoneId) )
			{
				requestNativeBannerErrorPool.Remove(zoneId);
			}
			if( requestNativeBannerNoAdAvailablePool.ContainsKey (zoneId) )
			{
				requestNativeBannerNoAdAvailablePool.Remove(zoneId);
			}
			if( requestNativeBannerNoNetworkPool.ContainsKey (zoneId) )
			{
				requestNativeBannerNoNetworkPool.Remove(zoneId);
			}
			requestNativeBannerFilledPool.Add (zoneId, onRequestFilled);
			requestNativeBannerErrorPool.Add (zoneId, onErrorAction);
			requestNativeBannerNoAdAvailablePool.Add (zoneId, onNoAdAvailableAction);
			requestNativeBannerNoNetworkPool.Add (zoneId, onNoNetworkAction);
			tapsell.CallStatic("requestNativeBannerAd",zoneId);
			#else
			TapsellError error = new TapsellError();
			error.zoneId = zoneId;
			error.error = "Native ads are only available on Android platform.";
			onErrorAction(error);
			#endif
		}

		/// <summary>
		/// Shows the ad with specified options.
		/// </summary>
		/// <param name="adId">Ad identifier.</param>
		/// <param name="showOptions">Show options.</param>
		public static void showAd(String adId, TapsellShowOptions showOptions)
		{
			if(object.ReferenceEquals(showOptions,null))
			{
				showOptions = new TapsellShowOptions();
			}
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("showAd",adId,showOptions.backDisabled,showOptions.immersiveMode,showOptions.rotationMode,showOptions.showDialog);
			#elif UNITY_IOS && !UNITY_EDITOR
			string bDisabled = "false";
			if(showOptions.backDisabled){
				bDisabled = "true";
			}
			string sDialog = "false";
			if(showOptions.showDialog){
				sDialog = "true";
			}
			_TSShowAd(adId, bDisabled, showOptions.rotationMode, sDialog);
			#endif
		}

		public static void setRewardListener(Action<TapsellAdFinishedResult> onFinishedAction)
		{
			adFinishedAction = onFinishedAction;
		}

		public static String getVersion()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return tapsell.CallStatic<String>("getVersion");
			#elif UNITY_IOS && !UNITY_EDITOR
			return _TSGetVersion();
			#else
			return "NO SDK";
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
			string zone = result.zoneId;
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
			//if (adFinishedPool.ContainsKey (result.adId))
			//{
			//	adFinishedPool [result.adId] (result);
			//}
			if(adFinishedAction!=null)
			{
				adFinishedAction(result);
			}
		}

		public static void onOpened(TapsellResult result)
		{
			string zone = result.zoneId;
			if(String.IsNullOrEmpty(zone))
			{
				zone=defaultTapsellZone;
			}
			if (requestAdOpenedPool.ContainsKey (zone))
			{
				requestAdOpenedPool [zone] (result);
			}
		}

		public static void onClosed(TapsellResult result)
		{
			string zone = result.zoneId;
			if(String.IsNullOrEmpty(zone))
			{
				zone=defaultTapsellZone;
			}
			if (requestAdClosedPool.ContainsKey (zone))
			{
				requestAdClosedPool [zone] (result);
			}
		}

		// native ads

		public static void onNativeBannerError(TapsellError error)
		{
			string zoneId = error.zoneId;
			if (requestNativeBannerErrorPool.ContainsKey (zoneId))
			{
				requestNativeBannerErrorPool [zoneId](error);
			}
		}

		public static void onNativeBannerNoAdAvailable(String zone)
		{
			string zoneId = zone;
			if (requestNativeBannerNoAdAvailablePool.ContainsKey (zoneId))
			{
				requestNativeBannerNoAdAvailablePool [zoneId](zone);
			}
		}

		public static void onNativeBannerNoNetwork(String zone)
		{
			string zoneId = zone;
			if (requestNativeBannerNoNetworkPool.ContainsKey (zoneId))
			{
				requestNativeBannerNoNetworkPool [zoneId] (zone);
			}
		}

		public static void onNativeBannerRequestFilled(TapsellNativeBannerResult result)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			string zone = result.zoneId;
			if(result!=null)
			{
				if (mMonoBehaviour != null && mMonoBehaviour.isActiveAndEnabled) {
					mMonoBehaviour.StartCoroutine (loadNativeBannerAdImages(result));
				}
				else 
				{
					if (requestNativeBannerErrorPool.ContainsKey (zone))
					{
						TapsellError error = new TapsellError();
						error.zoneId = zone;
						error.error = "Invalid MonoBehaviour Object";
						requestNativeBannerErrorPool [zone](error);
					}
				}
			}
			else
			{
				if (requestNativeBannerErrorPool.ContainsKey (zone))
				{
					TapsellError error = new TapsellError();
					error.zoneId = zone;
					error.error = "Invalid Result";
					requestNativeBannerErrorPool [zone](error);
				}
			}
			#endif
		}

		static IEnumerator loadNativeBannerAdImages(TapsellNativeBannerResult result)
		{
			if(result.iconUrl!=null && !result.iconUrl.Equals(""))
			{
				WWW wwwIcon = new WWW (result.iconUrl);
				yield return wwwIcon;
				if(wwwIcon.texture!=null)
				{
					result.iconImage = wwwIcon.texture;
				}
			}
			if(result.portraitStaticImageUrl!=null && !result.portraitStaticImageUrl.Equals(""))
			{
				WWW wwwPortrait = new WWW (result.portraitStaticImageUrl);
				yield return wwwPortrait;
				if(wwwPortrait.texture!=null)
				{
					result.portraitBannerImage = wwwPortrait.texture;
				}
			}
			if(result.landscapeStaticImageUrl!=null && !result.landscapeStaticImageUrl.Equals(""))
			{
				WWW wwwLandscape = new WWW (result.landscapeStaticImageUrl);
				yield return wwwLandscape;
				if(wwwLandscape.texture!=null)
				{
					result.landscapeBannerImage = wwwLandscape.texture;
				}
			}
			string zone = result.zoneId;
			if (requestNativeBannerFilledPool.ContainsKey (zone))
			{
				requestNativeBannerFilledPool [zone](result);
			}
		}

		public static void onNativeBannerAdClicked(string adId)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("onNativeBannerAdClicked",adId);
			#endif
		}

		public static void onNativeBannerAdShown(string adId)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			tapsell.CallStatic("onNativeBannerAdShown",adId);
			#endif
		}
	}
}

