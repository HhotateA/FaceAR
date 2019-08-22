namespace VRoidSDK
{
    /// <summary>
    /// CachedLicenseに関する機能をまとめたマネージャクラス
    /// </summary>
    public class LicenseManager
    {
        /// <summary>
        /// Downloadしたライセンス情報をもとにLocalStorageに保存するキャッシュライセンスを作成する
        /// </summary>
        /// <param name="license">ダウンロードライセンス</param>
        /// <returns></returns>
        public static CachedLicense LicenseCache(DownloadLicense license)
        {
            return new CachedLicense(license);
        }

        /// <summary>
        /// LocalStorageからキャラクタモデルIDをもとにダウンロードライセンスを取得する
        /// </summary>
        /// <param name="characterModelId">キャラクタモデルID</param>
        /// <returns>キャッシュライセンス</returns>
        public static CachedLicense? LoadExistLicense(string characterModelId)
        {
            if (!LocalStorage.HasKey(characterModelId))
            {
                return null;
            }
            CachedLicense license = LocalStorage.GetGenericObject<CachedLicense>(characterModelId);
            if (license.downloadLicense.IsExpired())
            {
                return null;
            }
            return license;
        }
    }
}