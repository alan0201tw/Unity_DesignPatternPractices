using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.ObserverPattern
{
    public class Observer : MonoBehaviour
    {
        [SerializeField]
        private Subject observingSubject;

        private void Start()
        {
            observingSubject.OnBigImpact += () =>
            {
                Debug.Log("Observered BigImpact!");
            };
        }
    }
}