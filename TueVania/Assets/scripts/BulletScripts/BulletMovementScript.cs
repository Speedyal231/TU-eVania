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
    float addedVelocity;


    // Start is called before the first frame update
    void Start()
    {
        currentBulletExistanceTime = bulletExistanceTime;
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
        bulletObject.transform.position += (bulletSpeed + addedVelocity) * bulletObject.transform.right.normalized * Time.deltaTime;
    }

    private void HitCheck() 
    {
        Boolean hit = Physics2D.CircleCast(bulletObject.transform.position, bulletObject.transform.localScale.x/2, bulletObject.transform.right, hitRayOffset, groundLayer);
        if (hit) 
        { 
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
