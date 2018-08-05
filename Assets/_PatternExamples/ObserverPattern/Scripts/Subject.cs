using System;
using UnityEngine;

namespace DesignPatternExample.ObserverPattern
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Subject : MonoBehaviour
    {
        public event Action OnBigImpact;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.impulse.magnitude > 5f)
            {
                if(OnBigImpact != null)
                {
                    OnBigImpact.Invoke();
                }
            }
        }
    }
}