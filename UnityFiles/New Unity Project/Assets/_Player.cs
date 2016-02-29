using UnityEngine;
using System.Collections;

public class _Player : MonoBehaviour {


    public float speed;

    public float jumpforce;

    Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);

        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * jumpforce);

        }

    }
}
