using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoundaryTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
		 	if (col.gameObject.GetComponent<PlayerController>().isGrounded()) {
				col.gameObject.GetComponent<PlayerController>().LevelEnd();
			}
		}
	}
}
