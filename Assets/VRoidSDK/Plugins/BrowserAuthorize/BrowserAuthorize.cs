using UnityEngine;
using System.Collections;
using System;
using System.Text;

namespace VRoidSDK
{
    /// <summary>
    /// ブラウザを開き、アプリケーションの認証を行うためのクラス
    /// </summary>
    public class BrowserAuthorize : MonoBehaviour
    {
        private static SDKConfiguration sdkConfiguration;
        private Action<bool> OnRegistered = null;

        /// <summary>
        /// ブラウザ認証用のGameObjectインスタンスを作成する
        /// </summary>
        /// <param name="sdkConfig">アプリケーションの設定情報</param>
        /// <returns>ブラウザ認証インスタンス</returns>
        public static BrowserAuthorize GenerateInstance(SDKConfiguration sdkConfig)
        {
            BrowserAuthorize.sdkConfiguration = sdkConfig;
            GameObject instanceGo = new GameObject("BrowserAuthorize");
            GameObject.DontDestroyOnLoad(instanceGo);
            return instanceGo.AddComponent<BrowserAuthorize>();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
#if UNITY_ANDROID
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            if (activity == null)
            {
                return;
            }
            AndroidJavaObject intent = activity.Call<AndroidJavaObject>("getIntent");
            if (intent == null){
                return;
            }
            AndroidJavaObject intentUri = intent.Call<AndroidJavaObject>("getData");
            if (intentUri == null)
            {
                return;
            }
            string code = intentUri.Call<string>("getQueryParameter", "code");
            if(string.IsNullOrEmpty(code)){
                return;
            }
            RegisterCode(code);
#endif
        }

        /// <summary>
        /// OAuth認証後のリダイレクト先
        /// </summary>
        public string RedirectUri{
            get
            {
#if UNITY_EDITOR
                return "urn:ietf:wg:oauth:2.0:oob";
#elif UNITY_ANDROID
                return sdkConfiguration.AndroidUrlScheme;
#elif UNITY_IOS
                return sdkConfiguration.IOSUrlScheme;
#else
                return "urn:ietf:wg:oauth:2.0:oob";
#endif
            }
        }

        /// <summary>
        /// OAuthの認証コードを発行するためにブラウザを開く
        /// </summary>
        /// <param name="onRegistered">登録完了後のコールバック関数</param>
        public void OpenBrowser(Action<bool> onRegistered)
        {
            OnRegistered = onRegistered;
            var scopedText = sdkConfiguration.JoinScope();
            Authentication.Instance.BrowserAuthorized(this.RedirectUri, scopedText);
        }

        /// <summary>
        /// URLスキーマによりリダイレクトされたときに呼び出されるメソッド.
        /// パスに埋め込まれている認可コードを取り出して、登録を行う
        /// </summary>
        /// <param name="url">リダイレクトURL</param>
        public void OnOpenUrl(string url)
        {
            string authCode = "";
            string[] pathQuery = url.Split('?');
            if (pathQuery.Length > 1)
            {
                string[] urlQueryPairs = pathQuery[pathQuery.Length - 1].Split('&');
                for (int i = 0; i < urlQueryPairs.Length; ++i)
                {
                    string[] keyValue = urlQueryPairs[i].Split('=');
                    if (keyValue.Length > 1 && keyValue[0] == "code")
                    {
                        authCode = keyValue[1];
                        break;
                    }
                }
            }
            RegisterCode(authCode);
        }

        /// <summary>
        /// ブラウザ認証がキャンセルされたときに呼ばれるメソッド
        /// </summary>
        /// <param name="_message">メッセージ</param>
        public void OnCancelAuthorize(string _message)
        {
            if (OnRegistered != null)
            {
                OnRegistered(false);
            }
        }

        /// <summary>
        /// 認可コードを登録する
        /// </summary>
        /// <param name="authCode">登録する認可コード</param>
        public void RegisterCode(string authCode){
            Authentication.Instance.RegisterCode(authCode, this.RedirectUri, (bool isSuccess) => {
                if(OnRegistered != null) OnRegistered(isSuccess);
                if(isSuccess){
                    Destroy(this.gameObject);
                }
            });
        }
    }
}