using UnityEngine;

namespace VRoidSDK.Example
{
    public class MainSystem : MonoBehaviour
    {
        [SerializeField] private VRoidHubController controller;
        [SerializeField] private GameObject view;

        void Start()
        {
            controller.SetOnLoadHandler((characterModelId, vrmModel) =>
            {
                view.SetActive(true);
                ComponentUtil.DeleteAllChildren(transform);
                vrmModel.transform.parent = transform;
            });
            controller.SetOnCancelHandler(() => view.SetActive(true));
        }

        public void CloseCanvas()
        {
            view.SetActive(false);
        }
    }
}
