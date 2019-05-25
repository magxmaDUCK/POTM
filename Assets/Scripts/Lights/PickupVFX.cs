using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PickupVFX : MonoBehaviour
{
    [SerializeField]public Transform player;

    [SerializeField]public Material mat;

    private VisualEffect lightFX;

    private Vector3 position;

    private float startTime = 0;

    public float duration = 1.0f;

    float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lightFX = GetComponent<VisualEffect>();
        position = transform.position;

        //SET VFX COLOR TO MATERIAL COLOR
        GradientAlphaKey[] keysA= new GradientAlphaKey[4];

        keysA[0].alpha = 0;
        keysA[0].time = 0;
        keysA[1].alpha = 0;
        keysA[1].time = 1;

        keysA[2].alpha = 255;
        keysA[2].time = 0.29f;
        keysA[3].alpha = 255;
        keysA[3].time = 0.77f;

        GradientColorKey[] keysC = new GradientColorKey[2];

        keysC[0].color = mat.color;
        keysC[0].time = 0;

        keysC[1].color = Color.white;
        keysC[1].time = 1;

        Gradient g = new Gradient();
        g.SetKeys(keysC, keysA);

        lightFX.SetGradient("lightGradiant", g);

        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed = Time.time - startTime;
        transform.position = Vector3.Lerp(position, player.transform.position, timePassed / duration);

        if(timePassed > duration)
        {
            lightFX.SendEvent("OnStop");
            transform.position = player.transform.position;
        }

        if(timePassed > duration + 2f)
        {
            Destroy(gameObject);
        }
    }
}