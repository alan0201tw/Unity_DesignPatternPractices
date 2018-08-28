using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.EventQueuePattern
{
    [RequireComponent(typeof(EventQueue))]
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private EventQueue m_eventQueue;

        private void Reset()
        {
            m_eventQueue = GetComponent<EventQueue>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                m_eventQueue.SendRequest("ohhhhhhhhh");
            }
        }
    }
}