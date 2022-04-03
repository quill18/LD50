using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static DialogManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<DialogManager>();
            }

            return _Instance;
        }
    }
    private static DialogManager _Instance;

    public Dialog_TreasureChest Dialog_TreasureChest;
    public Dialog_Legacy Dialog_Legacy;

    public void DoDialog_TreasureChest(GameObject treasureChestGO, Weapon weapon)
    {
        Dialog_TreasureChest.Popup(treasureChestGO, weapon);
    }

    public void DoDialog_Legacy()
    {
        StartCoroutine(Delayed_Legacy());
    }

    IEnumerator Delayed_Legacy()
    {
        yield return new WaitForSeconds(2);
        Dialog_Legacy.Popup();
    }
}
