using UnityEngine;

namespace DesignPatternExample.ObserverPattern
{
    public class Observer : MonoBehaviour
    {
        [SerializeField]
        private Subject observingSubject;

        private void Start()
        {
            observingSubject.OnBigImpact += OnObservingSubjectCollide;
        }

        private void OnDestroy()
        {
            observingSubject.OnBigImpact -= OnObservingSubjectCollide;
        }

        private void OnObservingSubjectCollide(Object sender, CollisionData collisionData)
        {
            string eventSenderName = sender.name;

            if (collisionData.Impulse.magnitude > 10f)
            {
                Debug.Log(eventSenderName + " had a big collision with " + collisionData.OtherCollider.name);
            }
            else
            {
                Debug.Log(eventSenderName + " had a normal collision with " + collisionData.OtherCollider.name);
            }
        }
    }
}