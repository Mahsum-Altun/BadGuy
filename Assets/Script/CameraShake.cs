using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    Cinemachine.CinemachineFreeLook cinemachineFreeLook;
    float shakeTimer;
    public float shakeTimerSpeed;
    //float shakeTimerTotal;
    //float startingIntensity;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinemachineFreeLook = GetComponent<Cinemachine.CinemachineFreeLook>();
    }

    // Update is called once per frame
    public void ShakeCamera(float intensity, float time)
    {
        Cinemachine.CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineFreeLook.GetComponentInChildren<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        //startingIntensity = intensity;
        //shakeTimerTotal = time;
        shakeTimer = time;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime * shakeTimerSpeed;
            if (shakeTimer <= 0f)
            {
                //Time Over
                Cinemachine.CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineFreeLook.GetComponentInChildren<Cinemachine.CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f; //Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
            }
        }
    }
}
