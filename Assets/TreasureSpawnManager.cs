using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject[] TreasureChestPrefabs;
    public GameObject TreasureArrowPrefab;

    float timeBetweenChests = 20;
    float timeSinceChest = 99;

    float chestSpawnRadius = 50;

    // Update is called once per frame
    void Update()
    {
        timeSinceChest += Time.deltaTime;
        if (timeSinceChest < timeBetweenChests)
            return;

        timeSinceChest = 0;

        Vector3 offset = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))) * Vector3.right * chestSpawnRadius;
        Vector3 playerPos = EnemyTarget.Instance.transform.position;

        SpawnChest(playerPos + offset);
    }

    public void SpawnChest(Vector2 pos)
    {
        GameObject chestGO = Instantiate(TreasureChestPrefabs[Random.Range(0, TreasureChestPrefabs.Length)], pos, Quaternion.identity);
        Instantiate(TreasureArrowPrefab).GetComponent<TreasureArrow>().TreasureChestTarget = chestGO;

    }
}
