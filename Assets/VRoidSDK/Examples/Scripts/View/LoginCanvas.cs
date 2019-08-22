using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Example
{
    public class LoginCanvas : MonoBehaviour
    {
        [SerializeField] private VRoidHubController vRoidHub;

        [SerializeField] private GameObject codeField;
        [SerializeField] private InputField codeInputField;
        [SerializeField] private SDKConfiguration sdkConfiguration;
        [SerializeField] private GameObject connectButton;
        [SerializeField] private GameObject loginButton;
        [SerializeField] private GameObject logoutButton;
        [SerializeField] private GameObject loginFailureMessage;
        [SerializeField] private GameObject connectionFailureMessage;
        [SerializeField] private CharacterListMenuCanvas menuCanvas;
        [SerializeField] private GameObject accountInfomation;
        [SerializeField] private RawImage userIcon;
        [SerializeField] private Text username;

        private LoginCanvasController loginCanvasController;

        void Awake()
        {
            var loginModel = new LoginCanvasModel();
            loginModel.CodeField.Subscribe((result) => codeField.SetActive(result));
            
            loginModel.ConnectButton.Subscribe((result) => connectButton.SetActive(result));
            loginModel.LoginButton.Subscribe((result) => loginButton.SetActive(result));
            loginModel.LogoutButton.Subscribe((result) => logoutButton.SetActive(result));
            loginModel.LoginFailureMessage.Subscribe((result) => loginFailureMessage.SetActive(result));
            loginModel.ConnectionFailureMessage.Subscribe((result) => connectionFailureMessage.SetActive(result));
            
            loginModel.AccountInfomation.Subscribe((result) => accountInfomation.SetActive(result));
            loginModel.Username.Subscribe((result) => username.text = result);
            loginModel.IsLoggedIn.Subscribe((result) =>
            {
                if (!result)
                {
                    menuCanvas.OnLogOut();
                    OpenLoginMenu();
                }
            });

            loginModel.MenuCanvas.Subscribe((result) => menuCanvas.gameObject.SetActive(result));
            loginModel.MenuCanvasOpenState.Subscribe((result) =>
            {
                if (result)
                {
                    gameObject.SetActive(false);
                    menuCanvas.Open();   
                }
            });

            loginCanvasController = new LoginCanvasController(loginModel, vRoidHub, sdkConfiguration);
        }

        public void OpenLoginMenu()
        {
            loginCanvasController.OpenLoginMenu(account => {
                StartCoroutine(LoadUtil.LoadThumbnail(account.user_detail.user.icon.sq170, userIcon));
            });
        }

        public void OpenAccountMenu()
        {
            loginCanvasController.OpenAccountMenu(account =>
            {
                StartCoroutine(LoadUtil.LoadThumbnail(account.user_detail.user.icon.sq170, userIcon));
            });
            gameObject.SetActive(true);
        }

        public void Init()
        {
            loginCanvasController.Init((account) => {
                StartCoroutine(LoadUtil.LoadThumbnail(account.user_detail.user.icon.sq170, userIcon));
            });
            gameObject.SetActive(true);
        }

        public void ConnectHub()
        {
            loginCanvasController.ConnectHub((account) => {
                StartCoroutine(LoadUtil.LoadThumbnail(account.user_detail.user.icon.sq170, userIcon));  
            });
        }

        public void OnLoginButtonClicked()
        {
            loginCanvasController.BrowseRegisterCode(codeInputField.text);
        }

        public void Logout()
        {
            loginCanvasController.Logout();
        }

        public void Close()
        {
            loginCanvasController.Close();
        }
    }
}