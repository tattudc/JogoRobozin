using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLossTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			col.gameObject.GetComponent<PlayerController>().Break();
		}
	}
}
