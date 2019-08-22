namespace VRoidSDK.Example
{
    public class CharacterLicensePanelModel
    {
        public ReactiveBoolProperty AcceptButtonRow { get; private set; }
        public ReactiveBoolProperty CancelButtonRow { get; private set; }
        public ReactiveBoolProperty AcceptButton { get; private set; }
        public ReactiveBoolProperty RetryButton { get; private set; }
        public ReactiveBoolProperty LoadingMessageText { get; private set; }
        
        public CharacterLicensePanelModel()
        {
            AcceptButtonRow = new ReactiveBoolProperty(false);
            CancelButtonRow = new ReactiveBoolProperty(false);
            AcceptButton = new ReactiveBoolProperty(false);
            RetryButton = new ReactiveBoolProperty(false);
            LoadingMessageText = new ReactiveBoolProperty(false);
        }
        
        public void ShowLoadingAreaButtons()
        {
            AcceptButtonRow.Set(true);
            CancelButtonRow.Set(true);
        }

        public void HideLoadingAreaButtons()
        {
            AcceptButtonRow.Set(false);
            CancelButtonRow.Set(false);
        }

        public void ShowAcceptButton()
        {
            AcceptButton.Set(true);
            RetryButton.Set(false);
        }

        public void ShowRetryButton()
        {
            AcceptButton.Set(false);
            RetryButton.Set(true);  
        }
    }
}