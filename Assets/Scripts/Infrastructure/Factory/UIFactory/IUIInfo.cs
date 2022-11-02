using UnityEngine;

namespace KasherOriginal.Factories.UIFactory
{
    public interface IUIInfo
    {
        public GameObject LoadingGameScreen { get; }
        public GameObject MainMenuScreen { get; }
        public GameObject GameplayScreen { get; }
        public GameObject GameLoseScreen { get; }
    }
}

