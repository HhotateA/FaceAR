using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VRoidSDK.Example
{
    public class CharacterListMenuCanvas : MonoBehaviour
    {
        [SerializeField] private CharacterLicensePanel characterLicensePanel;
        [SerializeField] private GridLayoutGroup allCharacterScrollRoot;
        [SerializeField] private GridLayoutGroup accountCharacterScrollRoot;
        [SerializeField] private GridLayoutGroup heartCharacterScrollRoot;
        [SerializeField] private GameObject charcterThumbnailImageObj;
        [SerializeField] private GameObject allSelectedTabImageObj;
        [SerializeField] private GameObject allNotSelectedTabImageObj;
        [SerializeField] private GameObject yoursSelectedTabImageObj;
        [SerializeField] private GameObject yoursNotSelectedTabImageObj;

        [SerializeField] private GameObject likedSelectedTabImageObj;
        [SerializeField] private GameObject likedNotSelectedTabImageObj;
        [SerializeField] private GameObject loadNextAllCharacterButton;
        [SerializeField] private GameObject loadNextAccountCharacterButton;
        [SerializeField] private GameObject loadNextHeartCharacterButton;
        [SerializeField] private RawImage accountIcon;
        [SerializeField] private int loadModelSize = 12; 

        private CharacterListMenuCanvasController Controller;

        void Awake()
        {
            var model = new CharacterListMenuCanvasModel();
            model.AllSelectedTabImage.Subscribe((result) =>
            {
                allSelectedTabImageObj.SetActive(result);
                allNotSelectedTabImageObj.SetActive(!result);
            });
            model.YoursSelectedTabImage.Subscribe((result) =>
            {
                yoursSelectedTabImageObj.SetActive(result);
                yoursNotSelectedTabImageObj.SetActive(!result);
            });
            model.LikedSelectedTabImage.Subscribe((result) =>
            {
                likedSelectedTabImageObj.SetActive(result);
                likedNotSelectedTabImageObj.SetActive(!result);
            });
            
            model.AllCharacterScrollRoot.Subscribe((result) => allCharacterScrollRoot.transform.parent.parent.parent.gameObject.SetActive(result));
            model.YoursCharacterScrollRoot.Subscribe((result) => accountCharacterScrollRoot.transform.parent.parent.parent.gameObject.SetActive(result));
            model.LikedCharacterScrollRoot.Subscribe((result) => heartCharacterScrollRoot.transform.parent.parent.parent.gameObject.SetActive(result));

            var allTab = new AllTab(loadModelSize);
            var yoursTab = new YoursTab(loadModelSize);
            var likedTab = new LikedTab(loadModelSize);
            
            allTab.LoadedTab.SubscribeAddCollection((result) => SetThumbnail(allCharacterScrollRoot, result));
            yoursTab.LoadedTab.SubscribeAddCollection((result) => SetThumbnail(accountCharacterScrollRoot, result));
            likedTab.LoadedTab.SubscribeAddCollection((result) => SetThumbnail(heartCharacterScrollRoot, result));
            
            allTab.NextLinkState.Subscribe((result) => loadNextAllCharacterButton.SetActive(result));
            yoursTab.NextLinkState.Subscribe((result) => loadNextAccountCharacterButton.SetActive(result));
            likedTab.NextLinkState.Subscribe((result) => loadNextHeartCharacterButton.SetActive(result));

            Controller = new CharacterListMenuCanvasController(model, allTab, yoursTab, likedTab);
        }

        public void Open()
        {
            if (accountIcon.texture == null)
            {
                HubApi.GetAccount(account => StartCoroutine(LoadUtil.LoadThumbnail(account.user_detail.user.icon.sq50, accountIcon)), null);
            }
            gameObject.SetActive(true);
            OnAllTabClicked();
        }

        public void OnLogOut()
        {
            accountIcon.texture = null;

            ComponentUtil.DeleteAllChildren(allCharacterScrollRoot.transform);
            ComponentUtil.DeleteAllChildren(accountCharacterScrollRoot.transform);
            ComponentUtil.DeleteAllChildren(heartCharacterScrollRoot.transform);

            if (Controller != null)
            {
                Controller.CleanTab();
            }
        }

        public void ReloadList()
        {
            ComponentUtil.DeleteAllChildren(allCharacterScrollRoot.transform);
            ComponentUtil.DeleteAllChildren(accountCharacterScrollRoot.transform);
            ComponentUtil.DeleteAllChildren(heartCharacterScrollRoot.transform);
            Controller.CleanTab();
            Controller.LoadActiveTab();
        }

        private void OnSelectCharacterModel(CharacterThumbnailImage thumbnailImage)
        {
            var children = transform.Cast<Transform>().ToList();
            children.ForEach(c => c.gameObject.SetActive(false));
            characterLicensePanel.Init(thumbnailImage.CharacterModel,
                    () => children.ForEach(c => c.gameObject.SetActive(true)),
                    () => children.ForEach(c => c.gameObject.SetActive(true))
            );
        }

        public void OnLoadNextAccountCharactersClicked()
        {
            Controller.LoadActiveNextLink();
        }

        public void OnLoadNextHeartCharactersClicked()
        {
            Controller.LoadActiveNextLink();
        }

        public void OnLoadAllAccountCharactersClicked()
        {
            Controller.LoadActiveNextLink();
        }

        public void OnAllTabClicked()
        {
            Controller.SetAllTab();
            StartThumbnailLoad(allCharacterScrollRoot);
        }

        public void OnYoursTabClicked()
        {
            Controller.SetYoursTab();
            StartThumbnailLoad(accountCharacterScrollRoot);
        }

        public void OnLikedTabClicked()
        {
            Controller.SetLikedTab();
            StartThumbnailLoad(heartCharacterScrollRoot);
        }
        
        private void SetThumbnail(GridLayoutGroup scrollRoot, CharacterModel characterModel)
        {
            ComponentUtil.InstantiateTo<CharacterThumbnailImage>(scrollRoot.gameObject, charcterThumbnailImageObj)
                .Init(characterModel, OnSelectCharacterModel);
        }
        
        private void StartThumbnailLoad(GridLayoutGroup list)
        {
            foreach (Transform child in list.transform)
            {
                child.GetComponent<CharacterThumbnailImage>().LoadImageIfNotYet();
            }
        }
    }
}