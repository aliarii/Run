using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;
    private GameObject tileToSpawn;
    public float spawnPosZ = 0;
    public float eachTileLength = 27;
    public int numberOfTiles = 8;
    private int totalSpawnTile = 3;
    private Transform playerTransform;

    private int previousIndex;
    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < totalSpawnTile; i++)
        {
            if (i == 0)
            {
                EnableTile();
            }
            else
                EnableTile(Random.Range(0, numberOfTiles));
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //disable passed tiles and spawn new one
        if (playerTransform.position.z - eachTileLength >= spawnPosZ - (totalSpawnTile * eachTileLength))
        {
            int index = Random.Range(0, numberOfTiles);
            while (index == previousIndex)
                index = Random.Range(0, numberOfTiles);
            DisableTile();
            EnableTile(index);
        }
    }
    public void EnableTile(int index = 0)
    {
        tileToSpawn = tilePrefabs[index];
        //if tile already spawned, spawn other option
        if (tileToSpawn.activeInHierarchy)
            tileToSpawn = tilePrefabs[index + 8];
        if (tileToSpawn.activeInHierarchy)
            tileToSpawn = tilePrefabs[index + 16];

        //where will it be spawn 
        tileToSpawn.transform.position = Vector3.forward * spawnPosZ;
        tileToSpawn.transform.rotation = Quaternion.identity;
        tileToSpawn.SetActive(true);

        activeTiles.Add(tileToSpawn);
        spawnPosZ += eachTileLength;
        previousIndex = index;

    }
    private void DisableTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
        LevelUIManager.score += 10;
    }
}
