namespace VRoidSDK.Example
{
    public class CanModifyProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanModifyText { get; private set; }
        private string DefaultValue;

        public CanModifyProperty(string defaultValue)
        {
            CanModifyText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.modification == "allow")
            {
                CanModifyText.Set(OkText(DefaultValue));
            }
            else if (license.modification == "default")
            {
                CanModifyText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanModifyText.Set(NgText(DefaultValue));
            }
        }

        public override void Reset()
        {
            CanModifyText.Set(DefaultValue);
        }
    }
}