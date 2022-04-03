using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        LastNonZeroDirection = Vector2.right;
    }

    public float MaxSpeed = 5;
    public Vector2 DesiredDirection;
    public Vector2 LastNonZeroDirection;

    Rigidbody2D rb;

    SpriteRenderer spriteRenderer;

    void FixedUpdate()
    {
        if (TimeManager.IsPaused)
            return;

        DesiredDirection = Vector2.ClampMagnitude(DesiredDirection, 1f);
        rb.velocity = DesiredDirection * MaxSpeed;

        if (DesiredDirection.x != 0)
        {
            //spriteRenderer.flipX = DesiredDirection.x < 0;
            transform.localScale = new Vector3(DesiredDirection.x < 0 ? -1 : 1, 1, 1);
        }

        if(DesiredDirection.sqrMagnitude > 0)
        {
            LastNonZeroDirection = DesiredDirection;
        }
    }

    public void BlinkAwayFromTarget(Transform target, float dist)
    {
        Vector2 dir = this.transform.position - target.position;

        this.rb.position = this.rb.position + (dir * dist);
    }

}
