using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {
	
	public GameObject boomerang;
	public float walkSpeed = 5;

	public float horizontalInput = 0;
	public float verticalInput = 0;

	private GameObject sprite;


	// Use this for initialization
	void Start () {
		transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
		sprite = transform.FindChild ("Sprite").gameObject;
	}

	// Update is called once per frame
	void Update () {

		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");

		Vector3 direction = new Vector3 (horizontalInput, 0, verticalInput) * walkSpeed * Time.deltaTime;
		transform.Translate (direction);

		Vector2 a = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 b = Camera.main.ScreenToViewportPoint (Input.mousePosition);

		float angle = (Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg) + 90;
		sprite.transform.rotation = Quaternion.Euler (new Vector3(90, 270, angle));
	}
}
