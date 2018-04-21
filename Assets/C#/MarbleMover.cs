using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleMover : MonoBehaviour {
	public float speed = 1;
	private Rigidbody2D myRigid;
	// Use this for initialization
	void Start () {
		myRigid = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		myRigid.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed));
	}
}
