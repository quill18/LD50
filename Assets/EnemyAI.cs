using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
    }

    CharacterMover characterMover;

    float disabledMovementTime;

    // Move towards the target
    void Update()
    {
        if(EnemyTarget.Instance == null)
        {
            return;
        }

        if(disabledMovementTime > 0)
        {
            disabledMovementTime -= Time.deltaTime;
            return;
        }

        Vector2 targetPos = EnemyTarget.Instance.transform.position;

        characterMover.DesiredDirection = targetPos - (Vector2)this.transform.position;
    }

}
