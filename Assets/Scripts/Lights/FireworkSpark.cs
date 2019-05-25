
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkSpark : MonoBehaviour
{
    public float minVertSpeed, maxVertSpeed;
    public float minHorSpeed, maxHorSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(minHorSpeed, maxHorSpeed), Random.Range(minVertSpeed, maxVertSpeed), Random.Range(minHorSpeed, maxHorSpeed)),ForceMode.Impulse);
    }
}
