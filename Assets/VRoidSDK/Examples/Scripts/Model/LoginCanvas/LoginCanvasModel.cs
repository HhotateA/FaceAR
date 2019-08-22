namespace VRoidSDK.Example
{
    public enum PreviousView
    {
        Closed,
        Menu
    }

    public class LoginCanvasModel
    {
        public ReactiveBoolProperty CodeField { get; private set; }
        public ReactiveBoolProperty LoginButton { get; private set; }
        public ReactiveBoolProperty LogoutButton { get; private set; }
        public ReactiveBoolProperty LoginFailureMessage { get; private set; }
        public ReactiveBoolProperty IsLoggedIn { get; private set; }
        public ReactiveBoolProperty ConnectButton { get; private set; }
        public ReactiveBoolProperty ConnectionFailureMessage { get; private set; }
        public ReactiveBoolProperty AccountInfomation { get; private set; }
        public ReactiveStringProperty Username { get; private set; }
        public ReactiveBoolProperty MenuCanvas { get; private set; }
        public ReactiveBoolProperty MenuCanvasOpenState { get; set; }        
        
        public PreviousView PreviousView { get; private set; }

        public LoginCanvasModel()
        {
            CodeField = new ReactiveBoolProperty(false);
            ConnectButton = new ReactiveBoolProperty(false);
            LoginButton = new ReactiveBoolProperty(false);
            LogoutButton = new ReactiveBoolProperty(false);
            LoginFailureMessage = new ReactiveBoolProperty(false);
            ConnectionFailureMessage = new ReactiveBoolProperty(false);
            MenuCanvas = new ReactiveBoolProperty(false);
            AccountInfomation = new ReactiveBoolProperty(false);
            Username = new ReactiveStringProperty("");
            MenuCanvasOpenState = new ReactiveBoolProperty(false);
            IsLoggedIn = new ReactiveBoolProperty(false);
        }

        public void Error<T>(T error)
        {
           AccountInfomation.Set(false);
           LoginFailureMessage.Set(true);
           ConnectionFailureMessage.Set(true);
           ConnectButton.Set(true);
           LogoutButton.Set(true);
           LoginButton.Set(true);
        }

        public void SetAccountInformation(Account account)
        {
            Username.Set(account.user_detail.user.name);
            AccountInfomation.Set(true);
            IsLoggedIn.Set(true);
        }
        
        public void UnSetAccountInformation()
        {
            AccountInfomation.Set(false);
            LogoutButton.Set(false);
        }

        public void OpenLoginMenu()
        {
            PreviousView = PreviousView.Closed;
            
            ConnectButton.Set(true);
            LogoutButton.Set(false);
            
            AccountInfomation.Set(false);
            Open();
        }

        public void OpenAccountMenu()
        {
            PreviousView = PreviousView.Menu;
            
            ConnectButton.Set(false);
            LogoutButton.Set(true);
            
            AccountInfomation.Set(true);
            Open();
        }
        
        private void Open()
        {
            MenuCanvas.Set(false);
            CodeField.Set(false);
            LoginButton.Set(false);
            ConnectionFailureMessage.Set(false);
            LoginFailureMessage.Set(false);
        }

        public void OpenCodeField()
        {
            ConnectButton.Set(false);
            LoginButton.Set(true);
            CodeField.Set(true);
        }

        public void HideMenuCanvas()
        {
            CodeField.Set(false);
            MenuCanvas.Set(false);  
        }

        public void HideConnectButton()
        {
            LoginFailureMessage.Set(false);
            ConnectButton.Set(false);
        }

        public void Authorize()
        {
            IsLoggedIn.Set(true);
            MenuCanvasOpenState.Set(true);
        }

        public void Logout()
        {
            Authentication.Instance.Logout();
            IsLoggedIn.Set(false);
        }
    }
}