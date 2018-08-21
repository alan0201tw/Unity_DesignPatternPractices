using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.CommandPattern
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }
    
    public class JumpCommand : Command
    {
        // for execution
        private Rigidbody m_rigidbody;
        private readonly float jumpForce = 500f;
        // for undo
        private Vector3 originalPosition;

        public JumpCommand(Rigidbody rigidbody)
        {
            m_rigidbody = rigidbody;
        }

        public override void Execute()
        {
            originalPosition = m_rigidbody.position;

            m_rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }

        public override void Undo()
        {
            m_rigidbody.position = originalPosition;
            m_rigidbody.velocity = Vector3.zero;
        }
    }
    
    public class MoveCommand : Command
    {
        // for execution
        private Rigidbody m_rigidbody;
        private Vector3 m_movement;
        // for undo
        private Vector3 originalPosition;

        public MoveCommand(Rigidbody rigidbody, Vector3 movement)
        {
            m_rigidbody = rigidbody;
            m_movement = movement;
        }

        public override void Execute()
        {
            // record original position before moving
            originalPosition = m_rigidbody.position;

            m_rigidbody.MovePosition(originalPosition + m_movement);
        }

        public override void Undo()
        {
            m_rigidbody.position = originalPosition;
        }
    }
}
