using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI scoreText = null;
	[SerializeField] private Transform seedTransform = null;
	[SerializeField] private Transform seedStartingPos = null;
	[SerializeField] private Transform collectables = null;

    private uint currentMultiplier = 1;
	private float currentSeedHeight = 0f;
	private float maxUpwardDistanceTraveled = 0f;
	private int currentScore = 0;

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
		seedTransform.position = new Vector2(seedStartingPos.position.x, seedStartingPos.position.y);
	}

	private void Update()
	{
		// Get the current seed height
		currentSeedHeight = seedTransform.position.y - seedStartingPos.position.y;

		// Calculate the distance upward the seed has traveled
		if ((currentSeedHeight + currentMultiplier) > maxUpwardDistanceTraveled)
		{
			maxUpwardDistanceTraveled = currentSeedHeight + currentMultiplier;
		}

		// Calculate the score
		currentScore = (int)((maxUpwardDistanceTraveled) * 10);

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
		// Reset the collectibles
		foreach( Transform t in collectables)
		{
			t.gameObject.SetActive(true);
		}

		// Reset seed positions
		seedTransform.position = new Vector2(seedStartingPos.position.x, seedStartingPos.position.y);

		// Reset score and multiplier
		maxUpwardDistanceTraveled = 0;
		currentMultiplier = 1;
	}
}
