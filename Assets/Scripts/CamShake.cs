using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamShake : MonoBehaviour
{

    [Tooltip("If rotation shake, max degrees of the rotation. Else, max movement in units (meters)")]
    public float shakeStrength;
    [Tooltip("Speed of the camera shake")]
    public float shakeSpeed;
    private float perlinX = 1f;
    private float perlinY = 1f;

    private ColorGrading colorgradingLayer;
    public float postExposureFX;

    private Vector3 pos = Vector3.zero;

    public bool camShake = false;

    private void Start()
    {
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorgradingLayer);
        pos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = pos + (camShake?CameraShake() : Vector3.zero);
        colorgradingLayer.postExposure.value = postExposureFX;

    }

    public Vector3 CameraShake()
    {
        float perlinValueX = Mathf.PerlinNoise(Time.time * shakeSpeed * perlinX, Time.time * shakeSpeed * perlinX) - 0.5f;
        float perlinValueY = Mathf.PerlinNoise(Time.time * shakeSpeed * perlinY + 1.0f, Time.time * shakeSpeed * perlinY + 1.0f) - 0.5f;

        return new Vector3(perlinValueX * shakeStrength, perlinValueY * shakeStrength, 0);
    }
}
