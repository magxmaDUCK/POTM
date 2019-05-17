using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusState: MonoBehaviour
{
    [SerializeField] AK.Wwise.State ambState;

    private void OnTriggerEnter(Collider other)
    {        
        {
            ambState.SetValue();
        }       
    }
}
