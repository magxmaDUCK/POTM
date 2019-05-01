using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuPause : MonoBehaviour
{
    public float vignetteIntensityAdded = 0.2f;
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
        depthOfFieldLayer.active = false;

        MenuPauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Start") && pressPause == false)
        {
            pressPause = true;
        }

        else if(Input.GetButtonDown("Start") && pressPause == true)
        {
            pressPause = false;
        }

        if(pressPause == true && timeLerp < 1)
        {
            Pause();
        }

        if(pressPause == false)
        {
            if(timeLerp > 0)
            {
                PauseQuit();
            }
            
            else if(timeLerp <= 0)
            {
                depthOfFieldLayer.active = false;
            }
        }
    }

    public void Pause()
    {
        timeLerp = Mathf.Clamp01(timeLerp);
        timeLerp += timeScaleSpeed * Time.unscaledDeltaTime;

        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleMin, timeLerp);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        vignetteLayer.intensity.value = Mathf.Lerp(vignetteIntensityInit, vignetteIntensityAdded, timeLerp);

        depthOfFieldLayer.active = true;
        depthOfFieldLayer.aperture.value = Mathf.Lerp(depthOfFieldApertureInit, 0.05f, timeLerp);

        MenuPauseCanvas.SetActive(true);
    }

    public void PauseQuit()
    {
        timeLerp = Mathf.Clamp01(timeLerp);
        timeLerp -= timeScaleSpeed * Time.unscaledDeltaTime * 0.5f;

        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleMin, timeLerp);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        vignetteLayer.intensity.value = Mathf.Lerp(vignetteIntensityInit, vignetteIntensityAdded, timeLerp);

        depthOfFieldLayer.aperture.value = Mathf.Lerp(depthOfFieldApertureInit, 0.05f, timeLerp);

        MenuPauseCanvas.SetActive(false);
    }
}
