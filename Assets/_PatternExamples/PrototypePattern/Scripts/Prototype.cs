﻿using UnityEngine;

namespace DesignPatternExample.PrototypePattern
{
    public abstract class Monster
    {
        protected Vector3 m_position;
        protected Color m_color;

        public abstract Monster Clone();
        public abstract void InstantiateInScene();
    }

    public class Goblin : Monster
    {
        public Goblin(Vector3 _position)
        {
            m_color = Color.red;
            m_position = _position;
            InstantiateInScene();
        }

        public Goblin(Goblin _prototype)
        {
            m_color = Color.red;
            m_position = _prototype.m_position;
            InstantiateInScene();
        }

        public override Monster Clone()
        {
            return new Goblin(this);
        }

        public override void InstantiateInScene()
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.GetComponent<Renderer>().material.color = m_color;
            gameObject.transform.position = m_position;
        }
    }

    public class Skeleton : Monster
    {
        public Skeleton(Vector3 _position)
        {
            m_color = Color.grey;
            m_position = _position;
            InstantiateInScene();
        }

        public Skeleton(Skeleton _prototype)
        {
            m_color = Color.grey;
            m_position = _prototype.m_position;
            InstantiateInScene();
        }

        public override Monster Clone()
        {
            return new Skeleton(this);
        }

        public override void InstantiateInScene()
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.GetComponent<Renderer>().material.color = m_color;
            gameObject.transform.position = m_position;
        }
    }
}