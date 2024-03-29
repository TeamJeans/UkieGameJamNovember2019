﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollectable : MonoBehaviour
{
	[SerializeField] private uint multiplierToAdd = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			GameManager.Instance.AddToMultiplier(multiplierToAdd);
			gameObject.SetActive(false);
		}
	}
}
