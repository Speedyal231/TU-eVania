using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    [SerializeField] AnimationControlScript script;

    [SerializeField] BoxCollider2D boxCollider;
    float currentAnimTime;
    bool broke;
    const string Idle = "idle";
    const string Break = "Break";

    void Start()
    {
        boxCollider.enabled = true;
        broke = false;
    }

    private void Count()
    {
        if (currentAnimTime > 0)
            currentAnimTime -= Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        Count();
        if (broke && !(currentAnimTime > 0))
        {
            Destroy(gameObject);
        }
        if (broke && !(currentAnimTime > 0.25))
        {
            boxCollider.enabled = false;
        }
    }

    public void BreakAnimPlay()
    {
        script.ChangeAnimationState(Break);
        broke = true;
        currentAnimTime = 0.3f;
    }
}
