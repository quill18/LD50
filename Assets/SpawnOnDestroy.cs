using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject[] Spawns;

    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (isQuitting)
            return;

        foreach (GameObject spawn in Spawns)
        {
            GameObject go = Instantiate(spawn, this.transform.position, Quaternion.identity);

            WeaponProjectile my_wp = GetComponent<WeaponProjectile>();
            WeaponProjectile other_wp = go.GetComponent<WeaponProjectile>();

            if (my_wp != null && other_wp != null)
            {
                other_wp.Velocity = my_wp.Velocity;
                other_wp.Damage = my_wp.Damage;
                other_wp.NumHits = my_wp.NumHits;
                other_wp.FacesVelocity = my_wp.FacesVelocity;
            }
        }
    }
}
