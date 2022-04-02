using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Things that target-based weapons can choose.
/// </summary>
public class Targettable : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        AllTargets.Add(this);
    }

    private void OnDisable()
    {
        AllTargets.Remove(this);
    }

    public static HashSet<Targettable> AllTargets = new HashSet<Targettable>();
}
