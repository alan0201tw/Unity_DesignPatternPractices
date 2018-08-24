using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesignPatternExample.ObjectPoolPattern
{
    public class ObjectPool<T> where T : class
    {
        private List<ObjectPoolUnit> m_pool = new List<ObjectPoolUnit>();
        private Dictionary<T, ObjectPoolUnit> m_objectBeingUsed = new Dictionary<T, ObjectPoolUnit>();

        private Func<T> m_factoryFunction;
        private bool m_isExtendable;

        public int PoolSize
        {
            get { return m_pool.Count; }
        }

        public int UsedObjectCount
        {
            get { return m_objectBeingUsed.Count; }
        }

        public List<T> UsedObjects
        {
            get  { return m_objectBeingUsed.Keys.ToList(); }
        }

        public ObjectPool(Func<T> factoryFunction, int poolStartingSize, bool isExtendable)
        {
            // do validation
            if (factoryFunction == null)
                throw new Exception("ObjectPool constructor : factoryFunction cannot be null.");
            if (poolStartingSize <= 0)
                throw new Exception("ObjectPool constructor : poolStartingSize must be greater than 0.");

            // initialize
            m_isExtendable = isExtendable;
            m_factoryFunction = factoryFunction;

            for (int i = 0; i < poolStartingSize; i++)
            {
                CreateNewUnit();
            }
        }

        private ObjectPoolUnit CreateNewUnit()
        {
            // create new units with passed-in factory method
            ObjectPoolUnit newUnit = new ObjectPoolUnit(m_factoryFunction.Invoke());
            m_pool.Add(newUnit);

            return newUnit;
        }

        public T GetObject()
        {
            ObjectPoolUnit freeUnit = null;
            for (int i = 0; i < m_pool.Count; i++)
            {
                // if this unit is being used, continue to find a free one
                if (m_pool[i].IsBeingUsed == true)
                    continue;

                // if a free unit is found, store it and break the loop
                freeUnit = m_pool[i];
                break;
            }

            if (freeUnit == null)
            {
                if (m_isExtendable == false)
                {
                    // since we constraint T as class, this will always be null
                    // if T can be data types or structs, the default value can vary
                    return default(T);
                }
                else
                {
                    freeUnit = CreateNewUnit();
                }
            }

            freeUnit.IsBeingUsed = true;
            m_objectBeingUsed.Add(freeUnit.ObjectInstance, freeUnit);
            return freeUnit.ObjectInstance;
        }

        public void ReleaseObject(T releasingObject)
        {
            if (m_objectBeingUsed.ContainsKey(releasingObject))
            {
                ObjectPoolUnit releasingUnit = m_objectBeingUsed[releasingObject];
                releasingUnit.IsBeingUsed = false;
                m_objectBeingUsed.Remove(releasingObject);
            }
            else
            {
                Debug.LogWarning("Object Pool of Type : " + typeof(T) + " trying to release object : " + releasingObject + ", which is not in pool.");
            }
        }

        private class ObjectPoolUnit
        {
            private T m_instance;
            public T ObjectInstance
            {
                get { return m_instance; }
            }

            private bool m_isBeingUsed;
            public bool IsBeingUsed
            {
                get { return m_isBeingUsed; }
                set { m_isBeingUsed = value; }
            }

            public ObjectPoolUnit(T instance)
            {
                m_instance = instance;
                m_isBeingUsed = false;
            }
        }
    }

}