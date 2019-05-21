using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PickupVFX : MonoBehaviour
{
    [SerializeField]public Transform player;

    private VisualEffect lightFX;

    private Vector3 position;

    private float startTime = 0;

    public float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        lightFX = GetComponent<VisualEffect>();
        position = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float timePassed = Time.time - startTime;
        transform.position = Vector3.Lerp(position, player.transform.position, timePassed / duration);
        lightFX.SetVector3("AttractiveTargetPosition", player.position);

        if(timePassed > duration)
        {
            lightFX.SendEvent("OnStop");
        }

        if(timePassed > duration + 2f)
        {
            Destroy(this.gameObject);
        }
    }
}