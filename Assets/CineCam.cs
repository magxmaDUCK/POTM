using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCam : MonoBehaviour
{
    public Camera cineCam;
    public Camera fpCam;

    public void SwitchCam()
    {
        cineCam.gameObject.SetActive(false);
        //GetComponent<Camera>().enabled = true;
        fpCam.gameObject.SetActive(true);
    }
}
