using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EquippedWeapons = new List<Weapon>();

        // Dummy load the "Spin Sword" weapon.

        Weapon w = WeaponPrototypes.GetComponent<Weapon>();
        Debug.Log("Weapon: " + w.Name);
        EquippedWeapons.Add(w);
    }

    public int MaxWeapons = 4;
    List<Weapon> EquippedWeapons;

    public GameObject WeaponPrototypes;

    public void GainWeapon(Weapon w)
    {
        // check if we already have, is so call Level Up

        if(HasWeapon(w))
        {
            w.LevelUp();
        }
        else
        {
            EquippedWeapons.Add(w);
        }
    }

    public bool HasWeapon(Weapon w)
    {
        return EquippedWeapons.Contains(w);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.IsPaused)
            return;

        foreach (Weapon w in EquippedWeapons)
        {
            w.Fire();
        }
    }
}
