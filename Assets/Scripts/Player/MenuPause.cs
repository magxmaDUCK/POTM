using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuPause : MonoBehaviour
{
    public float vignetteIntensityAdded = 0.2f;
    public float vignetteIntensitySpeed = 2f;
    public float depthOfFieldApertureSpeed = 2f;
    public float timeScaleMin = 0.1f;
    public float timeScaleSpeed = 0.1f;
    public GameObject MenuPauseCanvas;


    private bool pressPause = false;
    private Vignette vignetteLayer;
    private DepthOfField depthOfFieldLayer;
    private float timeScaleInit = 1.0f;
    private float timeLerp = 0.0f;
    private float vignetteIntensityInit;
    private float depthOfFieldApertureInit;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vignetteLayer);
        volume.profile.TryGetSettings(out depthOfFieldLayer);

        vignetteIntensityAdded += vignetteLayer.intensity.value;
        vignetteIntensityInit = vignetteLayer.intensity.value;

        depthOfFieldApertureInit = depthOfFieldLayer.aperture.value;

        MenuPauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && pressPause == false)
        {
            pressPause = true;
        }

        else if(Input.GetKeyDown("space") && pressPause == true && Time.timeScale < 1)
        {
            pressPause = false;
        }

        if(pressPause == true)
        {
            Pause();
        }

        if(pressPause == false)
        {
            PauseQuit();
        }
    }

    public void Pause()
    {
        timeLerp = Mathf.Clamp01(timeLerp);
        timeLerp += timeScaleSpeed * Time.unscaledDeltaTime;

        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleMin, timeLerp);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if(vignetteLayer.intensity.value < vignetteIntensityAdded)
        {
            vignetteLayer.intensity.value += vignetteIntensitySpeed * Time.deltaTime;
        }

        if(depthOfFieldLayer.aperture.value > 0.1f)
        {
            depthOfFieldLayer.aperture.value -= depthOfFieldApertureSpeed * Time.deltaTime;
        }

        MenuPauseCanvas.SetActive(true);
    }

    public void PauseQuit()
    {
        timeLerp = Mathf.Clamp01(timeLerp);
        timeLerp -= timeScaleSpeed * Time.unscaledDeltaTime * 0.5f;

        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleMin, timeLerp);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if(vignetteLayer.intensity.value > vignetteIntensityInit)
        {
            vignetteLayer.intensity.value -= vignetteIntensitySpeed * Time.deltaTime * 0.5f;
        }

        if(depthOfFieldLayer.aperture.value < depthOfFieldApertureInit)
        {
            depthOfFieldLayer.aperture.value += depthOfFieldApertureSpeed * Time.deltaTime * 0.5f;
        }

        MenuPauseCanvas.SetActive(false);
    }
}
