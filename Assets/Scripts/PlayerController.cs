using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private bool jumping = false;
	private bool grounded = false;
	private bool doubleJump = false;
	private bool doubleJumping = false;
	private bool movingRight = true;
	private bool active = true;

	private Rigidbody2D rigidBody;
	private Animator ani;
	public Transform groundCheck;
	public LayerMask layerMask;
	
	public float acceleration = 100f;
	public float maxSpeed = 10f;
	public float jumpSpeed = 500f;

	private float startingHealth = 100f;
	private float currentHealth;
	private Slider healthBar;

	public AudioSource jumpAudio;

	// Use this for initialization
	void Awake () {
		rigidBody = GetComponent<Rigidbody2D> ();

		ani = GetComponent<Animator>();

		healthBar = FindObjectOfType<Slider>();
		currentHealth = startingHealth;
		healthBar.value = currentHealth;
	}

	// Update is called once per frame
	void Update() {
		if (active) {
			if (grounded) {
				doubleJump = true;
			}

			if (Input.GetButtonDown("Jump")) {
				if (grounded) {
					jumping = true;
					doubleJump = true;
				} else if (doubleJump) {
					doubleJumping = true;
					doubleJump = false;
				}
			}
		}
	}

	//Called in fixed time intervals, frame rate independent
	void FixedUpdate() {
		if (active) {
			float moveH = Input.GetAxis ("Horizontal");

			ani.SetFloat("speed", Mathf.Abs(moveH));

			//grounded = Physics2D.Linecast(transform.position, groundCheck.position, layerMask);
			grounded = Physics2D.OverlapBox (groundCheck.position, (new Vector2 (1.3f, 0.2f)), 0f, layerMask);

			ani.SetBool("grounded", grounded);
			ani.SetFloat("vertSpeed", rigidBody.velocity.y);

			if (moveH < 0 && movingRight) {
				Flip();
			} else if (moveH > 0 && !movingRight) {
				Flip();
			}

			rigidBody.velocity = new Vector3 (maxSpeed * moveH, rigidBody.velocity.y, 0);
			/*if (rigidBody.velocity.x * moveH < maxSpeed) {
				rigidBody.AddForce (Vector2.right * moveH * acceleration);
			}

			if (Mathf.Abs (rigidBody.velocity.x) > maxSpeed) {
				Vector2 vel = new Vector2 (Mathf.Sign (rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
				rigidBody.velocity = vel;
			}*/

			if (jumping) {
				rigidBody.AddForce(new Vector2(0f, jumpSpeed));
				jumping = false;
				jumpAudio.Play();
			}
			if (doubleJumping) {
				rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 0);
				rigidBody.AddForce(new Vector2(0f, jumpSpeed));
				doubleJumping = false;
				jumpAudio.Play();
			}
		}
	}

	void Flip() {
		movingRight = !movingRight;
		transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
	}

	public bool isGrounded () {
		return grounded;
	}

	public void Break () {
		active = false;
		//rigidBody.bodyType = RigidbodyType2D.Static;
		ani.SetBool("active", false);
		ani.Play("Break");
		FindObjectOfType<GameController>().Break();
		healthBar.fillRect.GetComponentInChildren<Image>().color = new Color(0,0,0,0);
	}

	public void LevelEnd () {
		active = false;
		rigidBody.bodyType = RigidbodyType2D.Static;
		ani.SetBool("active", false);
		ani.Play("CelebrationRoll");
		FindObjectOfType<GameController>().LevelEnd();
	}

	public float HealthChange (float value) {
		if (currentHealth + value < startingHealth) {
			currentHealth += value;
		} else {
			currentHealth = startingHealth;
		}

		healthBar.value = currentHealth;

		if (currentHealth <= 0) {
			Break();
		}

		return currentHealth;
	}


}
