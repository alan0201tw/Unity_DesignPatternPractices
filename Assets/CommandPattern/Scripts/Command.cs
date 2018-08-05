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
        private Rigidbody rigidbody;
        private readonly float jumpForce = 500f;
        // for undo
        private Vector3 originalPosition;

        public JumpCommand(Rigidbody _rigidbody)
        {
            rigidbody = _rigidbody;
        }

        public override void Execute()
        {
            originalPosition = rigidbody.position;

            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }

        public override void Undo()
        {
            rigidbody.position = originalPosition;
            rigidbody.velocity = Vector3.zero;
        }
    }
    
    public class MoveCommand : Command
    {
        // for execution
        private Rigidbody rigidbody;
        private Vector3 movement;
        // for undo
        private Vector3 originalPosition;

        public MoveCommand(Rigidbody _rigidbody, Vector3 _movement)
        {
            rigidbody = _rigidbody;
            movement = _movement;
        }

        public override void Execute()
        {
            // record original position before moving
            originalPosition = rigidbody.position;

            rigidbody.MovePosition(originalPosition + movement);
        }

        public override void Undo()
        {
            rigidbody.position = originalPosition;
        }
    }
}
