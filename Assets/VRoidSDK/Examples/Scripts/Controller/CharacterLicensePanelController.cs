using System;
using UnityEngine;

namespace VRoidSDK.Example
{
    public class CharacterLicensePanelController
    {
        private CharacterLicenseModel LicenseModel;
        private CharacterLicensePanelModel PanelModel;
        private ProgressBarModel ProgressBarModel;

        public CharacterLicensePanelController(CharacterLicenseModel licenseModel, CharacterLicensePanelModel panelModel,
            ProgressBarModel progressBar)
        {
            LicenseModel = licenseModel;
            PanelModel = panelModel;
            ProgressBarModel = progressBar;
        }

        public void Init()
        {
            PanelModel.ShowAcceptButton();
        }

        public void UpdateView(CharacterModel model)
        {
            LicenseModel.SetLicenseInfo(model);
        }

        public void ShowAcceptButton()
        {
            PanelModel.HideLoadingAreaButtons();
            PanelModel.ShowAcceptButton();
        }

        public void ShowRetryButton()
        {
            PanelModel.ShowRetryButton();
        }

        public void UpdateDownloadProgress(float progress)
        {
            ProgressBarModel.UpdateProgress(progress);
        }

        public void ShowLoadingMessage()
        {
            PanelModel.HideLoadingAreaButtons();
            ProgressBarModel.ProgressBarActive.Set(false);
            PanelModel.LoadingMessageText.Set(true);
        }

        public void ShowLoadFinish()
        {
            PanelModel.ShowLoadingAreaButtons();
            PanelModel.LoadingMessageText.Set(false);
        }

        public void LoadCharacterAsync(CharacterModel model, Action<GameObject> onSuccess, Action<float> onDownloadProgress, Action<Exception> onError)
        {
            HubModelDeserializer.Instance.LoadCharacterAsync(
                characterModelId: model.id,
                onLoadComplete: onSuccess,
                onDownloadProgress: onDownloadProgress,
                onError: onError
            );
        }

        public void ResetLicenseText()
        {
            LicenseModel.Reset();
        }
    }
}