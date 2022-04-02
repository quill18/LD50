using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    float MaxSpeed = 5;
    public Vector2 DesiredDirection;

    Rigidbody2D rb;

    void FixedUpdate()
    {
        DesiredDirection = Vector2.ClampMagnitude(DesiredDirection, 1f);
        rb.velocity = DesiredDirection * MaxSpeed;
    }

    public void BlinkAwayFromTarget(Transform target, float dist)
    {
        Vector2 dir = this.transform.position - target.position;

        this.rb.position = this.rb.position + (dir * dist);
    }

}
