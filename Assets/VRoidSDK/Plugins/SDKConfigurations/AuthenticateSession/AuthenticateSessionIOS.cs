using System.Runtime.InteropServices; 
using UnityEngine;

namespace VRoidSDK
{
#if !UNITY_EDITOR && UNITY_IOS
    internal class AuthenticateSessionIOS : IAuthenticateSession
    {
        [DllImport("__Internal")]
        private static extern void OpenBrowserWindow(string url, string urlScheme);

        public void OpenURL(string url, string urlScheme)
        {
            OpenBrowserWindow(url, urlScheme);
        }
    }
#endif
}