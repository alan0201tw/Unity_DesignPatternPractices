using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.EventQueuePattern
{
    /*
        The purpose of using EventQueue is to decouple when a message or event is sent from 
        when it is processed. This can be helpful whenyou're trying to display some hint to the player,
        or some achievement unlock animations that need to be played in sequence.

        Another point worth mentioning is that, you can pass in different objects to the event queue.
        If you pass in an event ( for example a object that symbolizes the boss is dead), then the
        queue will react to this "event" as it process, sort of like an asynchronous Observer pattern.

        If you pass in messages or, in this case "requests", it means that the queue itself already
        know what it needs to do when the request comes in, it just delay the action until actual
        processing.

        According to Bob Nystrom, in the latter case the request can be treated as an Command obejct in
        the Command pattern. And if you allow global access to the event queue and accept requests, you
        are pretty much making a "Service" as in the ServiceLocator pattern.

        Finally, in this demo I use a simple Queue as the container for requests.
        Bob Nystrom suggests ring buffer ( or circular array ) as it provides several advantages.
        1. No dynamic allocation
            You don't need to "new" a request everytime some code send a message just that so you
            can stuff something in the queue.
        2. Cache-friendly or better for Data Locality
            Queue is basically a linked list, you aren't being nice to CPU when you don't put your
            data together, contiguously.

        ... and so on.
        
        Still, if you aren't facing a performance problem (or you're lazy, like me), a simple Queue
        will do the job. But if you want to implement the ring buffer, you don't really need to
        code a generic version and put it on github as a individual repo to contribute to the open 
        source community to use it.

        You can just create an array or a List ( Hey, .NET Lists are backed by arrays, what you know
        'bout that? ), and do some magic things when you access datas in it. You can find how to do 
        that in the reference below. Afterall, this project is just for very basic demonstration.

        Ref : http://gameprogrammingpatterns.com/event-queue.html

    */
    public class EventQueue : MonoBehaviour
    {
        [SerializeField]
        private float processCooldown = 1f;

        private Queue<Request> m_requestQueue = new Queue<Request>();
        private float m_timeUntilNextProcess = 0;

        public void SendRequest(string content)
        {
            m_requestQueue.Enqueue(new Request(content));
        }

        private void Start()
        {
            m_timeUntilNextProcess = processCooldown;
        }

        private void Update()
        {
            m_timeUntilNextProcess -= Time.deltaTime;

            if (m_requestQueue.Count > 0 && m_timeUntilNextProcess <= 0)
            {
                ProcessSingleRequest();

                m_timeUntilNextProcess = processCooldown;
            }
        }

        private void ProcessSingleRequest()
        {
            Request request = m_requestQueue.Dequeue();

            Debug.LogWarning("A log message from EventQueue : " + request.content);
        }

        // this is a single unit in the actual event queue, you can name it event or message...etc.
        class Request
        {
            public string content;

            public Request(string content)
            {
                this.content = content;
            }
        }
    }
}