using System;

namespace VRoidSDK.Example
{
    public class LoginCanvasController
    {
        private LoginCanvasModel Model;
        private VRoidHubController HubController;
        private SDKConfiguration Configuration;
        private BrowserAuthorize browserAuthorize;

        public LoginCanvasController(LoginCanvasModel model, VRoidHubController hubController, SDKConfiguration conf)
        {
            Model = model;
            HubController = hubController;
            Configuration = conf;
        }

        public void Init(Action<Account> onSuccess)
        {
            Model.HideMenuCanvas();

            Authentication.Instance.Init(Configuration.AuthenticateMetaData);

            if (!Authentication.Instance.IsAuthorized())
            {
                Model.UnSetAccountInformation();
                return;
            }

            HubApi.GetAccount((account) => {
                Model.SetAccountInformation(account);
                onSuccess(account);
            }, Model.Error);
        }

        public void OpenLoginMenu(Action<Account> onSuccess)
        {
            Model.OpenLoginMenu();
            
            if (!Model.IsLoggedIn.Get())
            {
                Init(onSuccess);
                return;
            }

            Model.MenuCanvasOpenState.Set(true);
        }

        public void OpenAccountMenu(Action<Account> onSuccess)
        {
            Model.OpenAccountMenu();
            if (!Model.IsLoggedIn.Get())
            {
                Init(onSuccess);
            }
        }

        public void ConnectHub(Action<Account> onBrowseAuthorizeSuccess)
        {
            Model.HideConnectButton();
            Authentication.Instance.AuthorizeWithExistAccount(isAuthSuccess =>
            {
                if (isAuthSuccess)
                {
                    Model.Authorize();
                    return;
                }

                OpenBrowse(Configuration, onBrowseAuthorizeSuccess);
            }, Model.Error);
        }

        private void OpenBrowse(SDKConfiguration conf, Action<Account> onBrowseAuthorizeSuccess)
        {
            browserAuthorize = BrowserAuthorize.GenerateInstance(Configuration);
            Model.OpenCodeField();
            browserAuthorize.OpenBrowser(isSuccess => {
                if (isSuccess)
                {
                    Model.Authorize();
                    return;
                }

                OpenLoginMenu(onBrowseAuthorizeSuccess);
                Model.LoginFailureMessage.Set(true);
            });
        }

        public void BrowseRegisterCode(string code)
        {
            if (browserAuthorize != null)
            {
                browserAuthorize.RegisterCode(code);
            }
        }

        public void Logout()
        {
            Model.Logout();
        }

        public void Close()
        {
            switch (Model.PreviousView)
            {
                case PreviousView.Closed:
                    HubController.Cancel();
                    return;
                case PreviousView.Menu:
                    Model.MenuCanvasOpenState.Set(true);
                    return;
                default:
                    UnityEngine.Debug.LogError("Invalid View Mode");
                    break;  
            }
        }
    }
}