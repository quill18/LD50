using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    SpriteRenderer sr;
    float flashTimer = 0;

    public void DoFlash()
    {
        flashTimer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            sr.material.SetFloat("_FlashAmount", Mathf.Sin(flashTimer * 50f) > 0 ? 1f : 0f);
        }
        else
        {
            sr.material.SetFloat("_FlashAmount", 0f);
        }
    }
}
