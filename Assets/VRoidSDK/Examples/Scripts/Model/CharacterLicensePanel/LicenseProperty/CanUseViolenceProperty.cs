namespace VRoidSDK.Example
{
    public class CanUseViolenceProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanUseViolenceText { get; private set; }
        private string DefaultValue;
        public CanUseViolenceProperty(string defaultValue)
        {
            CanUseViolenceText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.violent_expression == "allow")
            {
                CanUseViolenceText.Set(OkText(DefaultValue));
            }
            else if (license.violent_expression == "default")
            {
                CanUseViolenceText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanUseViolenceText.Set(NgText(DefaultValue));
            }
        }

        public override void Reset()
        {
            CanUseViolenceText.Set(DefaultValue);
        }
    }
}