using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float cooldown = 1;
    float timeSinceLastAttack = 0;
    float attackRadius = 2;
    float attackDamage = 1;

    public LayerMask LayerMask;

    public GameObject AttackAnimation;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack < cooldown)
            return;

        // Do attack.
        Debug.Log("ATTACK!");
        timeSinceLastAttack = 0;

        AttackAnimation.SetActive(true);

        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, attackRadius, LayerMask);
        foreach(Collider2D col in cols)
        {
            Health health = col.GetComponentInParent<Health>();
            if (health == null)
                continue;

            health.ChangeHP(-attackDamage);
        }
    }
}
