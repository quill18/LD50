using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        timeActive = 0;
    }

    public float time = 0.4f;
    float timeActive;

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.IsPaused)
            return;

        timeActive += Time.deltaTime;

        if(timeActive > time)
        {
            gameObject.SetActive(false);
        }
    }
}
