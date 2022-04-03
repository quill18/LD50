using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog_Legacy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Pages;

    public TMP_Text TheBlankIsDead;
    public TMP_Text LegacyPoints;
    public TMP_Text Stats;

    //public Image[] CharacterImages;
    public TMP_Text[] CharacterWeaponTexts;
    Weapon[] characterWeapons = new Weapon[2];
    public TMP_Text[] CharacterTitleTexts;
    string[] characterTitles = new string[2];

    public void UpdateLegacyPoints(int spendPoints = 0)
    {
        int points = PlayerPrefs.GetInt("Legacy Points", 0);

        if(spendPoints > 0)
        {
            points -= spendPoints;
            PlayerPrefs.SetInt("Legacy Points", points);
        }

        LegacyPoints.text = points.ToString() + " legacy points\n (Unspent points are saved.)";
    }

    void SetupUI()
    {
        TheBlankIsDead.text = "The " + PlayerMetaData.Title + " is dead.";

        Stats.text =  "You survived for " + PlayerMetaData.TimeOfDeath + " seconds\n\n";
        Stats.text += "You slayed "+ PlayerMetaData.EnemiesKilled + " would-be assassin" + (PlayerMetaData.EnemiesKilled == 1 ? "" : "s") + "\n\n";
        Stats.text += "You looted "+ PlayerMetaData.ChestsLooted + " chest" + (PlayerMetaData.ChestsLooted == 1 ? "" : "s") + "\n\n";
        Stats.text += "Time for another to inherit the throne";

        // If we aren't fixed on the two character versions, remember to change CharacterImages

        characterTitles[0] = "King";
        characterTitles[1] = "Queen";

        // Pick two random weapons.
        Weapon[] ws = GameObject.FindObjectsOfType<Weapon>();
        characterWeapons[0] = ws[ Random.Range(0, ws.Length) ];
        do
        {
            // Loop until characters have different weapons
            characterWeapons[1] = ws[Random.Range(0, ws.Length)];
        } while (characterWeapons[1] == characterWeapons[0]);

        CharacterWeaponTexts[0].text = "Wielding:\n" + characterWeapons[0].Name;
        CharacterWeaponTexts[1].text = "Wielding:\n" + characterWeapons[1].Name;

        UpdateLegacyPoints();
    }

    public void ChooseCharacter(int charNum)
    {
        Debug.Log("Setting new character to " + characterTitles[charNum] + " with " + characterWeapons[charNum].Name);
        PlayerPrefs.SetString("Title", characterTitles[charNum]);
        PlayerPrefs.SetString("StartingWeapon", characterWeapons[charNum].Name);

        Close();
    }

    public void Popup()
    {
        TimeManager.Pause();

        SetupUI();

        GoToPage(0);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        TimeManager.Unpause();
        gameObject.SetActive(false);
        SceneManager.LoadScene("DummyScene");
    }

    public void GoToPage(int pageIndex)
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(i == pageIndex);
        }
    }

}
