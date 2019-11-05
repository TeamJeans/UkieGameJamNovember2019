﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollisionTrigger : MonoBehaviour
{
	[SerializeField] private Vector2 seedOffsetFromDirt = Vector2.zero;
	private Vector2 currentCheckpointPosition = Vector2.zero;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Dirt")
		{
			// Grow new flower
			Debug.Log("Touching dirt");

			// Set this piece of dirt as the new checkpoint
			currentCheckpointPosition = collision.transform.position;
		}

		if (collision.tag == "Wall")
		{
			// Respawn at last checkpoint
			Debug.Log("Touching wall");

			// Reset the player's multiplier
			GameManager.Instance.ResetMultiplier();

			// Send the seed back to the last checkpoint
			transform.parent.position = new Vector2(currentCheckpointPosition.x + seedOffsetFromDirt.x, currentCheckpointPosition.y + seedOffsetFromDirt.y);
		}

		if (collision.tag == "FinishLine")
		{
			// Reached the end
			MenuManager.Instance.EndReached();
		}
	}
}
