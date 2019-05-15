using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    [Min(0)]
    public float islandRadius;

    private float farPlane;
    
    // Update is called once per frame
    void Update()
    {
        if( Vector2.Distance(Vector2.zero, new Vector2(transform.position.x, transform.position.z)) > (islandRadius + farPlane))
        {
            Vector3 dir = (Vector3.zero - transform.position).normalized;
            transform.position = new Vector3(dir.x * (farPlane + islandRadius), transform.position.y, dir.z*(farPlane + islandRadius));
        }
    }
}
