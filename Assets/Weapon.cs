using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public string Name = "";

    public float Damage = 1;
    public float Scale = 1;

    public GameObject[] ProjectilePrefabs;

    public float ScatterRadius;

    public int NumHits = 1; // How many enemies this can damage before stopping. Pass this to projectiles.

    public float Cooldown = 1;
    private float TimeSinceFired = 0;

    public enum TARGETTING { NEAREST, RANDOM_SPOT, PLAYER_CENTER }
    public TARGETTING Targetting = TARGETTING.NEAREST;


    /// <summary>
    /// ATTEMPTS to fire the weapon, if the cooldown is ready.
    /// This is effectively our "update" routine, but has to be
    /// explicitly called by the player logic.
    /// </summary>
    public void Fire()
    {
        TimeSinceFired += Time.deltaTime;
        if(TimeSinceFired < Cooldown)
        {
            return;
        }

        // Do fire
        TimeSinceFired = 0;
        GameObject playerGO = EnemyTarget.Instance.gameObject;

        GameObject projGO = Instantiate(ProjectilePrefabs[ Random.Range(0, ProjectilePrefabs.Length) ]);
        WeaponProjectile projectile = projGO.GetComponent<WeaponProjectile>();
        projectile.Damage = Damage;
        projectile.NumHits = NumHits;
        projectile.Velocity = Vector2.zero; // TODO

        if (Targetting == TARGETTING.PLAYER_CENTER)
        {
            // Attach "projectile" to player so it follows them
            projGO.transform.SetParent(playerGO.transform);
            projGO.transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// Player picked up another copy of the weapon. What do?
    /// </summary>
    public void LevelUp()
    {
        Damage *= 1.1f;
    }

}

