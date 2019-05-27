
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class FireworkSpark : MonoBehaviour
{
    public float minVertSpeed, maxVertSpeed;
    public float minHorSpeed, maxHorSpeed;

    public float duration = 2f;

    //public GameObject trail;
    private VisualEffect trailFX;

    private float startTime = 0f;

    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(minHorSpeed, maxHorSpeed), Random.Range(minVertSpeed, maxVertSpeed), Random.Range(minHorSpeed, maxHorSpeed)),ForceMode.Impulse);
        startTime = Time.time;
        trailFX = GetComponent<VisualEffect>();
        mat = GetComponent<Renderer>().material;
        trailFX.SetVector4("Col", mat.GetColor("_EmissionColor") * mat.GetFloat("_DisolveLerp"));
    }

    private void Update()
    {
        trailFX.SetVector3("pos", transform.position);

        trailFX.SetVector4("Col", mat.GetColor("_EmissionColor") * mat.GetFloat("_DisolveLerp"));

        if (Time.time - startTime > duration)
        {
            Destroy(gameObject);
        }
    }

}
