using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAmbSwitch : MonoBehaviour
{
    [SerializeField] AK.Wwise.Switch ambSwitch;

    private void OnColliderEnter(Collider other)
    {
        ambSwitch.SetValue(null);
    }
}
