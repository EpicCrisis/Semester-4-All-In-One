using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	Vector3 velocity = Vector3.zero;
	public float speed = 1f;

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		velocity.x = (Input.GetAxis ("Horizontal") * Time.deltaTime * speed);
		this.transform.Translate (velocity);
		velocity.y = (Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		this.transform.Translate (velocity);
	}
}
