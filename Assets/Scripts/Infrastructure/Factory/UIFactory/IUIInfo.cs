using UnityEngine;

namespace KasherOriginal.Factories.UIFactory
{
    public interface IUIInfo
    {
        public GameObject LoadingGameScreen { get; }
        public GameObject MainMenuScreen { get; }
    }
}

