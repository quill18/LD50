using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        allWeapons ??= GameObject.FindObjectsOfType<Weapon>();

        /*PlayerWeapons pw = GameObject.FindObjectOfType<PlayerWeapons>();

        if(pw.EquippedWeapons.Count >= pw.MaxWeapons)
        {
            // chest has to contain an existing weapon.
        }*/

        myWeapon = allWeapons[ Random.Range(0, allWeapons.Length) ];
    }

    Weapon[] allWeapons;
    Weapon myWeapon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerWeapons pw = collision.GetComponentInParent<PlayerWeapons>();

        if (pw == null)
            return;

        // Show player a dialog box to see if they want to claim the weapon.
        GameObject.FindObjectOfType<DialogManager>().DoDialog_TreasureChest(gameObject, myWeapon);
    }
}
