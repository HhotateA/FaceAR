using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using VRM;

namespace VRoidSDK
{
    /// <summary>
    /// VRoid Hubのキャラクターを3Dモデルとして読み込む機能を提供するシングルトン
    /// </summary>
    public class HubModelDeserializer : MonoBehaviour, ICoroutineHandlable
    {
        // Singleton
        private static HubModelDeserializer instance;

        /// <summary>
        /// シングルトンオブジェクトを取り出す
        /// </summary>
        public static HubModelDeserializer Instance
        {
            get
            {
                if (HubModelDeserializer.instance == null)
                {
                    GameObject gameObject = new GameObject(Guid.NewGuid().ToString());
                    DontDestroyOnLoad(gameObject);
                    HubModelDeserializer.instance = gameObject.AddComponent<HubModelDeserializer>();
                }
                return HubModelDeserializer.instance;
            }
        }

        /// <summary>
        /// VRoid HubのキャラクターモデルIDからキャラクターモデルのGameObjectを取得する
        /// </summary>
        /// <remarks>
        /// 初めて取り込むキャラクターモデルは、VRoidHubApi経由でモデルデータをダウンロードし、LocalStorageにキャッシュされる。
        /// 一度取り込まれたキャラクターモデルは、次からキャッシュから読み込まれるようになる
        /// </remarks>
        /// <param name="characterModelId">取り出すキャラクターモデルID</param>
        /// <param name="maxCacheCount">最大で保持するキャッシュの数</param>
        /// <param name="onLoadComplete">キャラクターモデルの読み込みに成功した時のコールバック</param>
        /// <param name="onDownloadProgress">ダウンロードの進捗状況を通知するコールバック</param>
        /// <param name="onError">エラー発生時のコールバック</param>
        public void LoadCharacterAsync(string characterModelId,
                                       uint maxCacheCount,
                                       Action<GameObject> onLoadComplete,
                                       Action<float> onDownloadProgress,
                                       Action<Exception> onError)
        {
            ModelLoaderFactory.Create(characterModelId, this, maxCacheCount, (loader) => {
                loader.OnVrmModelLoaded = onLoadComplete;
                loader.OnProgress = onDownloadProgress;
                loader.OnError = onError;
                loader.Load();
            }, (ApiErrorFormat errorFormat) => {
                onError(new Exception(errorFormat.message));
            });
        }

        /// <summary>
        /// VRoid HubのキャラクターモデルIDからキャラクターモデルのGameObjectを取得する
        /// </summary>
        /// <remarks>
        /// 初めて取り込むキャラクターモデルは、VRoidHubApi経由でモデルデータをダウンロードし、LocalStorageにキャッシュされる。
        /// 一度取り込まれたキャラクターモデルは、次からキャッシュから読み込まれるようになる (最大10件までキャッシュを保持します。)
        /// </remarks>
        /// <param name="characterModelId">取り出すキャラクターモデルID</param>
        /// <param name="onLoadComplete">キャラクターモデルの読み込みに成功した時のコールバック</param>
        /// <param name="onDownloadProgress">ダウンロードの進捗状況を通知するコールバック</param>
        /// <param name="onError">エラー発生時のコールバック</param>
        public void LoadCharacterAsync(string characterModelId,
                                       Action<GameObject> onLoadComplete,
                                       Action<float> onDownloadProgress,
                                       Action<Exception> onError)
        {
            LoadCharacterAsync(characterModelId, 10, onLoadComplete, onDownloadProgress, onError);
        }

        /// <summary>
        /// コルーチン処理を実行する
        /// </summary>
        /// <param name="routine">処理するコルーチン</param>
        public void RunMonoCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}
