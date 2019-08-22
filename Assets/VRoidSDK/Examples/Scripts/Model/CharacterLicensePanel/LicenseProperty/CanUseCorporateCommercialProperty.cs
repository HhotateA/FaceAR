namespace VRoidSDK.Example
{
    public class CanUseCorporateCommercialProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanUseCorporateCommercialText { get; private set; }
        private string DefaultValue;
        public CanUseCorporateCommercialProperty(string defaultValue)
        {
            CanUseCorporateCommercialText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.corporate_commercial_use == "allow")
            {
                CanUseCorporateCommercialText.Set(OkText(DefaultValue));
            }
            else if (license.corporate_commercial_use == "default")
            {
                CanUseCorporateCommercialText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanUseCorporateCommercialText.Set(NgText(DefaultValue));
            }
        }

        public override void Reset()
        {
            CanUseCorporateCommercialText.Set(DefaultValue);
        }
    }
}