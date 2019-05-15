using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PickupVFX : MonoBehaviour
{
    [SerializeField]public Transform player;

    private VisualEffect lightFX;

    // Start is called before the first frame update
    void Start()
    {
        lightFX = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lightFX.SetVector3("AttractiveTargetPosition", player.position);
    }
}
