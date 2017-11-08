using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {

	public float maxSpeed;
	private Vector2 movement;

	private Transform pointer;

	// Use this for initialization
	void Start () {
		pointer = GameObject.Find ("PointerRoot").transform;
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical"));
	}

	void FixedUpdate(){
		float pointerYRot = pointer.rotation.eulerAngles.y;

		transform.position += new Vector3( maxSpeed * movement.x * Time.deltaTime, 0f,
		                                   maxSpeed * movement.y * Time.deltaTime);
	}

}
