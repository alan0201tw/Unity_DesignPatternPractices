using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.FactoryPattern
{
    public interface IAudioServiceProvider
    {
        float GetVolume();
        void SetVolume(float _volume);
        bool PlayAudioClip(AudioClip _clip);
        /// ...etc
    }

    public class BrokenAudioServiceProvider : IAudioServiceProvider
    {
        public BrokenAudioServiceProvider() { }

        public float GetVolume()
        {
            return 0f;
        }

        public bool PlayAudioClip(AudioClip _clip)
        {
            return false;
        }

        public void SetVolume(float _volume)
        {
            return;
        }
    }

    public class InstancingAudioServiceProvider : IAudioServiceProvider
    {
        public InstancingAudioServiceProvider(float _volume)
        {
            m_volume = _volume;
        }

        private float m_volume;

        public float GetVolume()
        {
            return m_volume;
        }

        public bool PlayAudioClip(AudioClip _clip)
        {
            GameObject dummy = new GameObject();
            AudioSource dummyAudioSource = dummy.AddComponent<AudioSource>();

            dummyAudioSource.clip = _clip;
            dummyAudioSource.volume = m_volume;
            dummyAudioSource.Play();

            // destroy the dummy gameObject one second after the clip is finished
            Object.Destroy(dummy, _clip.length + 1f);

            return true;
        }

        public void SetVolume(float _volume)
        {
            m_volume = _volume;
        }
    }
}