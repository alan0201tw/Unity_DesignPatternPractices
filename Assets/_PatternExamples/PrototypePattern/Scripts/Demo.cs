using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.PrototypePattern
{
    public class Demo : MonoBehaviour
    {
        private Goblin m_goblin;
        private Skeleton m_skeleton;

        private void Start()
        {
            m_goblin = new Goblin(new Vector3(0f, 0f, 0f));
            m_skeleton = new Skeleton(new Vector3(1f, 0f, 0f));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Cloning a goblin...");
                m_goblin.Clone();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Cloning a skeleton...");
                m_skeleton.Clone();
            }
        }
    }
}