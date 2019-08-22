namespace VRoidSDK.Example
{
    public abstract class LicensePropertyBase : ILicenseProperty
    {
        public abstract void UpdateLicenseText(CharacterLicense license);
        public abstract void Reset();

        protected string OkText(string text)
        {
            return text + DecolateBoldColorText("OK", "#B1CC29");
        }

        protected string NgText(string text)
        {
            return text + DecolateBoldColorText("NG", "#ADADAD");
        }

        protected string NotSetText(string text)
        {
            return text + DecolateBoldColorText("未設定", "#ADADAD");
        }

        protected string NeedText(string text)
        {
            return text + DecolateBoldColorText("必要", "#FD3737");
        }

        protected string NoNeedText(string text)
        {
            return text + DecolateBoldColorText("不要", "#5C5C5C");
        }

        private string DecolateBoldColorText(string word, string colorKey)
        {
            return string.Format("<b><color={0}>{1}</color></b>", colorKey, word);
        }
    }
}