using System.Collections.Generic;

namespace VRoidSDK.Example
{
    public class CharacterListMenuCanvasModel
    {
        public ReactiveBoolProperty AllSelectedTabImage { get; private set; }
        public ReactiveBoolProperty YoursSelectedTabImage { get; private set; }
        public ReactiveBoolProperty LikedSelectedTabImage { get; private set; }

        public ReactiveBoolProperty AllCharacterScrollRoot { get; private set; }
        public ReactiveBoolProperty YoursCharacterScrollRoot { get; private set; }
        public ReactiveBoolProperty LikedCharacterScrollRoot { get; private set; }

        public CharacterListMenuCanvasModel()
        {
            AllSelectedTabImage = new ReactiveBoolProperty(false);
            YoursSelectedTabImage = new ReactiveBoolProperty(false);
            LikedSelectedTabImage = new ReactiveBoolProperty(false);
            AllCharacterScrollRoot = new ReactiveBoolProperty(false);
            YoursCharacterScrollRoot = new ReactiveBoolProperty(false);
            LikedCharacterScrollRoot = new ReactiveBoolProperty(false);
        }

        public void ShowAllTab()
        {
            AllSelectedTabImage.Set(true);
            YoursSelectedTabImage.Set(false);
            LikedSelectedTabImage.Set(false);
            
            AllCharacterScrollRoot.Set(true);
            YoursCharacterScrollRoot.Set(false);
            LikedCharacterScrollRoot.Set(false);
        }

        public void ShowYoursTab()
        {
            AllSelectedTabImage.Set(false);
            YoursSelectedTabImage.Set(true);
            LikedSelectedTabImage.Set(false);
            
            AllCharacterScrollRoot.Set(false);
            YoursCharacterScrollRoot.Set(true);
            LikedCharacterScrollRoot.Set(false);
        }

        public void ShowLikedTab()
        {
            AllSelectedTabImage.Set(false);
            YoursSelectedTabImage.Set(false);
            LikedSelectedTabImage.Set(true);

            AllCharacterScrollRoot.Set(false);
            YoursCharacterScrollRoot.Set(false);
            LikedCharacterScrollRoot.Set(true);
        }
    }
}