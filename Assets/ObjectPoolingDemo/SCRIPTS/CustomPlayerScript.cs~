using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerScript : MonoBehaviour
{
	Vector2 moveUp = Vector2.up;
	Vector2 moveDown = Vector2.down;
	Vector2 moveLeft = Vector2.left;
	Vector2 moveRight = Vector2.right;
	public float playerSpeed = 10f;
	public float shootRate = 100f;
	float shootRateCounter = 0f;
	bool canShoot = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") < 0f) {
			transform.Translate (moveLeft * playerSpeed * Time.deltaTime);
		}
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") > 0f) {
			transform.Translate (moveRight * playerSpeed * Time.deltaTime);
		}
		if (Input.GetButton ("Vertical") && Input.GetAxis ("Vertical") < 0f) {
			transform.Translate (moveDown * playerSpeed * Time.deltaTime);
		}
		if (Input.GetButton ("Vertical") && Input.GetAxis ("Vertical") > 0f) {
			transform.Translate (moveUp * playerSpeed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.Space) && canShoot) {
			CustomObjectPoolScript.Instance.Spawn ("PlayerBulletPrefab", this.transform.position, this.transform.rotation);
			canShoot = false;
		}
		if (!canShoot) {
			shootRateCounter += Time.deltaTime * 1000f;
			if (shootRateCounter > shootRate) {
				shootRateCounter = 0f;
				canShoot = true;
			}
		}
	}
}
