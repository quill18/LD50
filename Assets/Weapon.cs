using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string Name = "";

    public float Damage = 1;
    public float Scale = 1;

    public GameObject[] ProjectilePrefabs;

    public AudioClip[] FireSounds;

    public float ScatterRadius;

    public int NumHits = 1; // How many enemies this can damage before stopping. Pass this to projectiles.

    public float Cooldown = 1;
    private float TimeSinceFired = 0;

    public float ProjectileSpeed = 10f;
    public bool FacesVelocity;

    public int NumProjectiles = 1;
    public float MultiProjectileDelay = 0.1f;
    public float MultiProjectileSpread = 0;

    public float LifeSpan = 10;

    public float ReturnToPlayerTime = 0;    // For the boomerang.
    public float ReturnToPlayerDamping = 0.5f;
    public float ReturnToPlayerAngle = 0;
    public bool ReturnToPlayerSelfDestruct = false;

    public enum TARGETTING { NEAREST, RANDOM_SPOT, PLAYER_CENTER, RANDOM_SPOT_SPAWN, LINEAR_THROW }
    public TARGETTING Targetting = TARGETTING.NEAREST;

    public Sprite UI_Icon;


    /// <summary>
    /// ATTEMPTS to fire the weapon, if the cooldown is ready.
    /// This is effectively our "update" routine, but has to be
    /// explicitly called by the player logic.
    /// </summary>
    public void Fire()
    {
        TimeSinceFired += Time.deltaTime;
        if (TimeSinceFired < Cooldown)
        {
            return;
        }

        // Do fire
        if (Targetting == TARGETTING.NEAREST && HasValidTarget() == false)
        {
            return;
        }

        TimeSinceFired = 0;

        if (NumProjectiles == 1 || MultiProjectileDelay == 0)
        {
            SoundManager.Play(FireSounds);
        }

        StartCoroutine(Fire_Coroutine());
    }

    IEnumerator Fire_Coroutine()
    {
        for (int i = 0; i < NumProjectiles; i++)
        {
            if(EnemyTarget.Instance == null)
            {
                // Player died during a firing sequence.
                break;
            }

            if (MultiProjectileDelay > 0)
            {
                SoundManager.Play(FireSounds);
            }

            GameObject playerGO = EnemyTarget.Instance.gameObject;

            GameObject projGO = Instantiate(ProjectilePrefabs[Random.Range(0, ProjectilePrefabs.Length)]);
            WeaponProjectile projectile = projGO.GetComponent<WeaponProjectile>();
            projectile.Damage = Damage * (1f + LegacyManager.BonusDamage);
            projectile.NumHits = NumHits;
            projectile.FacesVelocity = FacesVelocity;
            projectile.LifeSpan = LifeSpan;

            projectile.ReturnToPlayerTime = ReturnToPlayerTime;
            projectile.ReturnToPlayerDamping = ReturnToPlayerDamping;
            projectile.ReturnToPlayerAngle = ReturnToPlayerAngle;
            projectile.ReturnToPlayerSelfDestruct = ReturnToPlayerSelfDestruct;

            projectile.transform.localScale = Vector3.one * Scale;

            projGO.transform.position = playerGO.transform.position;
            Vector2 targetSpot = playerGO.transform.position;

            if (Targetting == TARGETTING.PLAYER_CENTER)
            {
                // Attach "projectile" to player so it follows them
                projGO.transform.SetParent(playerGO.transform);
            }
            else if (Targetting == TARGETTING.NEAREST)
            {
                GameObject nearestTarget = GetNearestTarget();
                if (nearestTarget != null)
                {
                    targetSpot = nearestTarget.transform.position;
                }
            }
            else if (Targetting == TARGETTING.RANDOM_SPOT)
            {
                targetSpot = (Vector2)playerGO.transform.position + ScatterOffset();
            }
            else if (Targetting == TARGETTING.RANDOM_SPOT_SPAWN)
            {
                targetSpot = projGO.transform.position = (Vector2)playerGO.transform.position + ScatterOffset();
            }
            else if (Targetting == TARGETTING.LINEAR_THROW)
            {
                targetSpot = (Vector2)playerGO.transform.position + playerGO.GetComponent<CharacterMover>().LastNonZeroDirection;
            }

            projectile.Velocity = ((Vector2)targetSpot - (Vector2)projGO.transform.position).normalized * ProjectileSpeed;

            if(MultiProjectileSpread > 0 && i > 0)
            {
                projectile.Velocity = Quaternion.Euler(0, 0, MultiProjectileSpread * ((i+1)/2) * ( i%2==0 ? 1 : -1 ) ) * projectile.Velocity;
            }

            if (NumProjectiles > 1 && MultiProjectileDelay > 0)
            {
                yield return new WaitForSeconds(MultiProjectileDelay);
            }
        }

    }

    Vector2 ScatterOffset()
    {
        return Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))) *
            Vector3.right *
            Random.Range(ScatterRadius / 2f, ScatterRadius);
    }

    bool HasValidTarget()
    {
        return Targettable.AllTargets.Count != 0;
    }

    GameObject GetNearestTarget()
    {
        GameObject go = null;
        float dist = 0;

        foreach(Targettable t in Targettable.AllTargets)
        {
            //float d = (this.transform.position - t.transform.position).sqrMagnitude;
            float d = Vector2.Distance(EnemyTarget.Instance.transform.position, t.transform.position);
            if(go == null || d < dist)
            {
                go = t.gameObject;
                dist = d;
            }
        }

        return go;
    }

    /// <summary>
    /// Player picked up another copy of the weapon. What do?
    /// </summary>
    public void LevelUp()
    {
        Damage *= 1.1f;
    }

    public static Weapon GetWeaponByName(string name)
    {
        Weapon[] allWeapons = GameObject.FindObjectsOfType<Weapon>();

        foreach (Weapon w in allWeapons)
        {
            if (w.Name == name)
                return w;
        }

        Debug.LogError("Couldn't find weapon with name: " + name);
        return null;
    }


}

