using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    // Start is called before the first frame update
    public float Lifespan = 1;

    // Update is called once per frame
    void Update()
    {
        Lifespan -= Time.deltaTime;

        if(Lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
