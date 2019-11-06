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
	[SerializeField] private CameraFollow cameraFollow = null;
	[SerializeField] private GameObject seedPrefab = null;
	[SerializeField] private Transform dirtPatches = null;

	public GameObject lastFlowerHead;
    private uint currentMultiplier = 1;
	private float currentSeedHeight = 0f;
	private float maxUpwardDistanceTraveled = 0f;
	private int currentScore = 0;
	public bool flowerReady = false;
	private Vector3 seedSpawnPosition;

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


        if (Input.GetMouseButtonDown(1))
        {
                SpawnSeed();
        }
        

		
	}


    public void SpawnSeed()
    {
        if (flowerReady)
        {
            flowerReady = false;
            SpawnSeedAtFlowerHead();
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

		// Reset flowers
		foreach(Transform t in dirtPatches)
		{
			t.GetComponent<GrowFlower>().DestroyFlower();
		}

		// Reset seed positions
		//seedTransform.position = new Vector2(seedStartingPos.position.x, seedStartingPos.position.y);
		Destroy(seedTransform.gameObject);

		GameObject newSeed = Instantiate(seedPrefab, seedStartingPos.position, Quaternion.identity);
		seedTransform = newSeed.transform.Find("SeedBase").transform;
		cameraFollow.player = newSeed.transform.Find("SeedBase").gameObject;
		Debug.Log("ResetGame");

		// Reset score and multiplier
		maxUpwardDistanceTraveled = 0;
		currentMultiplier = 1;
	}

	public void RespawnSeedAtLastCheckpoint()
	{
		Debug.Log("RESPAWN");
		Destroy(seedTransform.gameObject);

		cameraFollow.player = lastFlowerHead;
		seedSpawnPosition = lastFlowerHead.transform.position;
		seedTransform = seedStartingPos;

		flowerReady = true;
	}

	public void PlantSeed(GameObject flowerHead)
	{
		Destroy(seedTransform.gameObject);
		cameraFollow.player = flowerHead;
		seedSpawnPosition = flowerHead.transform.position;
		seedTransform = seedStartingPos;

		StartCoroutine(WaitForFlowerAnim());
	}

	IEnumerator WaitForFlowerAnim()
	{
		yield return new WaitForSeconds(2.5f);

		flowerReady = true;
	}

	public void SpawnSeedAtFlowerHead()
	{
		GameObject newSeed = Instantiate(seedPrefab, seedSpawnPosition, Quaternion.identity);
		seedTransform = newSeed.transform.Find("SeedBase").transform;
		cameraFollow.player = newSeed.transform.Find("SeedBase").gameObject;
		Debug.Log("Spawned seed");
	}
}
