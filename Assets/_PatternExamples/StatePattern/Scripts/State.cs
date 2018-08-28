using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.StatePattern
{
    enum State : byte
    {
        Idle,
        Walking,
        Jumping,
        Ducking
    }

    // This part is sort of like the Strategy pattern, but it has the advantage for main object
    // (in this case, the Demo component) to change its behavior by changing the stateCommand object.
    // Please notice that the actual transitions between commands
    // While in Strategy pattern

    // you can also use this commmand instance to handle input
    public abstract class StateCommand
    {
        // use protected since all commmand will be inherit from here, so exposing to them is enough
        protected static IdleCommand idle = new IdleCommand();
        protected static WalkingCommand walkingCommand = new WalkingCommand();

        // this will return the next command, if it's null, the state remains unchanged
        public abstract StateCommand HangleInput(Demo demo);

        // in some case these might come in handy
        // you can call these in the main object (in this case, Demo component) when a transition
        // between stateCommands occurs.

        // public abstract void OnEnterState();
        // public abstract void OnExitState();
    }

    public class IdleCommand : StateCommand
    {
        public override StateCommand HangleInput(Demo demo)
        {
            StateCommand stateCommand = null;
            var m_rigidbody = demo.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 newVel = new Vector3(-1, m_rigidbody.velocity.y, 0);
                m_rigidbody.velocity = newVel;
                stateCommand = walkingCommand;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Vector3 newVel = new Vector3(1, m_rigidbody.velocity.y, 0);
                m_rigidbody.velocity = newVel;
                stateCommand = walkingCommand;
            }

            return stateCommand;
        }
    }

    public class WalkingCommand : StateCommand
    {
        public override StateCommand HangleInput(Demo demo)
        {
            throw new System.NotImplementedException();
        }
    }
}