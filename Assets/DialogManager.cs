using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Dialog_TreasureChest Dialog_TreasureChest;


    public void DoDialog_TreasureChest(GameObject treasureChestGO, Weapon weapon)
    {
        Dialog_TreasureChest.Popup(treasureChestGO, weapon);
    }
}
