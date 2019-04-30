using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Rendering.PostProcessing;

public class MenuPause : MonoBehaviour
{
    public float vignetteIntensityMax = 0.6f;
    public float vignetteIntensitySpeed = 0.1f;
    public float timeScaleMin = 0.1f;
    public float timeScaleSpeed = 0.1f;

    private PostProcessProfile profile;
    private bool inPause = false;
    private Vignette vignetteLayer;
    private float vignetteIntensityInit;
    private float timeScaleInit = 1.0f;
    private float timeLerp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            inPause = true;
        }

        if(inPause == true)
        {
            Pause();
        }
    }

    public void Pause()
    {
        timeLerp = Mathf.Clamp01(timeLerp);
        timeLerp += timeScaleSpeed * Time.unscaledDeltaTime;

        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleMin, timeLerp);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if(vignetteLayer.intensity.value < vignetteIntensityMax)
        {
            vignetteLayer.intensity.value += vignetteIntensitySpeed * Time.deltaTime;
        }
    }
}
