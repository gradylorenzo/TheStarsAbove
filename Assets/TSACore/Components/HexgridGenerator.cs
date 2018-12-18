using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexgridGenerator : MonoBehaviour {

    public int length = 5;
    public int width = 5;
    public float height = 1;
    public GameObject[] tilePrefabs;
    public GameObject MapGameObject;
    public float IncirculeRadius = 0.433f;
    public float CircumcircleRadius = 0.5f;
    public float PerlinScale;
    public Vector2 PerlinOffset;
    public List<Vector3> tiles;

    public List<GameObject> spawnedTiles;
    

    public void Start()
    {
        if (tilePrefabs.Length == 5)
        {
            OffsetPerlinNoise();
            DefineGrid();
            SpawnTiles();
            EventManager.MapGenerationComplete(width, length);
        }
        else
        {
            Debug.LogError("Not enough tile types assigned, aborting generation");
        }
    }

    private void OffsetPerlinNoise()
    {
        PerlinOffset = new Vector2(UnityEngine.Random.Range(0.0f, 10.0f), UnityEngine.Random.Range(0.0f, 10.0f));
    }

    private void DefineGrid()
    {
        

        for(int y = 0; y < length; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Vector3 newPos;
                newPos = new Vector3
                (
                    x * (CircumcircleRadius * 1.5f),     CalculatePN(x, y),     (y * (IncirculeRadius * 2)) + (x % 2 * (IncirculeRadius))
                );


                tiles.Add(newPos);
            }
        }
    }

    private float CalculatePN(int x, int y)
    {
        float xCoord = (float)x / width * PerlinScale + PerlinOffset.x;
        float yCoord = (float)y / length * PerlinScale + PerlinOffset.y;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return sample;
    }

    private void SpawnTiles()
    {
        foreach(GameObject go in spawnedTiles)
        {
            Destroy(go);
        }
        spawnedTiles.Clear();

        foreach (Vector3 p in tiles)
        {
            int i;
            if(p.y < 0.25f)
            {
                i = 0;
            }
            else if(p.y < 0.4f)
            {
                i = 1;
            }
            else if(p.y < 0.6f)
            {
                i = 2;
            }
            else if(p.y < 0.8f)
            {
                i = 3;
            }
            else
            {
                i = 4;
            }

            GameObject tile;


            tile = Instantiate(tilePrefabs[i], new Vector3(p.x, 0, p.z), tilePrefabs[i].transform.rotation);

            if(i == 0)
            {
                tile.transform.position = new Vector3(tile.transform.position.x, 0.1f * height, tile.transform.position.z);
            }
            else
            {
                tile.transform.position = new Vector3(tile.transform.position.x, Mathf.Max(0.15f * height, p.y * Mathf.Lerp(0, height, p.y)), tile.transform.position.z);
            }

            if(MapGameObject != null)
            {
                tile.transform.parent = MapGameObject.transform;
            }

            spawnedTiles.Add(tile);
        }
    }
}
