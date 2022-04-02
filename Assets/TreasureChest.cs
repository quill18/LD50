using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        allWeapons ??= GameObject.FindObjectsOfType<Weapon>();

        myWeapon = allWeapons[ Random.Range(0, allWeapons.Length) ];
    }

    Weapon[] allWeapons;
    Weapon myWeapon;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerWeapons pw = collision.GetComponentInParent<PlayerWeapons>();

        if (pw == null)
            return;

        // Show player a dialog box to see if they want to claim the weapon.
    }
}
