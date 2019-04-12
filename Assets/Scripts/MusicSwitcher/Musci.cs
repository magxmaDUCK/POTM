using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musci : MonoBehaviour
{

    public GameObject target;

    public float dist1;
    public float dist2;

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("MusicState", "Air");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (target.transform.position - transform.position).magnitude;

        if(dist <= dist1)
        {
            AkSoundEngine.SetState("MusicState", "Close");
            //Debug.Log("close");
        }
        else if(dist <= dist2)
        {
            AkSoundEngine.SetState("MusicState", "Air");
           //Debug.Log("air");
        }
        else
        {
            AkSoundEngine.SetState("MusicState", "Far");
            //Debug.Log("far");
        }
    }
}
