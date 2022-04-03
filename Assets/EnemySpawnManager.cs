using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float vertSize = Camera.main.orthographicSize;
        float aspectRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;


        EnemyGroupRadius = (aspectRatio > 1f) ? vertSize * aspectRatio : vertSize;

        EnemyGroupRadius += 4; // a buffer
    }

    public GameObject[] EnemyPrefabs;
    int nextEnemy = 0;
    bool randomEnemies = false;

    float EnemyGroupRadius;

    float enemySpawnCooldown = 4;
    float timeSinceSpawn = 9999999999;

    int numEnemies = 12; // This is the minumum -- max is up to 50% more

    float spacing = 10; // degrees

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.IsPaused)
            return;

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > enemySpawnCooldown)
        {
            timeSinceSpawn = 0;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        if (EnemyTarget.Instance == null)
            return;

        // Choose/Generate appropriate enemy
        GameObject enemyPrefab = EnemyPrefabs[nextEnemy++];
        if (nextEnemy >= EnemyPrefabs.Length)
            randomEnemies = true;

        if(randomEnemies)
        {
            nextEnemy = Random.Range(0, EnemyPrefabs.Length);
        }

        // Pick a spot just off screen to spawn in a clump of enemies

        Vector3 offset = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))) *
                Vector3.right * EnemyGroupRadius;

        int n = Random.Range(numEnemies, Mathf.FloorToInt(numEnemies * 1.5f));

        Vector3 playerPos = EnemyTarget.Instance.transform.position;

        for (int i = 0; i < n; i++)
        {
            SpawnEnemyAt(playerPos + offset, enemyPrefab);

            // Rotate by 5deg between spawns
            offset = Quaternion.Euler(new Vector3(0, 0, spacing)) * offset * Random.Range(0.9f, 1.1f);
        }
    }

    void SpawnEnemyAt(Vector3 pos, GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
