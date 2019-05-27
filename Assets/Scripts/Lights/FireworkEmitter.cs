using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkEmitter : MonoBehaviour
{

    public float minTime = 5f;
    public float maxTime = 10f;
    public GameObject FireWork;

    private float startTime;
    private float timeBeforeNext;
    private Spline.BezierSpline path;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        timeBeforeNext = Random.Range(minTime, maxTime);
        path = GetComponent<Spline.BezierSpline>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > timeBeforeNext)
        {
            LaunchFirework();
            startTime = Time.time;
            timeBeforeNext = Random.Range(minTime, maxTime);
        }
    }

    private void LaunchFirework()
    {
        GameObject go = Instantiate(FireWork, transform.position, Quaternion.identity);
        go.GetComponent<Spline.Walker>().path = path;
    }

    public void TurnOff()
    {
        Destroy(gameObject);
    }
}
