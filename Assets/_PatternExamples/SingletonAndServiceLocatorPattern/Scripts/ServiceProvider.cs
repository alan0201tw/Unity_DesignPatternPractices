using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesignPatternExample.SingletonAndServiceLocatorPattern
{
    public class ServiceProvider : MonoBehaviour, ISceneServiceProvider
    {
        void Awake()
        {
            GameServices.Instance.ProvideSceneService(this);
            // For a service like this, you can make it a constant one, you don't need a new one for
            // each scene. For a MonoBehavior to work that way, you need to call DontDestroyOnLoad.
            // But that makes scene closes less cleanly, so you might want to avoid this in actual
            // developing.

            // Instead, make a dummy GameObject or MonoBehavior whenever you need one.
            // instantiating one dummy only when you need to load a scene doesn't hurt too much.
            // But if you need to call many coroutines by non-MonoBehavior script, try make a
            // dummy MonoBehavior pool.

            // Ex : a dummy component used only for running coroutines, and a single gameObject that
            // has multiple instances of that component

            DontDestroyOnLoad(this);
        }

        public void LoadScene(int sceneIndex, Action onLoadComplete = null)
        {
            SceneManager.LoadScene(sceneIndex);

            if (onLoadComplete != null)
                onLoadComplete.Invoke();
        }

        public void LoadSceneAsync(int sceneIndex, Action onLoadComplete = null)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneIndex, onLoadComplete));
        }

        private IEnumerator LoadSceneAsyncCoroutine(int sceneIndex, Action onLoadComplete)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            
            // when the scene completes loading itself to memory
            // it still needs to call Awake and Start on all objects in that scene
            // this process cannot be cut and must be done in one cycle
            // so LoadAsync still yields a stall, but shorter.
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            if (onLoadComplete != null)
                onLoadComplete.Invoke();
        }
    }
}