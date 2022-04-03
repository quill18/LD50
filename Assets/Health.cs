using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        spriteFlasher = GetComponent<SpriteFlasher>();
    }

    private float currentHP;
    public float MaxHP = 1f;

    SpriteFlasher spriteFlasher;

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.IsPaused)
            return;
    }

    public void Die()
    {
        //Debug.Log(gameObject.name + " has died.");

        if(GetComponent<EnemyAI>() != null)
        {
            // That counts as a kill!

            if(Random.Range(0, 1000) <= PlayerPrefs.GetInt("Chest Drop Chance", 0))
            {
                // Drop a random chest
                GameObject.FindObjectOfType<TreasureSpawnManager>().SpawnChest(this.transform.position);
            }

            PlayerMetaData.EnemiesKilled++;
        }

        Destroy(gameObject);
    }

    public void ChangeHP(float v)
    {
        currentHP = Mathf.Clamp(currentHP + v, 0f, MaxHP);

        if(v < 0)
        {
            // Took Damage
            spriteFlasher?.DoFlash();
        }

        if(currentHP <= 0)
        {
            Die();
        }
    }

    public float GetHP()
    {
        return currentHP;
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }
}
