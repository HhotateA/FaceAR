namespace VRoidSDK.Example
{
    public class CharacterLicenseModel
    {
        public ReactiveStringProperty CharacterInfoText { get; private set; }
        
        public CanUseAvatarProperty CanUseAvatarProperty { get; private set; }
        public CanUseViolenceProperty CanUseViolenceProperty { get; private set; }
        public CanUseSexuality CanUseSexualityProperty { get; private set; }
        public CanUseCorporateCommercialProperty CanUseCorporateCommercialProperty { get; private set; }
        public CanUsePersonalCommercialProperty CanUsePersonalCommercialProperty { get; private set; }
        public CanModifyProperty CanModifyProperty { get; private set; }
        public CanRedistributeProperty CanRedistributeProperty { get; private set; }
        public ShowCreditProperty ShowCreditProperty { get; private set; }
        
        public CharacterLicenseModel(string canUseAvatar,
                                     string canUseViolence,
                                     string canUseSexuality,
                                     string canUseCorporateCommercial,
                                     string canUsePersonalCommercial,
                                     string canModify,
                                     string canRedistribute,
                                     string showCredit)
        {
            CharacterInfoText = new ReactiveStringProperty("");

            CanUseAvatarProperty = new CanUseAvatarProperty(canUseAvatar);
            CanUseViolenceProperty = new CanUseViolenceProperty(canUseViolence);
            CanUseSexualityProperty = new CanUseSexuality(canUseSexuality);
            CanUseCorporateCommercialProperty = new CanUseCorporateCommercialProperty(canUseCorporateCommercial);
            CanUsePersonalCommercialProperty = new CanUsePersonalCommercialProperty(canUsePersonalCommercial);
            CanModifyProperty = new CanModifyProperty(canModify);
            CanRedistributeProperty = new CanRedistributeProperty(canRedistribute);
            ShowCreditProperty = new ShowCreditProperty(showCredit);
        }

        public void SetLicenseInfo(CharacterModel model)
        {
            CharacterInfoText.Set(MakeInfoText(model));
            var license = model.license;

            CanUseAvatarProperty.UpdateLicenseText(license);
            CanUseViolenceProperty.UpdateLicenseText(license);
            CanUseSexualityProperty.UpdateLicenseText(license);
            CanUseCorporateCommercialProperty.UpdateLicenseText(license);
            CanUsePersonalCommercialProperty.UpdateLicenseText(license);
            CanModifyProperty.UpdateLicenseText(license);
            CanRedistributeProperty.UpdateLicenseText(license);
            ShowCreditProperty.UpdateLicenseText(license);
        }

        public void Reset()
        {
            CanUseAvatarProperty.Reset();
            CanUseViolenceProperty.Reset();
            CanUseSexualityProperty.Reset();
            CanUseCorporateCommercialProperty.Reset();
            CanUsePersonalCommercialProperty.Reset();
            CanModifyProperty.Reset();
            CanRedistributeProperty.Reset();
            ShowCreditProperty.Reset();
        }

        private string MakeInfoText(CharacterModel model)
        {
            return string.Format("<b><size=28>{0}</size></b>\n" +
                                 "<size=24>{1}</size>\n" +
                                 "<size=24>作者：<b>{2}</b></size>",
                                 model.character.name,
                                 model.name,
                                 model.character.user.name);
        }
    }
}