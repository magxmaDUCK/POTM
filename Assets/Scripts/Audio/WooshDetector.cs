using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooshDetector : MonoBehaviour
{
    bool hasWoosh = false;
    GameObject wooshParent = null;
    public AK.Wwise.Event Event = new AK.Wwise.Event();

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wooshable") && !hasWoosh)
            {
                Debug.Log("Woosh");
                collision.gameObject.AddComponent<EventPositionConfiner>();
                collision.gameObject.GetComponent<EventPositionConfiner>().setEvent(Event);
                hasWoosh = true;
                wooshParent = collision.gameObject;
                Debug.Log("WooshING");
             }            
}
    private void OnTriggerExit(Collider other)
{
    //if wooshable exit sound;
    if (other.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
    {
            if (other.gameObject.GetComponent<EventPositionConfiner>() != null)
            {
                AkSoundEngine.PostEvent("Play_Out_Woosh", other.transform.GetChild(0).gameObject);
                Destroy(other.gameObject.GetComponent<EventPositionConfiner>());
                Invoke("killWoosh", 2f);
            }
        }
    }
private void killWoosh()
    {
        Destroy(wooshParent.transform.GetChild(0).gameObject);
        hasWoosh = false;
        Debug.Log("WooshDEAD");
    }
}
