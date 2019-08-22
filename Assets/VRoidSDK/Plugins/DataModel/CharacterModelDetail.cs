using System;
using System.Collections.Generic;

namespace VRoidSDK
{
    /// <summary>
    /// キャラクターモデル詳細
    /// </summary>
    public struct CharacterModelDetail
    {
        /// <summary>
        /// 紐づくキャラクターモデル
        /// </summary>
        public CharacterModel character_model;
        
        /// <summary>
        /// モデルの説明
        /// </summary>
        public string description;
        
        /// <summary>
        /// モデル説明を分割した情報
        /// </summary>
        /// <remarks>
        /// 説明文中に、URLとplainテキストが混じっている場合、分割されて格納される
        /// </remarks>
        public List<DescriptionFragment> description_fragments;
        
        /// <summary>
        /// モデルに紐づくキャラクターの詳細情報
        /// </summary>
        public CharacterDetail character_detail;
    }
}
