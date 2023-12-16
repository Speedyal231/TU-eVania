using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class flyingChaseEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, player.transform.position) <= 0.5f){
            //change if u wanna slow it dwon
        }else{
            //sonra geri duzelt
        }
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if(chase == true){
            Chase();
        }else{
            ReturnStartPoint();
        }
        Flip();
    }
    private void ReturnStartPoint(){
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
}