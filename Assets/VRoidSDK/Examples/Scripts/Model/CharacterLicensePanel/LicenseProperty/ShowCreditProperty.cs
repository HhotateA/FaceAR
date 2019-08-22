namespace VRoidSDK.Example
{
    public class ShowCreditProperty : LicensePropertyBase
    {
        public ReactiveStringProperty ShowCreditText { get; private set; }
        private string DefaultValue;

        public ShowCreditProperty(string defaultValue)
        {
            ShowCreditText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.credit == "necessary")
            {
                ShowCreditText.Set(NeedText(DefaultValue));
            }
            else if (license.credit == "default")
            {
                ShowCreditText.Set(NotSetText(DefaultValue));
            }
            else
            {
                ShowCreditText.Set(NoNeedText(DefaultValue));
            }
        }

        public override void Reset()
        {
            ShowCreditText.Set(DefaultValue);
        }
    }
}