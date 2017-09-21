using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerBulletScript : MonoBehaviour
{

	public Vector2 direction = Vector2.up;
	public float bulletSpeed = 10f;
	public float yLimit = 5f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (direction * bulletSpeed * Time.deltaTime);
		if (this.transform.position.y > yLimit) {
			CustomObjectPoolScript.Instance.Despawn (gameObject);
		}
	}
}
