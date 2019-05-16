using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PickupVFX : MonoBehaviour
{
    [SerializeField]public Transform player;

    private VisualEffect lightFX;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        lightFX = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = lightFX.GetVector3("");

        lightFX.SetVector3("AttractiveTargetPosition", player.position);
    }
}
