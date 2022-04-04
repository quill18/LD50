using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Heart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = EnemyTarget.Instance.GetComponent<Health>();
    }

    public Image HeartImage;
    Health playerHealth;
    public GameObject ImminentDeath;

    // Update is called once per frame
    void Update()
    {
        if(EnemyTarget.Instance == null)
        {
            ImminentDeath.SetActive(false);
            return;
        }

        float healthPercentage = playerHealth == null ? 0 : playerHealth.GetHP() / playerHealth.GetMaxHP();

        ImminentDeath.SetActive( playerHealth.GetHP() <= 9);

        HeartImage.fillAmount = healthPercentage;
    }
}
