using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform FollowTarget;

    // Update is called once per frame
    void Update()
    {
        if (FollowTarget == null)
            return;

        this.transform.position = new Vector3( FollowTarget.position.x, FollowTarget.position.y, this.transform.position.z );
    }
}
