using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TutorialCheck());
    }

    IEnumerator TutorialCheck()
    {
        yield return null;
        if (PlayerPrefs.GetInt("Did Tutorial", 0) == 0)
        {
            Dialog_Tutorial.Popup();
            PlayerPrefs.SetInt("Did Tutorial", 1);
        }
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
    public Dialog_Tutorial Dialog_Tutorial;

    public void DoDialog_TreasureChest(GameObject treasureChestGO, Weapon weapon)
    {
        Dialog_TreasureChest.Popup(treasureChestGO, weapon);
    }

    public void DoDialog_Legacy()
    {
        StartCoroutine(Delayed_Legacy());
    }

    public void DoDialog_Tutorial()
    {
        Dialog_Tutorial.Popup();
    }

    IEnumerator Delayed_Legacy()
    {
        yield return new WaitForSeconds(2);
        Dialog_Legacy.Popup();
    }
}
