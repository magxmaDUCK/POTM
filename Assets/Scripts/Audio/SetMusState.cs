using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusState: MonoBehaviour
{
    [SerializeField] AK.Wwise.State ambState;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            ambState.SetValue();
        }
    }
}
