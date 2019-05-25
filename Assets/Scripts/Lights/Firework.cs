using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public GameObject spark;

    private float startTime = 0;
    private float explosionTime = 3;
    private float endTime = 6;
    private bool exploded = false;

    void Start()
    {
        startTime += Time.time;
        endTime += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > explosionTime && !exploded)
        {
            for(int i = 0; i < 6; i++)
            {
                Instantiate(spark, transform.position, transform.rotation, transform);
            }
            exploded = true;
        }

        if(Time.time - startTime > endTime)
        {
            Destroy(gameObject);
        }
    }
}
