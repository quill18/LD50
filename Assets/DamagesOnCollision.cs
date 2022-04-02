using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
    }

    CharacterMover characterMover;

    public float BlinkDistanceOnCollision = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(BlinkDistanceOnCollision > 0)
        {
            characterMover.BlinkAwayFromTarget(collision.transform, BlinkDistanceOnCollision);
        }
    }
}
