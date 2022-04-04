using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAspectRatioFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        // Is the screen wider than 16:9?

        if (Screen.width / Screen.height > 16f / 9f)
        {
            // Ultra wide screen aspect ratio
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
        }
        else
        {
            GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

        }
    }

}
