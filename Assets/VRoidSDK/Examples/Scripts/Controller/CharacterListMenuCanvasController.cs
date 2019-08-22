namespace VRoidSDK.Example
{
    public class CharacterListMenuCanvasController
    {
        private CharacterListMenuCanvasModel Model;
        private ITabModels ActiveTab;
        private AllTab AllTab;
        private YoursTab YoursTab;
        private LikedTab LikedTab;
        
        public CharacterListMenuCanvasController(CharacterListMenuCanvasModel model, AllTab all, YoursTab yours, LikedTab liked)
        {
            Model = model;
            AllTab = all;
            YoursTab = yours;
            LikedTab = liked;
        }

        public void SetAllTab()
        {
            ActiveTab = AllTab;
            ActiveTab.SendRequest();
            Model.ShowAllTab();
        }

        public void SetYoursTab()
        {
            ActiveTab = YoursTab;
            ActiveTab.SendRequest();
            Model.ShowYoursTab();
        }

        public void SetLikedTab()
        {
            ActiveTab = LikedTab;
            ActiveTab.SendRequest();
            Model.ShowLikedTab();
        }

        public void LoadActiveTab()
        {
            ActiveTab.SendRequest();
        }
        
        public void CleanTab()
        {
            AllTab.Clear();
            YoursTab.Clear();
            LikedTab.Clear();
        }
        
        public void LoadActiveNextLink()
        {
            ActiveTab.Next();
        }
    }
}