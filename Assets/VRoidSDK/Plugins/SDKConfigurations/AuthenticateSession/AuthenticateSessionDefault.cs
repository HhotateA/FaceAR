using System.Runtime.InteropServices; 
using UnityEngine;

namespace VRoidSDK
{
#if !UNITY_EDITOR && UNITY_ANDROID && ENABLE_MONO
    #error Unsupported for Mono Build on Android. Please use IL2CPP Scripting Backend for Android Devices.
#else
    internal class AuthenticateSessionDefault : IAuthenticateSession
    {
        public void OpenURL(string url, string urlScheme)
        {
            Application.OpenURL(url);
        }
    }
#endif
}