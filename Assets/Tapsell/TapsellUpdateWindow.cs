using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

namespace TapsellSDK.Editor
{
	public class TapsellUpdateWindow : EditorWindow
	{

		void OnGUI ()
		{
			titleContent = new GUIContent("Tapsell", "Tapsell Unity SDK Update");

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();

			GUILayout.Label(TapsellEditor.tapsellLogo);

			GUILayout.Label("Version: "+TapsellSettings.getPluginVersion());

			GUILayout.EndVertical();
			
			GUILayout.BeginVertical();

			GUILayout.Label("A new version of the Tapsell Unity SDK is available");

			EditorGUILayout.Space();

			GUILayout.Label("Current version: " + TapsellSettings.getPluginVersion());
			GUILayout.Label("Latest version: " + TapsellSettings.getLatestPluginVersion());

			EditorGUILayout.Space();

			EditorGUILayout.Space();

			EditorGUILayout.Space();

			EditorGUILayout.Space();

			GUILayout.Label("You can download the latest version from the Tapsell answers page.", EditorStyles.wordWrappedMiniLabel, GUILayout.MaxWidth(380));

			EditorGUILayout.Space();

			GUILayout.BeginHorizontal();

			if (GUILayout.Button(new GUIContent("Download & Docs (FA)", "Open the SDK docs webpage (Farsi)"), GUILayout.MaxWidth(150)))
			{
				Application.OpenURL("https://answers.tapsell.ir/?page_id=746");
			}

			if (GUILayout.Button(new GUIContent("Download & Docs (EN)", "Open the SDK docs webpage (English)"), GUILayout.MaxWidth(150)))
			{
				Application.OpenURL("https://answers.tapsell.ir/?page_id=968&lang=en");
			}

			if (GUILayout.Button(new GUIContent("Close", "Skip this version."), GUILayout.MaxWidth(60)))
			{
				EditorPrefs.SetString("ga_skip_version"+"-"+Application.dataPath, "New Version");
				Close();
			}

			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

		}
	}
}

#endif