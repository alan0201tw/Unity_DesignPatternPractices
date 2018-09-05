using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.VisitorPattern
{
    /// <summary>
    /// The visitor allows adding new virtual functions to a family of classes, without modifying the classes.
    /// To simply put, this is simulating functional programming language, where you can easily add methods 
    /// to multiple classes.
    /// 
    /// In OOP, you can easily create a new class and all methods in it. But creating a new method in a superclass
    /// is very difficult, since you need to crack open every subclass and implement the method.
    /// 
    /// On the contrary, Functional programming languages use pattern matching to determine the actual function
    /// when we sees the type of the pass-in object. Sort of like a switch statement on every type and execute a
    /// function according to the object's type. As you can imagine, in this case it's hard to create a new class,
    /// since you'll have to crack open every pattern matching code and add a new case for that class. But adding
    /// a function to multiple subclasses is simple, just create a new pattern matching and do everything there.
    /// 
    /// In the case of Visitor pattern, it's like all subclasses have an additional "Visit" method outside the
    /// class. And for a new method for all subclasses, just create a new type of visitor and do everything there.
    /// </summary>
    public interface IVisitor
    {
        // here, you should declare all concrete classes of an abstract class, or interface
        void Visit(GoblinHouse goblinHouse);
        void Visit(ElfHouse elfHouse);
    }

    public class HumanVisitor : IVisitor
    {
        public void Visit(GoblinHouse goblinHouse)
        {
            Debug.Log("A human is visiting a goblin house.");
        }

        public void Visit(ElfHouse elfHouse)
        {
            Debug.Log("A human is visiting a elf house.");
        }
    }

    public class GhostVisitor : IVisitor
    {
        public void Visit(GoblinHouse goblinHouse)
        {
            Debug.Log("A spooky ghost is visiting a goblin house.");
        }

        public void Visit(ElfHouse elfHouse)
        {
            Debug.Log("A spooky ghost is visiting a elf house.");
        }
    }

    public interface IHouse
    {
        void Accept(IVisitor visitor);
    }

    public class GoblinHouse : IHouse
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ElfHouse : IHouse
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}