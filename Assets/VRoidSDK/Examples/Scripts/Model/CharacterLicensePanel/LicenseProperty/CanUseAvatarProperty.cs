namespace VRoidSDK.Example
{
    public class CanUseAvatarProperty : LicensePropertyBase
    {
        public ReactiveStringProperty CanUseAvatarText { get; private set; }
        private string DefaultValue;

        public CanUseAvatarProperty(string defaultValue)
        {
            CanUseAvatarText = new ReactiveStringProperty("");
            DefaultValue = defaultValue;
        }

        public override void UpdateLicenseText(CharacterLicense license)
        {
            if (license.characterization_allowed_user == "everyone")
            {
                CanUseAvatarText.Set(OkText(DefaultValue));
            }
            else if (license.characterization_allowed_user == "default")
            {
                CanUseAvatarText.Set(NotSetText(DefaultValue));
            }
            else
            {
                CanUseAvatarText.Set(NgText(DefaultValue));
            }
        }

        public override void Reset()
        {
            CanUseAvatarText.Set(DefaultValue);
        }
    }
}