using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    public float HorizontalSens;

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = Input.GetAxis("Horizontal");
        Vector3 rot = transform.rotation.eulerAngles;

        transform.Rotate(-rot);
        transform.Rotate(new Vector3(0, turn * HorizontalSens * Time.deltaTime, 0));
        transform.Rotate(rot);
        
        
    }
}
