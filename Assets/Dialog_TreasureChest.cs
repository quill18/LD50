using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_TreasureChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TMP_WeaponName);
        Debug.Log(WeaponIcon);

    }

    GameObject treasureChestGO;
    Weapon weapon;
    public GameObject[] Pages;

    public TMP_Text TMP_WeaponName;
    public Image WeaponIcon;

    public GameObject ButtonGroup_LevelUp;
    public GameObject ButtonGroup_TakeWeapon;


    public void Popup(GameObject treasureChestGO, Weapon weapon)
    {
        Debug.Log("Popup for: " + weapon.Name);
        Debug.Log(TMP_WeaponName);

        TimeManager.Pause();
        this.treasureChestGO = treasureChestGO;
        this.weapon = weapon;

        TMP_WeaponName.text = weapon.Name;
        WeaponIcon.sprite = weapon.UI_Icon;

        bool hasWeap = GameObject.FindObjectOfType<PlayerWeapons>().HasWeapon(weapon);
        ButtonGroup_LevelUp.SetActive(hasWeap);
        ButtonGroup_TakeWeapon.SetActive(!hasWeap);

        GoToPage(0);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        TimeManager.Unpause();
        gameObject.SetActive(false);
    }

    public void GoToPage( int pageIndex )
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(i == pageIndex);
        }
    }

    public void TakeWeapon()
    {
        GameObject.FindObjectOfType<PlayerWeapons>().GainWeapon(weapon);
        Close();
        Destroy(treasureChestGO);
    }

}
