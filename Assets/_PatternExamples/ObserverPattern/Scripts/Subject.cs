using System;
using UnityEngine;

namespace DesignPatternExample.ObserverPattern
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Subject : MonoBehaviour
    {
        // wrap up original .NET EventHandler to a new defined UnityEventHandler
        // from .NET System namespace : definition of EventHandler
        // public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
        public delegate void UnityEventHandler<TEventArgs>(UnityEngine.Object sender, TEventArgs e) where TEventArgs : EventArgs;

        public event UnityEventHandler<CollisionData> OnBigImpact;

        // public event EventHandler<CollisionData> OnBigImpact;
        // another implementation
        // public event Action<CollisionData> OnBigImpact

        private void OnCollisionEnter(Collision collision)
        {
            OnBigImpact.Invoke(this, new CollisionData(collision));
            // if using Action rather than EventHandler
            // OnBigImpact.Invoke(new CollisionData(collision));
        }
    }

    public class CollisionData : EventArgs
    {
        private Vector3 impulse;
        public Vector3 Impulse { get { return impulse; } }

        private Collider otherCollider;
        public Collider OtherCollider { get { return otherCollider; } }

        public CollisionData(Collision _collision)
        {
            impulse = _collision.impulse;
            otherCollider = _collision.collider;
        }
    }
}