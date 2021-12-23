using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles = new List<GameObject>();
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles;
    private int totalSpawnTile = 4;
    public Transform playerTransform;

    private int previousIndex;
    void Start()
    {
        numberOfTiles = tilePrefabs.Length;

        for (int i = 0; i < totalSpawnTile; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, numberOfTiles));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 30 >= zSpawn - (totalSpawnTile * tileLength))
        {
            SpawnTile(Random.Range(0, numberOfTiles));
            DeleteTile();

        }
    }
    public void SpawnTile(int index = 0)
    {
        GameObject newTile = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(newTile);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        LevelUIManager.score += 10;
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
