using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.StatePattern
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private Collider m_collider;
        [SerializeField]
        private Rigidbody m_rigidbody;

        private void Reset()
        {
            m_collider = GetComponent<Collider>();
            m_rigidbody = GetComponent<Rigidbody>();
        }

        [SerializeField]
        private State m_state = State.Idle;

        private void Update()
        {
            switch (m_state)
            {
                case State.Idle:
                case State.Walking:

                    State nextState = State.Idle;

                    if (Input.GetKey(KeyCode.A))
                    {
                        Vector3 newVel = new Vector3(-1, m_rigidbody.velocity.y, 0);
                        m_rigidbody.velocity = newVel;
                        nextState = State.Walking;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        Vector3 newVel = new Vector3(1, m_rigidbody.velocity.y, 0);
                        m_rigidbody.velocity = newVel;
                        nextState = State.Walking;
                    }

                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        m_rigidbody.AddForce(Vector3.up * 400f);
                        nextState = State.Jumping;
                    }

                    m_state = nextState;

                    break;

                case State.Jumping:
                    if (Input.GetKey(KeyCode.A))
                    {
                        Vector3 newVel = new Vector3(-1, m_rigidbody.velocity.y, 0);
                        m_rigidbody.velocity = newVel;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        Vector3 newVel = new Vector3(1, m_rigidbody.velocity.y, 0);
                        m_rigidbody.velocity = newVel;
                    }

                    // I mean... you should probably do some ground check in your actual game,
                    // but I'll just do this here since it's just a demo.
                    // Please don't judge me for this :(
                    if (transform.position.y <= 0.5f)
                    {
                        if (m_rigidbody.velocity.magnitude > 0)
                            m_state = State.Walking;
                        else
                            m_state = State.Idle;
                    }

                    break;

                default:
                    break;
            }
        }
    }
}