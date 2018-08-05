using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody target;
        // .net List is an array-backed data structure, so use it as if it is an array
        // , don't confuse it with a LinkedList
        private List<Command> m_commandList = new List<Command>();
        [SerializeField]
        private int m_currentCommandIndex = -1;

        private void Update()
        {
            Command command = null;

            if (Input.GetKeyDown(KeyCode.A))
            {
                command = new MoveCommand(target, new Vector3(-1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                command = new MoveCommand(target, new Vector3(1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                command = new JumpCommand(target.GetComponent<Rigidbody>());
            }
            // Undo
            else if (Input.GetKeyDown(KeyCode.Z) && m_currentCommandIndex >= 0)
            {
                // undo last command, and reduce current command index
                m_commandList[m_currentCommandIndex].Undo();
                m_currentCommandIndex--;
            }
            // Redo
            else if (Input.GetKeyDown(KeyCode.C) && m_currentCommandIndex < m_commandList.Count - 1)
            {
                // point current command to the next one, then execute
                m_currentCommandIndex++;
                m_commandList[m_currentCommandIndex].Execute();
            }

            if (command != null)
            {
                command.Execute();

                // remove commands before the redo
                int removeIndex = m_currentCommandIndex + 1;
                m_commandList.RemoveRange(removeIndex, m_commandList.Count - removeIndex);

                m_commandList.Add(command);
                // point to the newest command index
                m_currentCommandIndex = m_commandList.Count - 1;
            }

            //Debug.Log(m_commandList.Count);
        }
    }
}