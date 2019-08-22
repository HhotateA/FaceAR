using System.Collections.Generic;
using System.Linq;

namespace VRoidSDK.Example
{
    public abstract class TabModelsList : ITabModels
    {
        public ReactiveListProperty<CharacterModel> LoadedTab { get; private set; }
        public bool IsCached { get; private set; }

        public TabModelsList()
        {
            LoadedTab = new ReactiveListProperty<CharacterModel>();
            IsCached = false;
        }

        public void AddRange(List<CharacterModel> list)
        {
            foreach (var i in list)
            {
                LoadedTab.Add(i);
            }

            IsCached = true;
        }

        public void Clear()
        {
            for (int i = LoadedTab.Count() - 1; i >= 0; i--)
            {
                LoadedTab.RemoveAt(i);
            }

            IsCached = false;
        }
        
        public abstract void SendRequest();
        public abstract void Next();
    }
}