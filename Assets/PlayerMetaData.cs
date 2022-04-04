using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeOfDeath = 0;
        EnemiesKilled = 0;
        ChestsLooted = 0;

        Title = PlayerPrefs.GetString("Title", "Monarch");
        WeaponName = PlayerPrefs.GetString("StartingWeapon", "Sword");
        Weapon = Weapon.GetWeaponByName(WeaponName);

        GetComponent<PlayerWeapons>().GainWeapon(Weapon);

        GetComponent<Health>().DeathSounds = (Title == "Queen") ? DeathSounds_Queen : DeathSounds_King;
        GetComponent<Health>().HurtSounds  = (Title == "Queen") ? HurtSounds_Queen : HurtSounds_King;

        switch (Title)
        {
            case "Monarch":
                spriteRenderer.sprite = Sprite_Monarch;
                break;
            case "King":
                spriteRenderer.sprite = Sprite_King;
                break;
            case "Queen":
                spriteRenderer.sprite = Sprite_Queen;
                break;
        }
    }

    static public string Title;
    static public string WeaponName;
    static public Weapon Weapon;

    public Sprite Sprite_Monarch;
    public Sprite Sprite_King;
    public Sprite Sprite_Queen;

    public AudioClip[] DeathSounds_King;
    public AudioClip[] DeathSounds_Queen;

    public AudioClip[] HurtSounds_King;
    public AudioClip[] HurtSounds_Queen;

    public SpriteRenderer spriteRenderer;

    public static int TimeOfDeath;
    public static int EnemiesKilled;
    public static int ChestsLooted;


    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (isQuitting)
            return;

        TimeOfDeath = Mathf.CeilToInt(TimeManager.Instance.ElapsedTime);
        //Debug.Log("SAVING LEGACY POINTS");
        PlayerPrefs.SetInt("Legacy Points", PlayerPrefs.GetInt("Legacy Points", 0) + TimeOfDeath);
        PlayerPrefs.SetInt("Total Legacy Points", PlayerPrefs.GetInt("Total Legacy Points", 0) + TimeOfDeath);
    }

}
