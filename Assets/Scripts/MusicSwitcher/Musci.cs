using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musci : MonoBehaviour
{

    public GameObject target;

    public float dist1;
    public float dist2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (target.transform.position - transform.position).magnitude;

        if(dist <= dist1)
        {
            //music1
        }
        else if(dist <= dist2)
        {
            //music2
        }
        else
        {
            //music3
        }
    }
}
