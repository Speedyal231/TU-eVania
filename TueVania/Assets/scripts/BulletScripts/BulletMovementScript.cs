using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletMovementScript : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float hitRayOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float bulletExistanceTime;

    float currentBulletExistanceTime;
    Vector3 initialVelocity;
    float addedVelocity;
    string tag = "Breakable";


    // Start is called before the first frame update
    void Start()
    {
        currentBulletExistanceTime = bulletExistanceTime;
        initialVelocity = rbPlayer.velocity;
        addedVelocity = rbPlayer.velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Count();
        Move();
        HitCheck();
        BulletTimeOutCheck();
    }

    private void Move() 
    {
        bulletObject.transform.position += (bulletSpeed) * bulletObject.transform.right.normalized * Time.deltaTime + initialVelocity;
    }

    private void HitCheck() 
    {
        Boolean contact = Physics2D.CircleCast(bulletObject.transform.position, bulletObject.transform.localScale.x/2, bulletObject.transform.right, hitRayOffset, groundLayer);
        RaycastHit2D hit = Physics2D.CircleCast(bulletObject.transform.position, bulletObject.transform.localScale.x / 2, bulletObject.transform.right, hitRayOffset, groundLayer);
        if (contact) 
        {
            if (hit.collider != null)
            {
                // Access the GameObject that was hit
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag(tag))
                {
                    // The hit object has the specified tag
                    Destroy(hitObject);
                }
            }
            Destroy(bulletObject);
        }
    }

    private void Count()
    {
        if (currentBulletExistanceTime > 0)
            currentBulletExistanceTime -= Time.fixedDeltaTime;
    }

    private void BulletTimeOutCheck() 
    {
        if (!(currentBulletExistanceTime > 0)) 
        {
            Destroy(bulletObject);
        }
    }

}
