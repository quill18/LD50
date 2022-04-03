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

    public float ReturnToPlayerTime = 0;    // For the boomerang.
    public float ReturnToPlayerDamping;
    public bool ReturnToPlayerSelfDestruct;
    public float ReturnToPlayerAngle = 0;
    float ElapsedTime = 0;

    Vector2 dampedVelocity;

    void Update()
    {
        if(EnemyTarget.Instance == null)
        {
            Destroy(gameObject);
            return;
        }

        if (TimeManager.IsPaused)
            return;

        ElapsedTime += Time.deltaTime;

        if(ElapsedTime > LifeSpan)
        {
            Destroy(gameObject);
        }

        if (ReturnToPlayerTime > 0)
        {
            if (ElapsedTime > ReturnToPlayerTime)
            {
                Vector2 dirToPlayer = Quaternion.Euler(0, 0, ReturnToPlayerAngle) * (EnemyTarget.Instance.transform.position - this.transform.position);

                if(ReturnToPlayerSelfDestruct && dirToPlayer.magnitude <= 0.5f)
                {
                    Destroy(gameObject);
                }

                dirToPlayer = dirToPlayer.normalized * Velocity.magnitude;

                rb.velocity = Vector2.SmoothDamp(rb.velocity, dirToPlayer, ref dampedVelocity, ReturnToPlayerDamping);
            }
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
