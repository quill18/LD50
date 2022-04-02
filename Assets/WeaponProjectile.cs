using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Velocity;

        if (FacesVelocity && Velocity.sqrMagnitude > 0)
        {
            rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(Velocity.y, Velocity.x);
        }
    }

    Rigidbody2D rb;

    public Vector2 Velocity;
    public float Damage;
    public int NumHits;
    public bool FacesVelocity;

    public float LifeSpan = 10;

    void Update()
    {
        LifeSpan -= Time.deltaTime;

        if(LifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NumHits--;
        if(NumHits <= 0)
        {
            Destroy(gameObject);
        }

        Health h = collision.GetComponentInParent<Health>();
        if (h == null)
            return;

        DoDamage(h);
    }

    void DoDamage(Health h)
    {
        h.ChangeHP(-Damage);
    }
}
