using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAmbSwitch : MonoBehaviour
{
    [SerializeField] AK.Wwise.Switch ambSwitch;
    
    private void OnTriggerEnter(Collider other)
    {
        ambSwitch.SetValue(GameObject.Find("WwiseHandler"));
    }
}
