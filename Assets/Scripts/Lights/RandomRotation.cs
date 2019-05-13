using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,0,Random.Range(0,360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
