using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private bool regenerate;
    private bool spawned;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private GameObject blockadeToSpawn;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip bossMusic;

    private void Start()
    {
        spawned = false;

        GetComponent<SpriteRenderer>().enabled = false;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        else
        {
            Debug.Log("the Spawner currently has no SpriteRenderer assigned.");
        }

        SpriteRenderer spawnPointRenderer = spawnPoint.GetComponent<SpriteRenderer>();
        if (spawnPointRenderer != null )
        {
            spawnPointRenderer.enabled = false;
        } else
        {
            Debug.Log("the SpawnPoint child of Spawner currently has no SpriteRenderer assigned.");
        }

        objectToSpawn.SetActive(false);
        blockadeToSpawn.SetActive(false);


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectToSpawn != null){
            Debug.Log("Collision Detected");
            if (spawned && !regenerate)
            {
                objectToSpawn.SetActive(true);
                blockadeToSpawn.SetActive(true);
                regenerate = false;
                objectToSpawn.transform.position = spawnPoint.transform.position;
                Debug.Log("Regen");
            }

            spawned = true;
            if (collision.gameObject.CompareTag("Player"))
            {
                objectToSpawn.SetActive(true);
                this.GetComponent<Collider2D>().enabled = false;
                switchMusic(bossMusic);
                blockadeToSpawn.SetActive(true);
                Debug.Log("First time");

            }
        }
    }

    private void switchMusic(AudioClip one)
    {
        src.Pause();
        src.clip = one;
        src.Play();
    }

 
}
