using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_Legacy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Pages;


    public void Popup()
    {
        TimeManager.Pause();

        GoToPage(0);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        TimeManager.Unpause();
        gameObject.SetActive(false);
    }

    public void GoToPage(int pageIndex)
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(i == pageIndex);
        }
    }

}
