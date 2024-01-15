using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsScript : MonoBehaviour
{
    
    [SerializeField] GameObject effect;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip clip;
    private string currentState;
    float currentAnimTime;
    [SerializeField] int animation;
    bool done;

    // Start is called before the first frame update

    void Start()
    {
        done = false;
        if (animation == 1)
        {
            HitEffect();
            src.PlayOneShot(clip);
        }
        else if (animation == 2)
        {
            GetEffect();
            src.PlayOneShot(clip);
        }
        else if (animation == 3)
        {
            PowerUpEffect();
            src.PlayOneShot(clip);
        }
    }

    const string hit = "hit";
    const string get = "Get";
    const string powerUp = "Powerup";

    private void HitEffect()
    {
        ChangeAnimationState(hit);
        currentAnimTime = 0.2f;
        done = true;
    }

    private void GetEffect()
    {
        ChangeAnimationState(get);
        currentAnimTime = 0.2f;
        done = true;
    }

    private void PowerUpEffect()
    {
        ChangeAnimationState(powerUp);
        currentAnimTime = 0.4f;
        done = true;
    }

    private void Count()
    {
        if (currentAnimTime > 0)
            currentAnimTime -= Time.fixedDeltaTime;
    }
    private void Update()
    {
        Count();
        if (currentAnimTime <= 0 && done)
        {
            Destroy(effect);
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    
}
