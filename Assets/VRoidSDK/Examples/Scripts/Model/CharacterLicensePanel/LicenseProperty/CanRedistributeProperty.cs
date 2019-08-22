namespace VRoidSDK.Example
{
    public class CanRedistributeProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanRedistributeText { get; private set; }
        private string DefaultValue;

        public CanRedistributeProperty(string defaultValue)
        {
            CanRedistributeText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }
        
        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.redistribution == "allow")
            {
                CanRedistributeText.Set(OkText(DefaultValue));
            }
            else if (license.redistribution == "default")
            {
                CanRedistributeText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanRedistributeText.Set(NgText(DefaultValue));
            }

        }

        public override void Reset()
        {
            CanRedistributeText.Set(DefaultValue);
        }
    }
}