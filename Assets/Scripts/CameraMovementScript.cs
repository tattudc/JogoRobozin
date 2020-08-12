using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.y >= 5) {
			transform.position = new Vector3 (player.position.x, (player.position.y - 5.0f), transform.position.z);
		} else if (player.position.y <= -3) {
			if ((player.position.y + 3.0f) > -3.4f) {
				transform.position = new Vector3 (player.position.x, (player.position.y + 3.0f), transform.position.z);
			} else {
				transform.position = new Vector3 (player.position.x, -3.4f, transform.position.z);
			}
		 } else
			transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
	}
}
