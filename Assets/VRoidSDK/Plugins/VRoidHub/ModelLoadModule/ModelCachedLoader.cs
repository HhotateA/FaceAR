using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRoidSDK
{
    /// <summary>
    /// キャッシュからモデルを読み取る
    /// </summary>
    public class ModelCachedLoader : ModelLoaderBase
    {
        private ICoroutineHandlable CoroutineHandler;
        private CachedLicense CacheLicense;
        private Func<string, byte[]> ModelDecryptFunc;
        private ITaskQueue TaskQueue;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="license">キャッシュ化されたライセンス情報</param>
        /// <param name="coroutine">コルーチンが実行できるハンドラオブジェクト</param>
        /// <param name="taskQueue">非同期処理をキューを使って実行できる機能を持ったモジュール</param>
        /// <param name="decryptFunc">復号処理を行う関数</param>
        public ModelCachedLoader(CachedLicense license, ICoroutineHandlable coroutine, ITaskQueue taskQueue, Func<string, byte[]> decryptFunc)
        {
            CacheLicense = license;
            CoroutineHandler = coroutine;
            ModelDecryptFunc = decryptFunc;
            TaskQueue = taskQueue;
        }

        /// <summary>
        /// キャッシュからモデルをロードする
        /// </summary>
        public override void Load()
        {
            CoroutineHandler.RunMonoCoroutine(LoadAsyncCachedBinary(CacheLicense));
            CacheLicense.UpdateLastAccessTime();
            CacheLicense.Save();
        }

        private IEnumerator LoadAsyncCachedBinary(CachedLicense cachedCharacter)
        {
            byte[] binary = new byte[] { };
            TaskQueue.Enqueue(() =>
            {
                binary = LoadCachedBinary(cachedCharacter);
            });

            yield return WaitFor();

            if (OnProgress != null)
            {
                OnProgress(1.0f);
            }

            LoadVRMFromBinary(binary);
        }

        private IEnumerator WaitFor()
        {
            while (TaskQueue.ExistQueueEvent)
            {
                yield return null;
            }
        }

        private byte[] LoadCachedBinary(CachedLicense cachedLicense)
        {
            if (LocalStorage.HasKey(cachedLicense.downloadLicense.character_model_id))
            {
                CachedLicense fileModel = LocalStorage.GetGenericObject<CachedLicense>(cachedLicense.downloadLicense.character_model_id);
                return ModelDecryptFunc(fileModel.filePath);
            }

            return new byte[0];
        }
    }
}