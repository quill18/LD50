using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject TreasureChestTarget;

    Plane[] planes = new Plane[6];

    // Update is called once per frame
    void Update()
    {
        if (EnemyTarget.Instance == null)
            return;

        if(TreasureChestTarget == null)
        {
            // Chest must have been collected.
            Destroy(gameObject);
            return;
        }

        GeometryUtility.CalculateFrustumPlanes(Camera.main, planes);

        Vector3 playerPos = EnemyTarget.Instance.transform.position;
        Vector3 dir = TreasureChestTarget.transform.position - playerPos;

        Ray ray = new Ray(playerPos, dir);

        float dist = Mathf.Infinity;
        Vector3 hitPoint = Vector3.zero;

        for (int i = 0; i < 4; i++)
        {
            if( planes[i].Raycast(ray, out float d) )
            {
                if (d < dist)
                {
                    hitPoint = ray.GetPoint(d);
                    dist = d;
                }
            }
        }

        // hitPoint contains the edge of the screen towards the chest.

        this.transform.position = hitPoint;

        Vector3 facingVector = TreasureChestTarget.transform.position - this.transform.position;

        if (facingVector.sqrMagnitude > 0)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(facingVector.y, facingVector.x);
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
