using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletMovementScript : MonoBehaviour
{
    //ADD bullet velocity
    [Header("Shooting Settings")]
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float hitRayOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float bulletExistanceTime;
    [SerializeField] GameObject effect;

    float currentBulletExistanceTime;
    string tag = "Breakable";
    bool gone = false;

    // Start is called before the first frame update
    void Start()
    {
        currentBulletExistanceTime = bulletExistanceTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GoneCheck();
        Count();
        Move();
        HitCheck();
        BulletTimeOutCheck();
    }

    private void Move() 
    {
        bulletObject.transform.position += (bulletSpeed) * bulletObject.transform.right.normalized * Time.deltaTime;
    }

    private void HitCheck() 
    {
        Boolean contact = Physics2D.CircleCast(bulletObject.transform.position, bulletObject.transform.localScale.x / 7, bulletObject.transform.right, hitRayOffset, groundLayer);
        RaycastHit2D hit = Physics2D.CircleCast(bulletObject.transform.position, bulletObject.transform.localScale.x / 7, bulletObject.transform.right, hitRayOffset, groundLayer);
        if (contact) 
        {

            if (hit.collider != null)
            {
                // Access the GameObject that was hit
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag(tag))
                {
                    // The hit object has the specified tag

                    BreakScript breakScript = hitObject.GetComponent<BreakScript>();

                    // Check if the script is found
                    if (breakScript != null)
                    {
                        // Now you can access methods or variables from breakScript
                        breakScript.BreakAnimPlay(); // Replace YourMethod() with the actual method you want to call
                    }
                } else if (hitObject.CompareTag("Item")){
                    
                } else if (hitObject.CompareTag("Enemy")) {
                    Debug.Log("Enemy hit");
                    IEnemy enemy = hitObject.GetComponent<IEnemy>();

                    if (enemy != null)
                    {
                        if (bulletObject.CompareTag("BigBlast")) {
                            Debug.Log("Recognized");
                            enemy.TakeDamage(20);
                        } else {
                            enemy.TakeDamage(2);
                        }
                        // Now you can call the TakeDamage method on any enemy type
                        
                    }
                }
            }
            gone = true;
        }
    }

    private void Count()
    {
        if (currentBulletExistanceTime > 0)
            currentBulletExistanceTime -= Time.fixedDeltaTime;
    }

    private void GoneCheck()
    {
        if (gone)
        {
            Instantiate(effect, bulletObject.transform.position, Quaternion.identity);
            Destroy(bulletObject);
        }
    }

    private void BulletTimeOutCheck() 
    {
        if (!(currentBulletExistanceTime > 0)) 
        {
            Destroy(bulletObject);
        }
    }

}
