using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is a rather simple example, and a very rough one.
 * 
 * A more practical and common place (in Unity) to see this pattern is when we're
 * trying to compute the MVP matrix of a GameObject. So the matrix needs to change
 * only when its parent (Transform) changes its Transform, otherwise the matrix
 * should remain the same, so there is no point to compute its MVP matrix every
 * frame by multipling all the matrices.
 * 
 * In short, for a GameObject, its MVP matrix looks like this.
 * 
 * globalMVP = parent.globalMVP * localMVP;
 * 
 * So we need to multiply all the matrices in order to get the actual MVP for
 * a particular GameObject. But we do not need to do this every frame, we can
 * use the dirty bit to check if the parent.globalMVP or localMVP is changed, then
 * compute accordingly.
 * 
 * like...
 * 
 *  Matrix4x4 localMVP;
 *  Matrix4x4 LocalMVP
 *  {
 *      get
 *      {
 *          return localMVP;
 *      }
 *      set
 *      {
 *          isDirty = true;
 *          localMVP = value;
 *      }
 *  }
 *  
 *  // in my opinion, I'll want this field to be marked as readonly by the class
 *  // , but I do not know an elegant way to do this, and also able to write to it
 *  // in the getter of GlobalMVP.
 *  Matrix4x4 globalMVP;
 *  Matrix4x4 GlobalMVP
 *  {
 *      get
 *      {
 *          if(isDirty == true)
 *          {
 *              // notice that by calling the parent's property, if the parent
 *              // also needed to recompute its GlobalMVP, it'll do exactly that.
 *              // So it'll recursively update every matrix that is dirty.
 *              globalMVP = parent.GlobalMVP * LocalMVP;
 *              isDirty = false;
 *          }
 *          return globalMVP;
 *      }
 *  }
 * 
 * 
 */
public class DirtyBitDemo : MonoBehaviour
{
    [SerializeField]
    private bool isUsingDirtyBit = false;

    private int[] elements = new int[10000000];

    private bool isDirty = true;

    private int m_cachedMaxElement;
    public int MaxElement
    {
        get
        {
            if (isUsingDirtyBit && !isDirty)
            {
                return m_cachedMaxElement;
            }
            isDirty = false;

            int max = -1;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] > max)
                {
                    max = elements[i];
                }
            }
            // cached the max element
            m_cachedMaxElement = max;

            return max;
        }
    }

    private void RandomElements()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = Random.Range(0, 10000);
        }
        isDirty = true;
    }

    private void Start()
    {
        RandomElements();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float startTime = Time.realtimeSinceStartup;
            Debug.Log(MaxElement);
            Debug.Log("isUsingDirtyBit = " + isUsingDirtyBit + ", Passed time = " + (Time.realtimeSinceStartup - startTime));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("RandomElements");
            RandomElements();
        }
    }
}