using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatformController : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 endPosition;

	public float movingDist = 20f;

	public float secs = 5f;

	// Use this for initialization
	void Awake () {
		startPosition = transform.position;
		endPosition = transform.position + (movingDist*Vector3.right);
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.Lerp (startPosition, endPosition, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.timeSinceLevelLoad/secs, 1f)));
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			if (col.gameObject.GetComponent<PlayerController>().isGrounded())
				col.gameObject.transform.parent = transform;
		}	
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.CompareTag("Player")) {
			col.gameObject.transform.parent = null;
		}	
	}
}
