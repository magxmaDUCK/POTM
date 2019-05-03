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

        transform.Rotate(new Vector3(60, 0, 0));
        transform.Rotate(new Vector3(0, turn * HorizontalSens * Time.deltaTime, 0));
        transform.Rotate(new Vector3(-60, 0, 0));
    }
}
