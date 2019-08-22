using System;
using UnityEngine.Networking;

namespace VRoidSDK
{
    /// <summary>
    /// 暗号化してライセンスをローカルストレージに保存する
    /// </summary>
    public class EncryptModelSave : IModelSavable
    {
        private Action<string, byte[]> encryptFunc;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">モデルファイルの暗号化を行う関数
        /// </param>
        public EncryptModelSave(Action<string, byte[]> f)
        {
            encryptFunc = f;
        }

        /// <summary>
        /// モデルデータをLocalStorageに保存する
        /// </summary>
        /// <param name="license">保存するキャッシュライセンス</param>
        /// <param name="downloadedData">保存するバイナリデータ</param>
        public void Save(CachedLicense license, byte[] downloadedData)
        {
            SaveEncryptedModelFile(license, downloadedData);
            license.Save();
        }

        private void SaveEncryptedModelFile(CachedLicense cachedLicense, byte[] characterBinary)
        {
            if(LocalStorage.HasKey(cachedLicense.downloadLicense.character_model_id))
            {
                CachedLicense before = LocalStorage.GetGenericObject<CachedLicense>(cachedLicense.downloadLicense.character_model_id);
                EncriptionModelFile.DeleteFile(before.filePath);
            }
            encryptFunc(cachedLicense.filePath, characterBinary);
        }
    }
}