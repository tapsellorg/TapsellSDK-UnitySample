using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;

namespace TapsellSDK.Editor {
	public class TapsellSettings{
		private static string pluginVersion = "3.0.34";

		public static string getPluginVersion()
		{
			return pluginVersion;
		}

		public static void setLatestPluginVersion(string newVersion)
		{
			PlayerPrefs.SetString ("TapsellLatestVersion", newVersion);
		}

		public static string getLatestPluginVersion()
		{
			return PlayerPrefs.GetString ("TapsellLatestVersion", null);
		}
	}
}