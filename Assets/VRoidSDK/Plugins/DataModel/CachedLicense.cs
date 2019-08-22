using System.Linq;
using System.Collections.Generic;
using System;

namespace VRoidSDK
{
    /// <summary>
    /// キャッシュしたダウンロードライセンス
    /// </summary>
    public struct CachedLicense
    {

        /// <summary>
        /// キャッシュしたダウンロードライセンス
        /// </summary>
        public DownloadLicense downloadLicense;

        /// <summary>
        /// ファイルの保存先
        /// </summary>
        public string filePath;

        /// <summary>
        /// 最後に利用した時刻
        /// </summary>
        public DateTime lastAccessTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="license">キャッシュするダウンロードライセンス</param>
        public CachedLicense(DownloadLicense license)
        {
            downloadLicense = license;
            filePath = string.Format("{0}_{1}", license.character_model_id, license.character_model_version_id);
            lastAccessTime = DateTime.Now.ToLocalTime();
        }

        /// <summary>
        /// キャッシュしているダウンロードライセンスと、他のダウンロードライセンスで同じモデルデータを使用しているか判定する
        /// </summary>
        /// <remarks>
        /// character_model_idとcharacter_model_versionがそれぞれ一致するか判定している
        /// </remarks>
        /// <param name="otherLicense">他のダウンロードライセンス</param>
        /// <returns>同一のモデルを使用しているか</returns>
        public bool IsSameModel(DownloadLicense otherLicense)
        {
            return downloadLicense.character_model_id == otherLicense.character_model_id &&
                   downloadLicense.character_model_version_id == otherLicense.character_model_version_id;
        }

        /// <summary>
        /// ローカルストレージ中にキャッシュ情報を保存する
        /// </summary>
        public void Save()
        {
            LocalStorage.SetValue(downloadLicense.character_model_id, this);
            LocalStorage.Save();
        }

        public void UpdateLastAccessTime() {
            lastAccessTime = DateTime.Now.ToLocalTime();
        }

        /// <summary>
        /// キャッシュ情報をクリアする
        /// </summary>
        public void  Clean() {
            LocalStorage.DeleteKey(this.downloadLicense.character_model_id);
        }

        /// <summary>
        /// 保存しているキャッシュ情報をmaxCacheCount件に減らす
        /// </summary>
        /// <param name="maxCacheCount">最大件数</param>
        /// <remarks>モデルのキャッシュ情報は削除しないので、戻り値を元に個別に削除する必要があります。</remarks>
        /// <returns>削除されたライセンス情報の一覧</returns>
        public static CachedLicense[] CleanCache(uint maxCacheCount)
        {
            var result = new List<CachedLicense>();
            var licenses = LocalStorage.GetGenericObjectArray<CachedLicense>();

            licenses = licenses.OrderBy((x) => x.lastAccessTime)
                               .Take((int)(licenses.Length - maxCacheCount))
                               .ToArray();

            foreach (var lic in licenses) {
                lic.Clean();
            }

            return licenses;
        }
    }
}