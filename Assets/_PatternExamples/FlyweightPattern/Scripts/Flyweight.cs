using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternExample.FlywrightPattern
{
    public class TerrainTile
    {
        private int m_movementCost;
        public int MovementCost { get { return m_movementCost; } }

        private bool m_isWater;
        public bool IsWater { get { return m_isWater; } }

        private Color m_tileColor;
        public Color TileColor { get { return m_tileColor; } }

        public TerrainTile(int movementCost, bool isWater, Color tileColor)
        {
            m_movementCost = movementCost;
            m_isWater = isWater;
            m_tileColor = tileColor;
        }
    }
    
    public class Flyweight
    {
        // shared tile objects
        private readonly TerrainTile WaterTile = new TerrainTile(3, true, Color.blue);
        private readonly TerrainTile HillTile =
            new TerrainTile(2, false, new Color(0.8f, 0.7f, 0.3f));
        private readonly TerrainTile GrassTile = new TerrainTile(1, false, Color.green);
        // mass tile references
        public TerrainTile[,] WorldTiles = new TerrainTile[100, 100];

        public Flyweight()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    float ran = Random.Range(0f, 1f);
                    if (ran < 0.2f)
                        WorldTiles[i, j] = WaterTile;
                    else if (ran < 0.6)
                        WorldTiles[i, j] = HillTile;
                    else
                        WorldTiles[i, j] = GrassTile;
                }
            }
        }
    }
    
    public class NonFlyweight
    {
        // mass tile references
        public TerrainTile[,] WorldTiles = new TerrainTile[100, 100];

        public NonFlyweight()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    float ran = Random.Range(0f, 1f);
                    if (ran < 0.2f)
                        WorldTiles[i, j] = new TerrainTile(3, true, Color.blue);
                    else if (ran < 0.6)
                        WorldTiles[i, j] = new TerrainTile(2, false, new Color(0.8f, 0.7f, 0.3f));
                    else
                        WorldTiles[i, j] = new TerrainTile(1, false, Color.green);
                }
            }
        }
    }
}