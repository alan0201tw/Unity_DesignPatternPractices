using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.VisitorPattern
{
    public class Demo : MonoBehaviour
    {
        IVisitor visitor = new HumanVisitor();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                visitor = new HumanVisitor();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                visitor = new GhostVisitor();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                visitor.Visit(new GoblinHouse());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                visitor.Visit(new ElfHouse());
            }
        }
    }
}