using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitePostEventWwise : MonoBehaviour
{
    public void PlayFold()
    {
        AkSoundEngine.PostEvent("Play_Fold", gameObject);
    }
    public void PlayUnFold()
    {
        AkSoundEngine.PostEvent("Play_Unfold", gameObject);
    }
}
