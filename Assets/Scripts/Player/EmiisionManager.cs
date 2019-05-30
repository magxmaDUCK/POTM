using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmiisionManager : MonoBehaviour
{
    public GameObject body;

    public List<float> pickupTimes = new List<float>();

    private Renderer rend;
    private Color baseCol;

    private int maxIntensity = 6;

    private List<int> deleteTimes = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        rend = body.GetComponent<Renderer>();
        baseCol = rend.material.GetVector("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pickupTimes.Count; i++)
        {
            if(Time.time - pickupTimes[i] > 1f)
            {
                deleteTimes.Add(i);
            }
            else
            {

            }
        }

        foreach(int i in deleteTimes)
        {
            
        }
    }
}
