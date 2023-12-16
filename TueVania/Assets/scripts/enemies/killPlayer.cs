using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour {
	//public Animator animator;
	public levelManager level_manager;

	// Use this for initialization
	void Start () {
		level_manager = FindObjectOfType<levelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
        {
			//animator.SetTrigger("damageTAKEN");
			level_manager.RespawnPlayer();
        } 
    }
}