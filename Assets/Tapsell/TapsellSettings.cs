using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;

namespace TapsellSDK.Editor
{
    public class TapsellSettings
    {
        private static string pluginVersion = "4.2.2.0";

        public static string getPluginVersion()
        {
            return pluginVersion;
        }
    }
}