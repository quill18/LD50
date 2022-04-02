using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generate/Manage an "infinite" tileable map
public class MapManager : MonoBehaviour
{
    void Awake()
    {
        mapContainer = this.transform;
        InitMap();
    }

    // How many TileChunks wide/tall is the map before you loop
    int MapWidth  = 100;
    int MapHeight = 100;

    float TileChunkWidth = 10;
    float TileChunkHeight = 10;

    public GameObject[] TileChunkPrefabs;

    byte?[,] tileChunkIds;
    GameObject[,] activeTileChunks; // Before spawning, check to see if already active.
    List<GameObject> activeTileChunkList; // Iterate through these to see if they are out of range

    Transform mapContainer;

    int chunkSpawnRadius = 1;
    int chunkDespawnRadius = 2;

    // Update is called once per frame
    void Update()
    {
        UpdateActiveTileChunks();
    }

    void InitMap()
    {
        //TileChunkWidth = TileChunkPrefabs[0].GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        tileChunkIds = new byte?[MapWidth, MapHeight];
        activeTileChunks = new GameObject[MapWidth, MapHeight];
        activeTileChunkList = new List<GameObject>();
    }

    void UpdateActiveTileChunks()
    {
        Vector2Int playerChunkCoord = WorldCoordToChunkCoord(
            EnemyTarget.Instance.transform.position /*+ 
            new Vector3((float)TileChunkWidth/2f,(float)TileChunkHeight/2f,0)*/);

        // Delete chunks too far from the player
        List<GameObject> newActiveChunks = new List<GameObject>();
        foreach(GameObject chunkGO in activeTileChunkList)
        {
            Vector2Int chunkCoord = WorldCoordToChunkCoord(chunkGO.transform.position);
            if(
                Mathf.Abs(chunkCoord.x - playerChunkCoord.x) > chunkDespawnRadius ||
                Mathf.Abs(chunkCoord.y - playerChunkCoord.y) > chunkDespawnRadius
                )
            {
                // Despawn!
                Destroy(chunkGO);
            }
            else
            {
                // Still good
                newActiveChunks.Add(chunkGO);
            }
        }
        activeTileChunkList = newActiveChunks;

        // Instantiate missing chunks near the player.
        // Player is the "EnemyTarget", so we can use that.
        for (int x = playerChunkCoord.x-chunkSpawnRadius; x <= playerChunkCoord.x + chunkSpawnRadius; x++)
        {
            for (int y = playerChunkCoord.y - chunkSpawnRadius; y <= playerChunkCoord.y + chunkSpawnRadius; y++)
            {
                ActivateChunkAt(x, y);
            }
        }
    }

    void ActivateChunkAt(int x, int y)
    {
        if (GetChunkAt(x, y) != null)
        {
            // Already active
            return;
        }


        GameObject newChunk = Instantiate(TileChunkPrefabs[GetChunkIdAt(x, y)], mapContainer);
        newChunk.transform.position = new Vector3(
                x * TileChunkWidth,
                y * TileChunkHeight,
                0
            );

        activeTileChunks[Modulo(x, MapWidth), Modulo(y, MapHeight)] = newChunk;
        activeTileChunkList.Add(newChunk);
    }

    GameObject GetChunkAt(int x, int y)
    {
        return activeTileChunks[Modulo(x, MapWidth), Modulo(y, MapHeight)];
    }

    byte GetChunkIdAt(int x, int y)
    {
        return tileChunkIds[Modulo(x, MapWidth), Modulo(y, MapHeight)] ??= (byte)Random.Range(0, TileChunkPrefabs.Length);         
    }

    // C# returns negative modulos in a way we don't want for our array.
    int Modulo(int total, int divider)
    {
        int r = total % divider;
        return r < 0 ? r + divider : r;
    }

    Vector2Int WorldCoordToChunkCoord(Vector2 worldPos)
    {
        return new Vector2Int( 
            Mathf.FloorToInt(worldPos.x / 10), 
            Mathf.FloorToInt(worldPos.y / 10) 
            );
    }
}
