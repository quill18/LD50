using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDecay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    Health health;

    // Update is called once per frame
    void Update()
    {
        health.ChangeHP(-Time.deltaTime, false);
    }
}
