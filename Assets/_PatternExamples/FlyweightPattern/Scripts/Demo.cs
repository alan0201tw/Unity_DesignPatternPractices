using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DesignPatternExample.FlywrightPattern
{
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private GameObject TilePrefab;

        [SerializeField]
        private bool isUsingFlyweight;

        private void Start()
        {
            if (isUsingFlyweight)
            {
                Transform flyweightParent = new GameObject("flyweightParent").transform;
                // Flyweight
                Flyweight flyweight = new Flyweight();
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        GameObject tile = Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity, flyweightParent);

                        tile.GetComponent<Renderer>().material.color = flyweight.WorldTiles[i, j].TileColor;
                    }
                }
            }
            else
            {
                Transform nonFlyweightParent = new GameObject("nonFlyweightParent").transform;
                // Non-Flyweight
                NonFlyweight nonFlyweight = new NonFlyweight();
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        GameObject tile = Instantiate(TilePrefab, new Vector3(i, j, 10), Quaternion.identity, nonFlyweightParent);

                        tile.GetComponent<Renderer>().material.color = nonFlyweight.WorldTiles[i, j].TileColor;
                    }
                }
            }
            // there are about 500000 bytes difference in my case
            Debug.Log("Total allocated memory : " + System.GC.GetTotalMemory(false) + " bytes");
        }
    }
}