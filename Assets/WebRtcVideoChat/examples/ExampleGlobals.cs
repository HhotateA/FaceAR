using System.Collections;
using Byn.Awrtc;
using UnityEngine;

namespace Byn.Unity.Examples
{
    /// <summary>
    /// Keeps the global urls / data used for the example applications and unit tests
    /// </summary>
    public class ExampleGlobals
    {
        /// <summary>
        /// Recommended signaling protcol.
        /// wss only for WebGL to avoid possible issues with ws due to certificates / platform
        /// specific SSL issues.
        /// </summary>
        public static string SignalingProtocol
        {
            get{

                string protocol = "ws";
                if(Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    protocol = "wss";
                }
                return protocol;
            }
        }
        /// <summary>
        /// Signaling. ws by default. wss for webgl
        /// </summary>
        public static string Signaling
        {
            get
            {
                return SignalingProtocol + "://signaling.because-why-not.com/test";
            }
        }

        /// <summary>
        /// Signaling for shared addresses (conference calls)
        /// </summary>
        public static string SharedSignaling
        {
            get
            {
                return SignalingProtocol + "://signaling.because-why-not.com/testshared";
            }
        }
        /// <summary>
        /// Signaling for the conference example app
        /// </summary>
        public static string SignalingConference
        {
            get
            {
                return SignalingProtocol + "://signaling.because-why-not.com/conferenceapp";
            }
        }
        /// <summary>
        /// Stun server
        /// </summary>
        public static readonly string StunUrl = "stun:stun.because-why-not.com:443";

        /// <summary>
        /// Turn server
        /// </summary>
        public static readonly string TurnUrl = "turn:turn.because-why-not.com:443";

        /// <summary>
        /// Turn server user (changed if overused)
        /// </summary>
        public static readonly string TurnUser = "testuser14";

        /// <summary>
        /// Turn server password (changed if userused)
        /// </summary>
        public static readonly string TurnPass = "pass14";


        public static IceServer DefaultIceServer
        {
            get{
                return new IceServer(TurnUrl, TurnUser, TurnPass);
            }
        }

        /// <summary>
        /// Backup stun server to keep essentials running during server maintenance
        /// </summary>
        public static readonly string BackupStunUrl = "stun:stun.l.google.com:19302";




        /// <summary>
        /// Only works on Android. Won't do anything on other platorms.
        /// This is a minimal permission helper for the example applications to remain as simile as possible. 
        /// They do not follow the guidlines of the Play Store or anything else! Don't use this
        /// in a final application!
        /// </summary>
        public static IEnumerator RequestPermissions()
        {
            bool hasAudio = true;
            bool hasVideo = true;
#if UNITY_ANDROID && UNITY_2018_3_OR_NEWER
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Microphone))
        {
            Debug.Log("Requesting microphone permissions");
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Microphone);
            //wait a frame. In some Unity versions calling it twice during a single frame will cause one dialog to be 
            //obmitted. Unity should stall here until the user either pressed allow or deny
            yield return null;
            hasAudio = UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Microphone);
        }
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera))
        {
            Debug.Log("Requesting camera permissions");
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
            yield return null;
            hasVideo = UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera);
        }

        Debug.Log("Returning from RequestPermission with audio: " + hasAudio + " video: " + hasVideo);
#endif
            yield return null;
        }
        
    }
}