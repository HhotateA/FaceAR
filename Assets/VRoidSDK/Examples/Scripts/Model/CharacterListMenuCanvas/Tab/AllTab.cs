using System.Collections.Generic;

namespace VRoidSDK.Example
{
    public class AllTab : TabModelsList
    {
        public ReactiveBoolProperty NextLinkState { get; private set; }
        private int LoadModelSize;
        private ApiLink? AccountNextLink;
        private ApiLink? HeartNextLink;

        public AllTab(int loadModelSize)
        {
            LoadModelSize = loadModelSize;
            NextLinkState = new ReactiveBoolProperty(false);
        }
        
        public override void SendRequest()
        {
            if (IsCached) return;
            
            HubApi.GetAccountCharacterModels(
                count: LoadModelSize,
                onSuccess: OnGetAccountSuccess,
                onError: OnError
            );
            
            HubApi.GetHearts(
                count: LoadModelSize,
                onSuccess: OnGetHeartSuccess,
                onError: OnError
            );
        }

        public override void Next()
        {
            if (AccountNextLink != null)
            {
                NextLinkState.Set(false);
                AccountNextLink.Value.RequestLink<List<CharacterModel>>(
                    onSuccess: OnGetAccountSuccess,
                    onError: OnError
                );
            }

            if (HeartNextLink != null)
            {
                NextLinkState.Set(false);
                HeartNextLink.Value.RequestLink<List<CharacterModel>>(
                    onSuccess: OnGetHeartSuccess,
                    onError: OnError
                );
            }
        }

        public void OnGetAccountSuccess(List<CharacterModel> characterModels, ApiLinksFormat links)
        {
            AddRange(characterModels);
            AccountNextLink = links.next;
            NextLinkState.Set(HeartNextLink != null || AccountNextLink != null);            
        }
        
        public void OnGetHeartSuccess(List<CharacterModel> characterModels, ApiLinksFormat links)
        {
            AddRange(characterModels);
            HeartNextLink = links.next;
            NextLinkState.Set(HeartNextLink != null || AccountNextLink != null);   
        }

        public void OnError(ApiErrorFormat error)
        {
            NextLinkState.Set(true);
            UnityEngine.Debug.LogError(error.message);
        }
    }
}