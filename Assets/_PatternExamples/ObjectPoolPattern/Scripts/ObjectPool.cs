using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesignPatternExample.ObjectPoolPattern
{
    // This generic ObjectPool is very much a more encapsulated version of 
    // the ObjectPool by Peter Cardwell-Gardner, which can be found here : 
    // https://github.com/thefuntastic/unity-object-pool

    // Also, this is a more generic version of ObjectPool, so objects that are not MonoBehaviors can
    // be pooled. But if you're using an ObjectPool solely for GameObejcts or MonoBehaviors, you can 
    // simplify it by using "isActive" as "IsBeingUsed", and do some initialization right here in the
    // pool when GetObject or ReleaseObject is called , for example "SetActive()" or 
    // "enabled = boolValue".

    // But generally I think that kind of stuff should be done outside the pool. If you want it to be
    // done internally but still want it to be generic, you can introduce more delegates like 
    // "Action<T> OnGetObject" and "Action<T> OnReleaseObject" and invoke here whenever those methods 
    // are called.

    // For example, in our Demo script there is a AudioSource pool. If you calls "GetObject" and directly
    // call "Play" on the returned AudioSource, it might played the clip that it previously played. In
    // that case, having a "clip = null" in OnRealeaseObject looks pretty good.

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

        /// <summary>
        /// Construct a object pool wherever you need one.
        /// </summary>
        /// <param name="factoryFunction">The function used to generate pooled object, all objects in the pool is instantiated usign this function.</param>
        /// <param name="poolStartingSize">The starting size of the pool.</param>
        /// <param name="isExtendable">Whether this object pool is extendable. If true, it will create a new object instance with factoryFunction when a request for object cannot be fulfilled, otherwise the pool will return null.</param>
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