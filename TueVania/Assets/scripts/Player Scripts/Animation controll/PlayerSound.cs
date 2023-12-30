using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;

    float currentClipDuration;
    public void PlaySound(int index, AudioSource src)
    {
        src.clip = clip[index];
        src.Play();
    }

    public void PlaySoundfixedLoop(int index, float waitMult, AudioSource src)
    {
        if (!(currentClipDuration > 0) || src.clip != clip[index])
        {
            src.clip = clip[index];
            currentClipDuration = src.clip.length * waitMult;
            src.Play();
        }
        
    }

    public void PlaySoundUninterupted(int index, float waitMult, AudioSource src)
    {
        if (!(currentClipDuration > 0))
        {
            src.clip = clip[index];
            currentClipDuration = src.clip.length * waitMult;
            src.Play();
        }

    }

    private void Count()
    {
        if (currentClipDuration > 0)
            currentClipDuration -= Time.fixedDeltaTime;
        
    }

    private void FixedUpdate()
    {
        Count();
    }
}
