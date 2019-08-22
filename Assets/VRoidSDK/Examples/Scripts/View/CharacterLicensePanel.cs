using System;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace VRoidSDK.Example
{
    public class CharacterLicensePanel : MonoBehaviour
    {
        [SerializeField] private VRoidHubController vRoidHubController;
        [SerializeField] private RawImage characterImage;
        [SerializeField] private Text canUseAvatarText;
        [SerializeField] private Text canUseViolenceText;
        [SerializeField] private Text canUseSexualityText;
        [SerializeField] private Text canUseCorporateCommercialText;
        [SerializeField] private Text canUsePersonalCommercialText;
        [SerializeField] private GameObject canUsePersonalCommercialProfit;
        [SerializeField] private GameObject canUsePersonalCommercialNonProfit;
        [SerializeField] private Text canModifiyText;
        [SerializeField] private Text canRedistributeText;
        [SerializeField] private Text showCreditText;
        [SerializeField] private Text characterInfo;
        [SerializeField] private GameObject acceptButtonRow;
        [SerializeField] private GameObject acceptButton;
        [SerializeField] private GameObject retryButton;
        [SerializeField] private GameObject cancelButtonRow;

        [SerializeField] private Slider progressSlider;
        [SerializeField] private Text loadingMessageText;

        private Action OnAccept;
        private Action OnCancel;
        private CharacterModel characterModel;
        private CharacterLicensePanelController Controller;
        
        void Awake()
        {
            var characterLicenseModel = new CharacterLicenseModel(canUseAvatarText.text,
                                                                  canUseViolenceText.text,
                                                                  canUseSexualityText.text,
                                                                  canUseCorporateCommercialText.text,
                                                                  canUsePersonalCommercialText.text,
                                                                  canModifiyText.text,
                                                                  canRedistributeText.text,
                                                                  showCreditText.text);
            characterLicenseModel.CharacterInfoText.Subscribe((result) => characterInfo.text = result);
            characterLicenseModel.CanUseAvatarProperty.CanUseAvatarText.Subscribe((result) => canUseAvatarText.text = result);
            characterLicenseModel.CanUseViolenceProperty.CanUseViolenceText.Subscribe((result) => canUseViolenceText.text = result);
            
            characterLicenseModel.CanUseSexualityProperty.CanUseSexualityText.Subscribe((result) => canUseSexualityText.text = result);
            characterLicenseModel.CanUseCorporateCommercialProperty.CanUseCorporateCommercialText.Subscribe((result) => canUseCorporateCommercialText.text = result);
            characterLicenseModel.CanUsePersonalCommercialProperty.CanUsePersonalCommercialText.Subscribe((result) => canUsePersonalCommercialText.text = result);
            characterLicenseModel.CanUsePersonalCommercialProperty.CanUsePersonalCommercialProfitOk.Subscribe((result) => canUsePersonalCommercialProfit.SetActive(result));
            characterLicenseModel.CanUsePersonalCommercialProperty.CanUsePersonalCommercialNonProfitOk.Subscribe((result) => canUsePersonalCommercialNonProfit.SetActive(result));
            characterLicenseModel.CanModifyProperty.CanModifyText.Subscribe((result) => canModifiyText.text = result);
            characterLicenseModel.CanRedistributeProperty.CanRedistributeText.Subscribe((result) => canRedistributeText.text = result);
            characterLicenseModel.ShowCreditProperty.ShowCreditText.Subscribe((result) => showCreditText.text = result);
            
            var characterLicensePanelModel = new CharacterLicensePanelModel();
            characterLicensePanelModel.AcceptButtonRow.Subscribe((result) => acceptButtonRow.SetActive(result));
            characterLicensePanelModel.CancelButtonRow.Subscribe((result) => cancelButtonRow.SetActive(result));
            characterLicensePanelModel.AcceptButton.Subscribe((result) => acceptButton.SetActive(result));
            characterLicensePanelModel.RetryButton.Subscribe((result) => retryButton.SetActive(result));
            characterLicensePanelModel.LoadingMessageText.Subscribe((result) => loadingMessageText.gameObject.SetActive(result));

            var progressBarModel = new ProgressBarModel();
            progressBarModel.ProgressBarActive.Subscribe((result) => progressSlider.gameObject.SetActive(result));
            progressBarModel.ProgressBarValue.Subscribe((result) => progressSlider.value = result);
            
            Controller = new CharacterLicensePanelController(characterLicenseModel, characterLicensePanelModel, progressBarModel);
        }

        public void Init(CharacterModel characterModel, Action onAccept = null, Action onCancel = null)
        {
            this.characterModel = characterModel;
            this.OnAccept = onAccept;
            this.OnCancel = onCancel;
            gameObject.SetActive(true);

            Controller.Init();

            StartCoroutine(LoadUtil.LoadThumbnail(characterModel, characterImage));
            this.UpdateView();
        }

        private void UpdateView()
        {
            Controller.UpdateView(characterModel);
        }

        public void OnAcceptButtonClicked()
        {
            Controller.ShowAcceptButton();
            Controller.LoadCharacterAsync(characterModel, (characterObj) => {
                OnLoadFinished();
                gameObject.SetActive(false);
                vRoidHubController.OnLoadModel(characterModel.id, characterObj);
            }, (progress) => {
                UpdateDownloadProgress(progress);
                if (progress >= 1.0f)
                {
                    OnLoading();
                }
            }, (error) => {
                Controller.ShowRetryButton();
                Debug.LogError(error.Message);
            });

            if (OnAccept != null)
            {
                OnAccept();
            }
        }

        public void OnCancelButtonClicked()
        {
            if (OnCancel != null)
            {
                OnCancel();
            }
            Close();
        }

        public void UpdateDownloadProgress(float progress)
        {
            Controller.UpdateDownloadProgress(progress);
        }

        public void OnLoading()
        {
            Controller.ShowLoadingMessage();
        }

        public void OnLoadFinished()
        {
            Controller.ShowLoadFinish();
            Close();
            vRoidHubController.Close();
        }

        private void Close()
        {
            characterImage.texture = null;
            Controller.ResetLicenseText();         
            gameObject.SetActive(false);
        }
    }
}