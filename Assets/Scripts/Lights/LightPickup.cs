using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPickup : MonoBehaviour
{

    private bool on = true;

    private int scoreValue = 1;



    public void TurnOff()
    {
        on = false;
    }

    public int getScoreValue()
    {
        return scoreValue;
    }

    public bool IsOn()
    {
        return on;
    }
}
