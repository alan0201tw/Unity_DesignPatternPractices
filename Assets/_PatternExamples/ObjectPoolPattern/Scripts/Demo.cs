using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.ObjectPoolPattern
{
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_clip;

        private ObjectPool<AudioSource> m_audioSourcePool;

        private void Awake()
        {
            m_audioSourcePool = new ObjectPool<AudioSource>(() =>
            {
                GameObject dummyGameObject = new GameObject("Dummy Audio Source");
                dummyGameObject.transform.parent = transform;
                dummyGameObject.SetActive(false);

                AudioSource audioSource = dummyGameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                return audioSource;
            }, 5, true);
        }

        private AudioSource GetReadyToUseAudioSource()
        {
            AudioSource source = m_audioSourcePool.GetObject();
            source.gameObject.SetActive(true);

            return source;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AudioSource source = GetReadyToUseAudioSource();
                source.clip = m_clip;
                source.Play();
            }

            foreach (var source in m_audioSourcePool.UsedObjects)
            {
                if (source.isPlaying == false)
                {
                    m_audioSourcePool.ReleaseObject(source);
                    source.gameObject.SetActive(false);
                }
            }
        }
    }
}