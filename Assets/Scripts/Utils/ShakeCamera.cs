using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aperture.Core.Singleton;
using Cinemachine;


public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    private CinemachineBasicMultiChannelPerlin c;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    public void Shake()
    {
        ShakeOnCamera(amplitude, frequency, time);
    }


    public void ShakeOnCamera(float amplitude, float frequency, float time)
    {
        c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = 0f;
            c.m_FrequencyGain = 0f;
        }
    }
}
