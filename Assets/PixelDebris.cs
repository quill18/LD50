using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDebris : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FloorY = ExplosionPoint.y + Random.Range(-0.75f, 0.25f);

        lifespan = Random.Range(1f, 2f);

        velocity = new Vector2(  Random.Range(-ExplosionForce.x, ExplosionForce.x), Random.Range(0, ExplosionForce.y) );
    }

    public Vector2 ExplosionPoint;

    float FloorY;

    Vector2 ExplosionForce = new Vector2(2, 4);
    Vector2 velocity;

    private float gravity = 10f;
    private float drag = 0.99f;

    float lifespan;

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if(lifespan < 0)
        {
            Destroy(gameObject);
            return;
        }

        velocity *= drag;
        velocity.y -= gravity * Time.deltaTime;

        Vector3 pos = this.transform.position + (Vector3)(velocity * Time.deltaTime);
        if(pos.y < FloorY)
        {
            velocity.y *= -0.8f;
            pos.y = FloorY;
        }

        this.transform.position = pos;
    }
}
