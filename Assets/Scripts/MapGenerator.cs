using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, Mesh, ColoredTerrain, ColoredNoise};
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public float heightMultiplier;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed,noiseScale, octaves, persistance, lacunarity, offset);

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();

        if(drawMode== DrawMode.NoiseMap)
        {
            mapDisplay.DrawNoiseMap(noiseMap);
        }
        else if(drawMode== DrawMode.Mesh)
        {
            mapDisplay.DrawMesh(MeshGenerator.GenerateMesh(noiseMap, heightMultiplier), noiseMap);
        }
        else if (drawMode == DrawMode.ColoredTerrain)
        {
            mapDisplay.DrawColoredMesh(MeshGenerator.GenerateMesh(noiseMap, heightMultiplier), noiseMap);
        }
        //else if(drawMode == DrawMode.ColoredNoise)
        //{
        //    mapDisplay.DrawColoredNoiseMap(noiseMap);
        //}
        
    }

    public void ApplyTerrainTexture()
    {

    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
    }
}
