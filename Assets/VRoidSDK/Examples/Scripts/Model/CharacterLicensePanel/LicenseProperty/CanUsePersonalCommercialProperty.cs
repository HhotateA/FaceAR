namespace VRoidSDK.Example
{
    public class CanUsePersonalCommercialProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanUsePersonalCommercialText { get; private set; }
        public ReactiveBoolProperty CanUsePersonalCommercialProfitOk { get; private set; }
        public ReactiveBoolProperty CanUsePersonalCommercialNonProfitOk { get;private set; }
        private string DefaultValue;

        public CanUsePersonalCommercialProperty(string defaultValue)
        {
            CanUsePersonalCommercialText = new ReactiveStringProperty("");
            CanUsePersonalCommercialProfitOk = new ReactiveBoolProperty(false);
            CanUsePersonalCommercialNonProfitOk = new ReactiveBoolProperty(false);
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.personal_commercial_use == "profit")
            {
                CanUsePersonalCommercialText.Set(OkText(DefaultValue));
                CanUsePersonalCommercialProfitOk.Set(true);
                CanUsePersonalCommercialNonProfitOk.Set(false);
            }
            else if (license.personal_commercial_use == "default")
            {
                CanUsePersonalCommercialText.Set(NotSetText(DefaultValue));
                CanUsePersonalCommercialProfitOk.Set(false);
                CanUsePersonalCommercialNonProfitOk.Set(true);
            }
            else if (license.personal_commercial_use == "nonprofit")
            {
                CanUsePersonalCommercialText.Set(OkText(DefaultValue));
                CanUsePersonalCommercialProfitOk.Set(false);
                CanUsePersonalCommercialNonProfitOk.Set(true);
            }
            else
            {
                CanUsePersonalCommercialText.Set(NgText(DefaultValue));
                CanUsePersonalCommercialProfitOk.Set(false);
                CanUsePersonalCommercialNonProfitOk.Set(false);
            }
        }

        public override void Reset()
        {
            CanUsePersonalCommercialText.Set(DefaultValue);
            CanUsePersonalCommercialProfitOk.Set(false);
            CanUsePersonalCommercialNonProfitOk.Set(false);
        }
    }
}