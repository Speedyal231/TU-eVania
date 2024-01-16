using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	//public GameObject deathParticle;
	public GameObject spawnParticle;
	public GameObject player;
    public float delay;
	public healthManager healthManager;

    // Use this for initialization
    void Start () {

		//kamera = FindObjectOfType<cameraController>();
		healthManager = FindObjectOfType<healthManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RespawnPlayer()
    {
		StartCoroutine("RespawnPlayerCo");	
    }
    public IEnumerator RespawnPlayerCo()
    { 
		//Instantiate(deathParticle, player.transform.position, player.transform.rotation);
//		player.enabled = false; 
 //       player.GetComponent<Renderer>().enabled = false;
//		kamera.isFollowing = false;

        //player.GetComponent<Rigidbody2D>().velocity= Vector2.zero;
//		Score_Manager.AddPoints(-penalty);
		Debug.Log("Player Respawn");
        yield return new WaitForSeconds(delay);
		player.transform.position = currentCheckpoint.transform.position;
//		player.transform.position = currentCheckpoint.transform.position;
//		player.knockbackCount = 0;
//		player.enabled = true;
//		player.GetComponent<Renderer>().enabled = true;
		healthManager.fullHealth();
		healthManager.isDead = false;
//		kamera.isFollowing = true;
		Instantiate(spawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);

	}
}