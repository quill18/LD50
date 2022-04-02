using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChunk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Iterate through our children to find TreasureSpawnSpots
        treasureSpawnSpots = new List<Transform>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if( this.transform.GetChild(i).gameObject.name == "TreasureSpawnSpots" )
            {
                treasureSpawnSpots.Add(this.transform.GetChild(i));
            }
        }

        // Chance of spawning a treasure chest at each of the spawn spots
        // TODO

        // Generate a 10x10 grid of floor tiles
        // 

        GameObject floorTileContainer = new GameObject();
        floorTileContainer.transform.SetParent(this.transform);
        floorTileContainer.transform.localPosition = Vector3.zero;
        Vector3 position;
        Quaternion rotation = Quaternion.identity;

        for(int x = 0; x < tilesWidth; x++)
        {
            for (int y = 0; y < tilesHeight; y++)
            {
                position = new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0);
                GameObject go = Instantiate(FloorTilePrefab, position, rotation, floorTileContainer.transform);
                go.GetComponent<SpriteRenderer>().sprite = FloorTileSprite[ Random.Range(0, FloorTileSprite.Length) ];
            }
        }
    }

    public GameObject FloorTilePrefab;
    public Sprite[] FloorTileSprite;

    int tilesWidth  = 10;
    int tilesHeight = 10;

    List<Transform> treasureSpawnSpots;
}
