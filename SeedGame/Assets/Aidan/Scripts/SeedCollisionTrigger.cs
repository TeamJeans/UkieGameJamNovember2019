using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollisionTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 seedOffsetFromDirt = Vector2.zero;
	private GameObject lastFlowerHead;
    [SerializeField] private GameObject treePrefab;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dirt"))
        {
            // Grow new flower
            Debug.Log("Touching dirt");

			// Grow flower on dirt if we haven't already
			collision.GetComponent<GrowFlower>().Grow();

            // Set this piece of dirt as the new checkpoint
			lastFlowerHead = collision.GetComponent<GrowFlower>().spawnedFlower.transform.Find("SeedSpawnPosition").gameObject;
			GameManager.Instance.lastFlowerHead = lastFlowerHead;
			GameManager.Instance.PlantSeed(lastFlowerHead);
        }

        if (collision.CompareTag("FinishLine"))
        {
            // Grow flower on dirt if we haven't already
            Instantiate(treePrefab, transform.position, Quaternion.identity);
            Debug.Log("POS " + transform.position);
        }

        if (collision.CompareTag("Wall"))
        {
            // Respawn at last checkpoint
            Debug.Log("Touching wall");

            // Reset the player's multiplier
            GameManager.Instance.ResetMultiplier();

			// Send the seed back to the last checkpoint
			//Debug.Log(lastFlowerHead);
			GameManager.Instance.RespawnSeedAtLastCheckpoint();

			Debug.Log("Respawn Seed");
        }

        if (collision.CompareTag("FinishLine"))
        {
            // Reached the end
            MenuManager.Instance.EndReached();
        }
    }
}
