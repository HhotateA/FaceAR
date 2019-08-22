namespace VRoidSDK
{
    /// <summary>
    /// モデルの説明情報
    /// </summary>
    public struct DescriptionFragment
    {
        /// <summary>
        /// 説明内容の型 (plain, url)
        /// </summary>
        public EnumDescriptionFragmentType type;
        
        /// <summary>
        /// 説明内容
        /// </summary>
        public string body;
        
        /// <summary>
        /// <para>文中のURLから`https://`が削られたURL</para>
        /// <para>plainテキストの場合はbodyと同じ</para>
        /// </summary>
        public string normalized_body;
    }
}