using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxClassicScript : MonoBehaviour
{

	public GameObject spriteA;
	GameObject spriteB;

	Vector3 velocity = Vector3.zero;
	public float parallaxVector = 1f;

	Vector3 threshold;

	void Start ()
	{
		// create copy of sprite A and assign to sprite B
		spriteB = Instantiate (spriteA, this.transform);
		Vector3 newPos = spriteA.transform.position;
		newPos.x = spriteA.GetComponent<SpriteRenderer> ().bounds.max.x;
		newPos.x += spriteA.GetComponent<SpriteRenderer> ().bounds.extents.x;
		spriteB.transform.position = newPos;

		// calculate threshold
		threshold = Vector3.zero;
		threshold.x = spriteA.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f;
	}

	void Update ()
	{
		ParallaxMovement ();
		ParallaxOffset ();
	}

	void ParallaxMovement ()
	{
		velocity.x = -(Input.GetAxis ("Horizontal") * Time.deltaTime * parallaxVector);
		spriteA.transform.Translate (velocity);
		spriteB.transform.Translate (velocity);
	}

	void ParallaxOffset ()
	{
		if (spriteA.transform.position.x < -threshold.x) { // check left parallax
			Vector3 newPos = spriteB.transform.position;
			newPos.x += spriteB.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f;
			spriteA.transform.position = newPos;
		} else if (spriteA.transform.position.x > threshold.x) { // check right parallax
			Vector3 newPos = spriteB.transform.position;
			newPos.x -= spriteB.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f;
			spriteA.transform.position = newPos;
		} else if (spriteB.transform.position.x < -threshold.x) { // check right parallax
			Vector3 newPos = spriteA.transform.position;
			newPos.x += spriteB.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f;
			spriteB.transform.position = newPos;
		} else if (spriteB.transform.position.x > threshold.x) { // check right parallax
			Vector3 newPos = spriteA.transform.position;
			newPos.x -= spriteB.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f;
			spriteB.transform.position = newPos;
		}
	}
}
