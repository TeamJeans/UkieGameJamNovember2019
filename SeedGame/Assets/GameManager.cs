using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI scoreText = null;
	[SerializeField] private Transform seedTransform = null;

    private uint currentMultiplier = 1;
	private float currentSeedHeight = 0f;
	private float upwardDistanceTraveled = 0f;
	private int currentScore = 0;
	private Vector2 seedStartingPos = Vector2.zero;

	private void Awake()
	{
		// Makes sure that this is the only instance of this object in the scene
		if (Instance != null)
		{
			if (Instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		// Initialise seed start position
		seedStartingPos = seedTransform.position;
	}

	private void Update()
	{
		// Get the current seed height
		currentSeedHeight = seedTransform.position.y - seedStartingPos.y;

		// Calculate the distance upward the seed has traveled
		if ((currentSeedHeight + currentMultiplier) > upwardDistanceTraveled)
		{
			upwardDistanceTraveled = currentSeedHeight + currentMultiplier;
		}

		// Calculate the score
		currentScore = (int)((upwardDistanceTraveled) * 10);

		// Update score text
		scoreText.text = "" + currentScore;
		if (currentMultiplier > 1)
		{
			scoreText.text = "" + currentScore + "x" + currentMultiplier;
		}


	}

	public void AddToMultiplier(uint multiplierToAdd)
	{
		// Increase the number of collectables collected
		currentMultiplier += multiplierToAdd;

		// SFX: Collectable pickup sound
	}

	public void ResetMultiplier()
	{
		currentMultiplier = 1;
	}

	public void ResetGame()
	{
		//TODO: Reset the collectibles
		seedTransform.position = new Vector2(seedStartingPos.x, seedStartingPos.y);
	}
}
