using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleWaggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    float t;
    RectTransform rt;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        rt.rotation = Quaternion.Euler(0, 0, Mathf.Sin(t * 20f) * 5f);
    }
}
