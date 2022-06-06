using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public static class CoinSoundController
{
    static float startTime;
    static float pitch;
    static float maxPitch = 2f;
    static float coinComboBreakTime = 1f;
    public static void Play()
    {
        if (Time.fixedTime - startTime > coinComboBreakTime)
        {
            pitch = 1;
        }
        else
        {
            pitch += 0.05f;
        }

        startTime = Time.fixedTime;
        AudioSource a = AudioManager.PlaySound(Sounds.CoinPickup);
        a.pitch = Mathf.Min(pitch, maxPitch);
    }
}
