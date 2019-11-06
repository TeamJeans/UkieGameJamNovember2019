using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowFlower : MonoBehaviour
{
	[SerializeField] private GameObject flowerPrefab;

	public GameObject spawnedFlower = null;
	bool flowerHasSpawned = false;

    public void Grow()
	{
		if (!flowerHasSpawned)
		{
			flowerHasSpawned = true;
			spawnedFlower = Instantiate(flowerPrefab, transform.position, transform.rotation);
		}
	}

	public void DestroyFlower()
	{
		if (flowerHasSpawned)
		{
			flowerHasSpawned = false;
			Destroy(spawnedFlower);
		}
	}
}
