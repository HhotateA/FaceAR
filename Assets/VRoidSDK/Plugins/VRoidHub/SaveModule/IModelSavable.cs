using UnityEngine.Networking;
namespace VRoidSDK
{
    /// <summary>
    /// キャラクターモデルの情報をLocalStorageに保存する手法を提供するインターフェース
    /// </summary>
    public interface IModelSavable
    {
        /// <summary>
        /// モデルデータをLocalStorageに保存する
        /// </summary>
        /// <param name="license">保存するキャッシュライセンス</param>
        /// <param name="downloadedData">保存するバイナリデータ</param>
        void Save(CachedLicense license, byte[] downloadedData);
    }
}