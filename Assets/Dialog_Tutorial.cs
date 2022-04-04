using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GoToPage(0);
    }

    public GameObject[] Pages;
    int activePage = 0;


    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            GoToPage(activePage + 1);

            if (activePage >= Pages.Length)
            {
                Close();
            }

        }
    }

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

        activePage = pageIndex;
    }

}
