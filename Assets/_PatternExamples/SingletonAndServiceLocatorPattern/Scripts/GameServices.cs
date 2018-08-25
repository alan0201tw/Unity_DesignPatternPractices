using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.SingletonAndServiceLocatorPattern
{
    public interface ISceneServiceProvider
    {
        void LoadScene(int sceneIndex, Action onLoadComplete = null);
        void LoadSceneAsync(int sceneIndex, Action onLoadComplete = null);
    }

    class NullSceneServiceProvider : ISceneServiceProvider
    {
        public void LoadScene(int sceneIndex, Action onLoadComplete = null)
        {
            Debug.LogError("NullSceneServiceProvider : LoadScene is called for sceneIndex = " + sceneIndex);
        }

        public void LoadSceneAsync(int sceneIndex, Action onLoadComplete = null)
        {
            Debug.LogError("NullSceneServiceProvider : LoadSceneAsync is called for sceneIndex = " + sceneIndex);
        }
    }

    public class GameServices
    {
        private static GameServices instance = new GameServices();
        public static GameServices Instance
        {
            get { return instance; }
        }

        // set the constructor to private, so no other instances can be created
        private GameServices() { }

        private ISceneServiceProvider m_sceneServiceProvider = new NullSceneServiceProvider();

        public ISceneServiceProvider GetSceneService()
        {
            return m_sceneServiceProvider;
        }

        public void ProvideSceneService(ISceneServiceProvider sceneServiceProvider)
        {
            // handle null reference error
            if (sceneServiceProvider == null)
            {
                Debug.LogError("GameServices : Someone calls ProvideSceneService while serviceProvider is null");
                return;
            }
            // correctly assign service provider
            m_sceneServiceProvider = sceneServiceProvider;
        }
    }
}