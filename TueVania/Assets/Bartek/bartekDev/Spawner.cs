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

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawned && !regenerate)
        {
            return;
        }

        spawned = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            objectToSpawn.SetActive(true);

        }
    }

 
}
