using System;
using UnityEngine;
namespace VRoidSDK
{
    /// <summary>
    /// 状況に応じてモデルをロードするモジュールを作成するファクトリー
    /// </summary>
    /// <remarks>
    /// ファイルがすでにキャッシュ上に存在する場合は、キャッシュからロードするモジュールを作成し、
    /// 存在しない場合は、HubApiを実行してダウンロードしてからロードするモジュールを作成する
    /// </remarks>
    public static class ModelLoaderFactory
    {
        /// <summary>
        /// モデルをロードするモジュールを作成する
        /// </summary>
        /// <param name="characterModelId">ロードするキャラクターモデルID</param>
        /// <param name="coroutineHandler">コルーチンが実行できるハンドラオブジェクト</param>
        /// <param name="maxCacheCount">キャッシュの最大保持件数</param>
        /// <param name="onSuccess">成功時のコールバック関数</param>
        /// <param name="onError">失敗時のコールバック関数</param>
        public static void Create(string characterModelId, ICoroutineHandlable coroutineHandler, uint maxCacheCount, Action<IModelLoader> onSuccess, Action<ApiErrorFormat> onError)
        {
            CachedLicense? cachedLicense = LicenseManager.LoadExistLicense(characterModelId);
            if(cachedLicense != null)
            {
                onSuccess(new ModelCachedLoader(cachedLicense.GetValueOrDefault(),
                                                coroutineHandler,
                                                UnityThreadQueue.Instance,
                                                EncriptionModelFile.ReadBytes));
            } else {
                HubApi.PostDownloadLicense(characterModelId: characterModelId,
                                           onSuccess: (DownloadLicense license) => {
                                               var newCachedLicense = LicenseManager.LicenseCache(license);
                                               var saveModule = new EncryptModelSave(EncriptionModelFile.WriteBytes);
                                               // 新しいモデルデータが保存される前に処理するので-1する必要がある
                                               var cacheCount = maxCacheCount - 1;
                                               if (cacheCount > 0) {
                                                    var deletedLicenses = CachedLicense.CleanCache(cacheCount);
                                                    foreach (var lic in deletedLicenses) {
                                                        EncriptionModelFile.DeleteFile(lic.filePath);
                                                    }
                                               }
                                               onSuccess(new ModelDownloadLoader(newCachedLicense, saveModule, Authentication.Instance));
                                           },
                                           onError: onError);
            }
        }
    }
}