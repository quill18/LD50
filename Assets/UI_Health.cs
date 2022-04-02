using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    TMP_Text textMesh;

    // Update is called once per frame
    void Update()
    {
        float h = 0;
        float mh = 100;

        if (King.Instance != null)
        {
            Health health = King.Instance.GetComponent<Health>();
            h = health.GetHP();
            mh = health.GetMaxHP();
        }

        string s = "'Til Death: " + Mathf.CeilToInt(h) + " ("+ MoodString(h, mh) +")";

        textMesh.text = s;
    }

    string MoodString(float h, float mh)
    {
        float percentage = h / mh;

        string s;

        if(percentage > 0.8f)
        {
            s = "Feeling Great";
        }
        else if (percentage > 0.6f)
        {
            s = "Feeling Good";
        }
        else if (percentage > 0.4f)
        {
            s = "Feeling Okay";
        }
        else if (percentage > 0.2f)
        {
            s = "Not Quite Dead";
        }
        else
        {
            s = "At Death's Door";
        }

        return s;
    }
}
