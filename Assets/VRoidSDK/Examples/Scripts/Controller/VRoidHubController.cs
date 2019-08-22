using System;
using UnityEngine;

namespace VRoidSDK.Example
{
    public class VRoidHubController : MonoBehaviour
    {
        [SerializeField] private LoginCanvas loginCanvas;

        private Action<string, GameObject> OnLoad;
        private Action OnCancel;

        public void Cancel()
        {
            Close();
            if (OnCancel != null) 
            {
                OnCancel();
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            loginCanvas.OpenLoginMenu();
        }

        public void SetOnLoadHandler(Action<string, GameObject> handler)
        {
            OnLoad = handler;
        }

        public void SetOnCancelHandler(Action handler)
        {
            OnCancel = handler;
        }

        public void OnLoadModel(string characterModelId, GameObject vrmModel)
        {
            if (OnLoad != null)
            {
                OnLoad(characterModelId, vrmModel);
            }
        }
    }
}
