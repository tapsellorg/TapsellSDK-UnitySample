using System.Collections;
using System.Collections.Generic;
using TapsellSDK;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AdProvider : MonoBehaviour
{

	public string ZoneId; // 5a3f5063fca4f000014b70a8
	public static TapsellAd ad;

	public void Request () {

		Tapsell.RequestAd (ZoneId, true,
			(TapsellAd result) => {
				// onAdAvailable
				Debug.Log ("on Ad Available");
				ad = result;
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
			},

			(TapsellAd result) => {
				// onExpiring
				Debug.Log ("expiring");
			},

			(TapsellAd result) => {
				// onOpen
				Debug.Log ("open");
			},

			(TapsellAd result) => {
				// onClose
				Debug.Log ("close");
			}

		);
	}

	public void Show () {
		Debug.Log("SHow add clicked");
		Debug.Log(ZoneId);
		Debug.Log(ad.adId);
		Tapsell.ShowAd (ZoneId, ad, new TapsellShowOptions ());
	}
}
