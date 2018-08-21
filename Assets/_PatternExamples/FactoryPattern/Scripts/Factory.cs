using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.FactoryPattern
{
    // When using Factory Pattern, I really wish that the actual constructor of the objects are not exposed
    // externally. Since there are no "friend" keyword in C# and Unity compiles all user-defined code in
    // a single DLL file, there really isn't any way to achieve both private constructor/class
    // and non-nested factory-token relationship. So I guess I should provide both examples.

    // This example shows an exposed constructor with seperated factory-token relationship.

    // this enum should not be known by the AudioServiceProviders, since the provider should not know
    // each other exists.
    // Also, there should be more subclasses if you want to use factory pattern. I just can't think up any
    // more type of AudioServices
    public enum AudioServiceType : byte
    {
        Broken,
        Instancing
    }

    public class AudioServiceFactory
    {
        public static IAudioServiceProvider CreateAudioService(AudioServiceType audioServiceType,
            float volume = 1f)
        {
            switch (audioServiceType)
            {
                case AudioServiceType.Broken:
                    return new BrokenAudioServiceProvider();
                case AudioServiceType.Instancing:
                default:
                    return new InstancingAudioServiceProvider(volume);
            }
        }
    }

    // This example shows a nested factory-token relationship with exposed base class and 
    // private sub-classes

    public enum MonsterRace : byte
    {
        Goblin,
        Ghost
    }

    public class MonsterFactory
    {
        public abstract class Monster
        {
            public abstract string Name { get; protected set; }
            public abstract void Talk();
        }

        public static Monster CreateMonster(MonsterRace race, string name = "")
        {
            switch(race)
            {
                case MonsterRace.Goblin:
                    return new Goblin(name);
                case MonsterRace.Ghost:
                    if (name == "")
                        return new AnonymousStranger();
                    return new SlientGhost(name);
            }

            Debug.LogError("MonsterFactory.CreateMonster Error : No corresponding monster with parameters race = " + race.ToString() + ", name = " + name);
            return null;
        }

        private class Goblin : Monster
        {
            private string m_name;

            public override string Name
            {
                get { return m_name; }
                protected set
                {
                    m_name = value;
                }
            }
            
            public Goblin(string name)
            {
                Name = name;
            }

            public override void Talk()
            {
                Debug.Log("Goblin " + Name + " says : grrrrrr...");
            }
        }

        private class SlientGhost : Monster
        {
            private string m_name;

            public override string Name
            {
                get { return m_name; }
                protected set
                {
                    m_name = value;
                }
            }

            public SlientGhost(string name)
            {
                Name = name;
            }

            public override void Talk()
            {
                // I mean... this guy is a SlientGhost, what do you expect?

                Debug.Log("The SlientGhost " + Name + " trys to talk, but cannot say anything");
            }
        }

        private class AnonymousStranger : Monster
        {
            public override string Name
            {
                get
                {
                    return "Anonymous";
                }
                protected set { }
            }

            public AnonymousStranger() { }

            public override void Talk()
            {
                Debug.Log("AnonymousStranger " + Name + " says : I am " + Name);
            }
        }
    }
}