using UnityEngine;

using System;
using System.Collections;
using UnityEngine.UI;
using VRoidSDK;

namespace VRoidSDK.Example
{
    public class LoadUtil
    {
        public static IEnumerator LoadThumbnail(CharacterModel characterModel, RawImage rawImage)
        {
            return LoadThumbnail(characterModel.portrait_image.sq150, rawImage);
        }

        public static IEnumerator LoadThumbnail(WebImage image, RawImage rawImage)
        {
            WWW www = new WWW(image.url);
            yield return www;
            if (string.IsNullOrEmpty(www.error) && www.texture != null)
            {
                rawImage.color = Color.white;
                rawImage.texture = www.texture;
            }
        }
    }
}