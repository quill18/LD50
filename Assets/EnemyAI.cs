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

    float enemyMaxRange = 20*20;

    float timeSinceLastUpdate = 999;

    float movementNoise = 45f;
    float updateInterval = 1f;

    // Move towards the target
    void Update()
    {
        if (TimeManager.IsPaused)
            return;

        if (EnemyTarget.Instance == null)
        {
            return;
        }

        timeSinceLastUpdate += Time.deltaTime;
        if(timeSinceLastUpdate < updateInterval)
        {
            // Only update the movement every so often.
            return;
        }
        timeSinceLastUpdate = Random.Range(0f, updateInterval/2f);

        if (disabledMovementTime > 0)
        {
            disabledMovementTime -= Time.deltaTime;
            return;
        }

        Vector2 targetPos = EnemyTarget.Instance.transform.position;

        Vector3 dirToTarget = targetPos - (Vector2)this.transform.position;

        if(dirToTarget.sqrMagnitude > enemyMaxRange)
        {
            this.transform.position += dirToTarget * 1.75f;
            dirToTarget = targetPos - (Vector2)this.transform.position;
        }

        dirToTarget = Quaternion.Euler(0, 0, Random.Range(-movementNoise, +movementNoise)) * dirToTarget;

        characterMover.DesiredDirection = dirToTarget;
    }

}
