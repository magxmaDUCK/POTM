using UnityEngine;
using System.Collections;
 
public class SkyBehaviour : MonoBehaviour
{
    public GameObject kite;
    public float distance = 50f;

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
        Camera.main.transform.rotation * Vector3.up);

        transform.position = (transform.position - kite.transform.position).normalized * distance + kite.transform.position;
    }
}