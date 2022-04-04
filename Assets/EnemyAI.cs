using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        timeSinceSound = Random.Range(-.5f, .5f);

    }

    CharacterMover characterMover;

    float disabledMovementTime;

    float enemyMaxRange = 20*20;

    float timeSinceLastUpdate = 999;

    float movementNoise = 45f;
    float updateInterval = 1f;

    public AudioClip[] RandomSounds;
    float soundCooldown = 2f;
    float timeSinceSound = 0;
    float soundChance = 0.25f;

    // Move towards the target
    void Update()
    {
        if (TimeManager.IsPaused)
            return;

        if (EnemyTarget.Instance == null)
        {
            characterMover.DesiredDirection = Vector2.zero;
            return;
        }

        timeSinceSound += Time.deltaTime;
        if(timeSinceSound > soundCooldown)
        {
            if(Random.Range(0f, 1f) < soundChance)
                SoundManager.Play(RandomSounds);

            timeSinceSound = Random.Range(-.5f, .5f);
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
