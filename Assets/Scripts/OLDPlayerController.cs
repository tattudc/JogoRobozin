using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDPlayerController : MonoBehaviour {

	private Rigidbody2D rigidBody;

	public float velocidade;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	//Chamado em um intervalo fixo, independente do FrameRate
	void FixedUpdate()
	{
		float moverV = Input.GetAxis ("Vertical");
		float moverH = Input.GetAxis ("Horizontal");

		Vector2 movimento = new Vector2 (moverH, moverV);

		rigidBody.AddForce (movimento * velocidade);
	}
}
