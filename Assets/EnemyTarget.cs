using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is just used to identify the object the enemies are trying to attack.
/// </summary>
public class EnemyTarget : MonoBehaviour
{
    public static EnemyTarget Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<EnemyTarget>();
            }

            return _Instance;
        }
    }
    private static EnemyTarget _Instance;
}
