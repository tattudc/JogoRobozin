using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	public float speed;
	public float damage;

	private Animator ani;
	private AudioSource explosionAudio;

	// Use this for initialization
	void Start () {
		ani = gameObject.GetComponent<Animator>();	
		explosionAudio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}

	void OnTriggerEnter2D (Collider2D col) {
		ani.Play("Exploding");
		explosionAudio.Play();
		transform.GetComponent<Rigidbody2D>().simulated = false;

		if (col.CompareTag ("Player")) {
			if (col.gameObject.GetComponent<PlayerController>().HealthChange(-damage) <= 0)
				transform.parent.GetComponent<ShooterScript>().Stop();
		}

		Invoke("Remove", 1f);
	}

	void StopMissile () {
		transform.GetComponent<Rigidbody2D>().simulated = false;
	}

	void Remove () {
		Destroy(gameObject);
	}

}
