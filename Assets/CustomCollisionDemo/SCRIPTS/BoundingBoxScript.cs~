using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxScript : MonoBehaviour
{

	SpriteRenderer spr;
	public BoundingBoxScript other;
	bool isColliding = false;
	public bool debugMode = false;

	void Start ()
	{
		this.spr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		DebugRender ();
		DetectCollision ();
	}

	void DetectCollision ()
	{
		if (this.spr.bounds.Intersects (other.spr.bounds)) {
			isColliding = true;
		} else {
			isColliding = false;
		}
	}

	//render sprite when collision is detected
	void DebugRender ()
	{
		if (debugMode) {
			if (isColliding) {
				this.spr.color = Color.red;
			} else {
				this.spr.color = Color.white;
			}
		}
	}
}
