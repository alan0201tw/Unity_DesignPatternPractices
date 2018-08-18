using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.FactoryPattern
{
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private AudioClip clip;

        [Range(0f, 1f)]
        [SerializeField]
        private float volume;

        [SerializeField]
        private AudioServiceType audioServiceType;

        private IAudioServiceProvider audioServiceProvider;

        [SerializeField]
        private string monsterName;
        [SerializeField]
        private MonsterRace monsterRace;

        private void Start()
        {
            Debug.Log("Creating audioServiceProvider with type = " + audioServiceType.ToString());

            audioServiceProvider = AudioServiceFactory.CreateAudioService(audioServiceType, volume);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                audioServiceProvider.PlayAudioClip(clip);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Creating Monster...");
                MonsterFactory.Monster monster = MonsterFactory.CreateMonster(monsterRace, monsterName);

                monster.Talk();
            }
        }
    }
}