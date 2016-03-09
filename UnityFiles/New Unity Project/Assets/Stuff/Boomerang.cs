using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	public GameObject thrower;
	public bool identified = false;

	private Rigidbody rb;

	void Awake(){
		rb = transform.GetComponent<Rigidbody> ();
	}
	void Update () {
		if (identified) {
			float distance = Vector3.Distance (thrower.transform.position, transform.position);

			Vector3 v = thrower.transform.position - transform.position;
			rb.AddForce (v.normalized * (1.0f - distance / 35) * 25);
		}
	}
}
