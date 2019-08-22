using System;

namespace VRoidSDK
{
    /// <summary>
    /// キャラクターのバージョン
    /// </summary>
    public struct CharacterModelVersion
    {
        /// <summary>
        /// バージョンID
        /// </summary>
        public string id;

        /// <summary>
        /// 作成日時
        /// </summary>
        public string created_at;

        /// <summary>
        /// VRMのspecVersion
        /// </summary>
        /// <remarks>
        /// UniVRM v0.50で追加された項目
        /// 0.50未満のUniVRMで作られたモデルの場合nullになる
        /// </remarks>
        public string spec_version;

        /// <summary>
        /// 三角ポリゴンの数
        /// </summary>
        public int triangle_count;

        /// <summary>
        /// メッシュ数
        /// </summary>
        public int mesh_count;

        /// <summary>
        /// サブメッシュ数
        /// </summary>
        public int mesh_primitive_count;

        /// <summary>
        /// モーフ数
        /// </summary>
        public int mesh_primitive_morph_count;

        /// <summary>
        /// ジョイント数
        /// </summary>
        public int joint_count;

        /// <summary>
        /// マテリアル数
        /// </summary>
        public int material_count;

        /// <summary>
        /// テクスチャ数
        /// </summary>
        public int texture_count;

        /// <summary>
        /// VRMファイルの容量
        /// </summary>
        public int? original_file_size;

        /// <summary>
        /// VRMファイルの圧縮容量(通信上でのサイズ)
        /// </summary>
        public int? original_file_compressed_file_size;

        /// <summary>
        /// 作成日時を取得する
        /// </summary>
        /// <returns>作成日時</returns>
        public DateTime? CreatedAt()
        {
            if (string.IsNullOrEmpty(created_at))
            {
                return null;
            }
            return DateTime.Parse(created_at);
        }
    }
}
