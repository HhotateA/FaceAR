using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Example
{
    public class CharacterThumbnailImage : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        private Action<CharacterThumbnailImage> onSelect;

        public CharacterModel CharacterModel { get; private set; }

        public void Init(CharacterModel model, Action<CharacterThumbnailImage> selectCallback)
        {
            onSelect = selectCallback;
            CharacterModel = model;
            StartCoroutine(LoadUtil.LoadThumbnail(CharacterModel, rawImage));
        }

        public void OnSelect()
        {
            onSelect(this);
        }

        public void LoadImageIfNotYet()
        {
            if (!rawImage.texture)
            {
                StartCoroutine(LoadUtil.LoadThumbnail(CharacterModel, rawImage));
            }
        }
    }   

}
