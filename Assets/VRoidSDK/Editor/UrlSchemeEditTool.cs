using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VRoidSDK.Editor
{
    public class UrlSchemeEditTool
    {
        private const string AndroidManifestDirectory = "Assets/Plugins/Android/";
        private const string AndroidManifestName = "AndroidManifest.xml";
        public const string AndroidManifestExamplePath = AndroidManifestDirectory + AndroidManifestName + ".example";

        public static SDKConfiguration LoadSDKConfiguration()
        {
            string[] pathes = AssetDatabase.GetAllAssetPaths();
            for (int i = 0; i < pathes.Length; ++i)
            {
                SDKConfiguration sdkConfiguration = AssetDatabase.LoadAssetAtPath<SDKConfiguration>(pathes[i]);
                if (sdkConfiguration != null)
                {
                    return sdkConfiguration;
                }
            }
            return null;
        }
    }
}
