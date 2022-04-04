using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegacyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitUpgrades();
        SetupUI();

    }

    Dictionary<string, LegacyUpgrade> legacyUpgrades = new Dictionary<string, LegacyUpgrade>();

    public GameObject UpgradeScreenContainer;
    public GameObject UpgradeButtonPrefab;

    static public float BonusDamage = 0;

    public Dialog_Legacy Dialog_Legacy;

    void InitUpgrades()
    {
        RegisterUpgrade(new LegacyUpgrade("x2 Health", 5, (l) => { GetPlayerGO().GetComponent<Health>().MaxHP *= Mathf.Pow(2, l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("+10% Global Damage", 5, (l) => { LegacyManager.BonusDamage = 0.1f * l.UpgradeLevel; }));
        RegisterUpgrade(new LegacyUpgrade("0.1% Chest Drop Chance", 5, (l) => { PlayerPrefs.SetInt("Chest Drop Chance", l.UpgradeLevel); }));

        RegisterUpgrade(new LegacyUpgrade("+2 Sword Damage", 5, (l) => { ChangeWeaponDamage("Sword", l.UpgradeLevel*2); }));
        RegisterUpgrade(new LegacyUpgrade("+10% Sword Radius", 5, (l) => { SetWeaponScale("Sword", 1f + (l.UpgradeLevel / 10f)); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Sword Cooldown", 2, (l) => { ChangeWeaponCooldown("Sword", 1f - (l.UpgradeLevel / 10f)); }));

        RegisterUpgrade(new LegacyUpgrade("+1 Bomb Damage", 5, (l) => { ChangeWeaponDamage("Bomb", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("+10% Bomb Radius", 5, (l) => { SetWeaponScale("Bomb", 1f + (l.UpgradeLevel / 10f)); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Bomb Cooldown", 5, (l) => { ChangeWeaponCooldown("Bomb", 1f - (l.UpgradeLevel / 10f)); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Bomb Fuse", 5, (l) => { ChangeWeaponLifespan("Bomb", 1f - (l.UpgradeLevel / 10f)); }));

        RegisterUpgrade(new LegacyUpgrade("+1 Magic Missile Damage", 5, (l) => { ChangeWeaponDamage("Magic Missile", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("+1 Magic Missiles", 3, (l) => { ChangeWeaponProjectiles("Magic Missile", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Magic Missile Cooldown", 5, (l) => { ChangeWeaponCooldown("Magic Missile", 1f - (l.UpgradeLevel / 10f)); }));

        RegisterUpgrade(new LegacyUpgrade("+1 Shuriken Damage", 5, (l) => { ChangeWeaponDamage("Shuriken", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("+2 Shuriken", 4, (l) => { ChangeWeaponProjectiles("Shuriken", l.UpgradeLevel * 2); }));
        RegisterUpgrade(new LegacyUpgrade("+1 Shuriken Hits", 5, (l) => { ChangeWeaponNumHits("Shuriken", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Shuriken Cooldown", 5, (l) => { ChangeWeaponCooldown("Shuriken", 1f - (l.UpgradeLevel / 10f)); }));

        RegisterUpgrade(new LegacyUpgrade("+1 Boomerang Damage", 3, (l) => { ChangeWeaponDamage("Boomerang", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("+1 Boomerang", 2, (l) => { ChangeWeaponProjectiles("Boomerang", l.UpgradeLevel); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Boomerang Cooldown", 5, (l) => { ChangeWeaponCooldown("Boomerang", 1f - (l.UpgradeLevel / 10f)); }));
        RegisterUpgrade(new LegacyUpgrade("+10% Boomerang Radius", 5, (l) => { SetWeaponScale("Boomerang", 1f + (l.UpgradeLevel / 10f)); }));

        RegisterUpgrade(new LegacyUpgrade("+0.5 Spirit Orb Damage", 5, (l) => { ChangeWeaponDamage("Spirit Orb", l.UpgradeLevel/2f); }));
        RegisterUpgrade(new LegacyUpgrade("-10% Spirit Orb Cooldown", 5, (l) => { ChangeWeaponCooldown("Spirit Orb", 1f - (l.UpgradeLevel / 10f)); }));
        RegisterUpgrade(new LegacyUpgrade("+1 Spirit Orb Hits", 5, (l) => { ChangeWeaponNumHits("Spirit Orb", l.UpgradeLevel); }));
    }

    void SetupUI()
    {
        foreach(KeyValuePair<string, LegacyUpgrade> upgrade in legacyUpgrades)
        {
            // upgrade.Key
            GameObject buttonGO = Instantiate(UpgradeButtonPrefab, UpgradeScreenContainer.transform);
            buttonGO.GetComponentInChildren<Button>().onClick.AddListener( () => { OnClick(buttonGO, upgrade.Value); } );
            upgrade.Value.ButtonGO = buttonGO;
            SetupButtonText(buttonGO, upgrade.Value);
        }
    }

    public void RefreshUI()
    {
        // Loop through each button to enable/disable buttons base on cost.
        foreach (KeyValuePair<string, LegacyUpgrade> upgrade in legacyUpgrades)
        {
            SetupButtonText(upgrade.Value.ButtonGO, upgrade.Value);
        }
    }

    void SetupButtonText(GameObject buttonGO, LegacyUpgrade upgrade)
    {
        buttonGO.transform.Find("Button Text").GetComponent<TMP_Text>().text = upgrade.Name;
        buttonGO.transform.Find("Limit Box/Limit Text").GetComponent<TMP_Text>().text = upgrade.UpgradeLevel.ToString() + "/" + upgrade.MaxLevel.ToString();
        buttonGO.transform.Find("Cost Box/Cost Text").GetComponent<TMP_Text>().text = "£" + CostToLevel(upgrade);

        //Debug.Log("- checking points: " + PlayerPrefs.GetInt("Legacy Points"));
        buttonGO.GetComponent<Button>().interactable = PlayerPrefs.GetInt("Legacy Points") >= CostToLevel(upgrade);


        Dialog_Legacy.UpdateLegacyPoints();
    }

    void OnClick( GameObject buttonGO, LegacyUpgrade upgrade)
    {
        Debug.Log("OnClick: " + upgrade.Name);

        int cost = CostToLevel(upgrade);
        int currentPoints = PlayerPrefs.GetInt("Legacy Points");

        if (cost > currentPoints)
        {
            Debug.Log("Insufficient legacy points.");
            return;
        }

        PlayerPrefs.SetInt("Legacy Points", currentPoints - cost);
        upgrade.IncreaseLevel();
        //SetupButtonText(buttonGO, upgrade);
        RefreshUI();
    }

    int CostToLevel(LegacyUpgrade upgrade)
    {
        // Cost is based on the current level
        return (int)Mathf.Pow(5, upgrade.UpgradeLevel);
    }

    void RegisterUpgrade( LegacyUpgrade up )
    {
        legacyUpgrades.Add(up.Name, up);
        up.ApplyUpgrade(up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Helpers
    void SetWeaponScale(string name, float scale)
    {
        Weapon w = Weapon.GetWeaponByName(name);
        w.Scale = scale;
    }
    
    void ChangeWeaponCooldown(string name, float cooldownPercChange)
    {
        Weapon.GetWeaponByName(name).Cooldown *= cooldownPercChange;
    }
    void ChangeWeaponLifespan(string name, float lifespanPercChange)
    {
        Weapon.GetWeaponByName(name).LifeSpan *= lifespanPercChange;
    }
    void ChangeWeaponDamage(string name, float bonusDamage)
    {
        Weapon.GetWeaponByName(name).Damage += bonusDamage;
    }

    void ChangeWeaponProjectiles(string name, int extraProjectiles)
    {
        Weapon.GetWeaponByName(name).NumProjectiles += extraProjectiles;
    }

    void ChangeWeaponNumHits(string name, int extraNumHits)
    {
        Weapon.GetWeaponByName(name).NumHits += extraNumHits;
    }

    GameObject GetPlayerGO()
    {
        return EnemyTarget.Instance.gameObject;
    }


}

public class LegacyUpgrade
{
    public LegacyUpgrade(string name, int maxLevel, ApplyUpgradeDelegate applyUpgrade)
    {
        Name = name;
        UpgradeLevel = PlayerPrefs.GetInt(Name, 0);
        MaxLevel = maxLevel;
        ApplyUpgrade = applyUpgrade;
    }

    public string Name;
    public int UpgradeLevel = 0;
    public int MaxLevel = 1;
    public delegate void ApplyUpgradeDelegate(LegacyUpgrade l);
    public ApplyUpgradeDelegate ApplyUpgrade;

    public GameObject ButtonGO;

    public void IncreaseLevel()
    {
        if (UpgradeLevel >= MaxLevel)
            return;

        UpgradeLevel++;

        PlayerPrefs.SetInt(Name, UpgradeLevel);
    }

}