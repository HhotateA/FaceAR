namespace VRoidSDK.Example
{
    public class CanUseSexuality : LicensePropertyBase
    {
        public ReactiveStringProperty CanUseSexualityText { get; private set; }
        private string DefaultValue;
        public CanUseSexuality(string defaultValue)
        {
            CanUseSexualityText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.sexual_expression == "allow")
            {
                CanUseSexualityText.Set(OkText(DefaultValue));
            }
            else if (license.sexual_expression == "default")
            {
                CanUseSexualityText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanUseSexualityText.Set(NgText(DefaultValue));
            }
        }

        public override void Reset()
        {
            CanUseSexualityText.Set(DefaultValue);
        }
    }
}