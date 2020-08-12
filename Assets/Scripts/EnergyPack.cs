using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPack : MonoBehaviour {

	public float healthRecovered = 25f;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Player")) {
			col.gameObject.GetComponent<PlayerController>().HealthChange(healthRecovered);
			Destroy(gameObject);
		}
	}

}
