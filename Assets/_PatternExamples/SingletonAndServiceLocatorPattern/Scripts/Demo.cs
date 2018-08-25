using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.SingletonAndServiceLocatorPattern
{
    public class Demo : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                GameServices.Instance.GetSceneService().LoadSceneAsync(1, () =>
                {
                    Debug.Log("Load Complete");
                });
            }
        }
    }
}