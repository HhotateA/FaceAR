using System;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;
using VRM;

namespace VRoidSDK
{
    /// <summary>
    /// VRoid Hubからキャラクターモデルをダウンロードしてモデルをロードする
    /// </summary>
    public class ModelDownloadLoader : ModelLoaderBase
    {
        private CachedLicense CacheLicense;
        private IModelSavable ModelSaveModule;
        private IAuthentication Authenticate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="license">キャッシュ化されたライセンスデータ</param>
        /// <param name="save">モデルを保存する機能を提供するモジュール</param>
        /// <param name="auth">認証機能を持ったモジュール</param>
        public ModelDownloadLoader(CachedLicense license, IModelSavable save, IAuthentication auth)
        {
            CacheLicense = license;
            ModelSaveModule = save;
            Authenticate = auth;
        }

        /// <summary>
        /// モデルをロードする
        /// </summary>
        public override void Load()
        {
            HubApi.GetDownloadLicenseDownload(CacheLicense.downloadLicense.id, OnDownloadSuccess, OnProgress, OnDownloadError);
        }

        private void OnDownloadSuccess(byte[] downloadBinary)
        {
            if(IsGzipBinary(downloadBinary))
            {
                downloadBinary = DecompressGzipBinary(downloadBinary);
            }
            var baseVrmLoadCallback = base.OnVrmModelLoaded;
            OnVrmModelLoaded = (context) => {
                ModelSaveModule.Save(CacheLicense, downloadBinary);
                baseVrmLoadCallback(context);
            };
            LoadVRMFromBinary(downloadBinary);
        }

        private void OnDownloadError(ApiErrorFormat error)
        {
            if (OnError != null)
            {
                OnError(new Exception(error.code + ": " + error.message));
            }
        }

        private bool IsGzipBinary(byte[] binary)
        {
            return binary.Length > 2 && binary[0] == 0x1F && binary[1] == 0x8B;
        }

        private byte[] DecompressGzipBinary(byte[] gzipBinary)
        {
            var buffer = new byte[4096];
            using (var memoryStream = new System.IO.MemoryStream(gzipBinary))
            using (var outputMemoryStream = new System.IO.MemoryStream())
            using (var gzStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                int bytesRead = -1;
                while ((bytesRead = gzStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputMemoryStream.Write(buffer, 0, bytesRead);
                }
                return outputMemoryStream.ToArray();
            }
        }
    }
}