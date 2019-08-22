using System;
namespace VRoidSDK
{
    /// <summary>
    /// キャラクターの利用条件
    /// </summary>
    public struct CharacterLicense
    {
        /// <summary>
        /// 改変
        /// </summary>
        /// <remarks>
        /// <para>allow: 許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string modification;
        
        /// <summary>
        /// 再配布
        /// </summary>
        /// <remarks>
        /// <para>allow: 許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string redistribution;
        
        /// <summary>
        /// クレジット表記
        /// </summary>
        /// <remarks>
        /// <para>necessary: 必須</para>
        /// <para>unnecessary: 不要</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string credit;
        
        /// <summary>
        /// アバターとしての利用
        /// </summary>
        /// <remarks>
        /// <para>everyone: 全員に許可</para>
        /// <para>default: 未設定</para>
        /// <para>author: 作成者のみ</para>
        /// </remarks>
        public string characterization_allowed_user;
        
        /// <summary>
        /// 性的表現での利用
        /// </summary>
        /// <remarks>
        /// <para>allow: 許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string sexual_expression;
        
        /// <summary>
        /// 暴力表現での利用
        /// </summary>
        /// <remarks>
        /// <para>allow: 許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string violent_expression;
        
        /// <summary>
        /// 法人の商用利用
        /// </summary>
        /// <remarks>
        /// <para>allow: 許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string corporate_commercial_use;
        
        /// <summary>
        /// 営利目的での活動
        /// </summary>
        /// <remarks>
        /// <para>profit: 許可</para>
        /// <para>nonprofit: 非商用利用に限り許可</para>
        /// <para>disallow: 不可</para>
        /// <para>default: 未設定</para>
        /// </remarks>
        public string personal_commercial_use;
    }
}
