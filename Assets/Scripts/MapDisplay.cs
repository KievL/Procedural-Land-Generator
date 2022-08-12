using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    [SerializeField] private Material noiseMaterial;
    [SerializeField] private Material colorMeshMaterial;
    [SerializeField] private Material meshMaterial;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        textureRender.sharedMaterial = noiseMaterial;

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D textureNoise = TextureGenerator(noiseMap);        

        textureRender.sharedMaterial.SetTexture("_BaseMap", textureNoise);
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }

    //public void DrawColoredNoiseMap(float[,] noiseMap)
    //{
    //    textureRender.sharedMaterial = colorNoiseMaterial;

    //    int width = noiseMap.GetLength(0);
    //    int height = noiseMap.GetLength(1);

    //    Texture2D textureNoise = TextureGenerator(noiseMap);

    //    textureRender.sharedMaterial.SetTexture("NoiseTex", textureNoise);
    //    textureRender.transform.localScale = new Vector3(width, 1, height);
    //}

    public void DrawMesh(MeshData meshData, float[,] noiseMap)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        meshRenderer.sharedMaterial = meshMaterial;
        meshRenderer.sharedMaterial.SetTexture("_BaseMap", TextureGenerator(noiseMap));
    }



    public void DrawColoredMesh(MeshData meshData, float [,] noiseMap)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        meshRenderer.sharedMaterial = colorMeshMaterial;
        meshRenderer.sharedMaterial.SetTexture("NoiseTex", TextureGenerator(noiseMap));
    }   

    Texture2D TextureGenerator(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[(y * width) + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }
}
