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

    // Move towards the king
    void Update()
    {
        if(King.Instance == null)
        {
            return;
        }

        if(disabledMovementTime > 0)
        {
            disabledMovementTime -= Time.deltaTime;
            return;
        }

        Vector2 kingPos = King.Instance.transform.position;

        characterMover.DesiredDirection = kingPos - (Vector2)this.transform.position;
    }

}
