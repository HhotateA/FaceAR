using System.Collections.Generic;

namespace VRoidSDK.Example
{
    public class YoursTab : TabModelsList
    {
        public ReactiveBoolProperty NextLinkState { get; private set; }
        private ApiLink? NextLink;
        private int LoadModelSize;
        
        public YoursTab(int loadModelSize)
        {
            LoadModelSize = loadModelSize;
            NextLinkState = new ReactiveBoolProperty(false);
        }
        
        public override void SendRequest()
        {
            if (IsCached) return;

            HubApi.GetAccountCharacterModels(
                count: LoadModelSize,
                onSuccess: OnGetSuccess,
                onError: OnError
            );
        }
        
        public override void Next()
        {
            if (NextLink != null)
            {
                NextLinkState.Set(false);
                NextLink.Value.RequestLink<List<CharacterModel>>(
                    onSuccess: OnGetSuccess,
                    onError: OnError
                );
            }
        }

        public void OnGetSuccess(List<CharacterModel> characterModels, ApiLinksFormat links)
        {
            AddRange(characterModels);
            NextLink = links.next;
            NextLinkState.Set(links.next != null);
        }

        public void OnError(ApiErrorFormat error)
        {
            NextLinkState.Set(true);
            UnityEngine.Debug.LogError(error.message);
        }
    }
}