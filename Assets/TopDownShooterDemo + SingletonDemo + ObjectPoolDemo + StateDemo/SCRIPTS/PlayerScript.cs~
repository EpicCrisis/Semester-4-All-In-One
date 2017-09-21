using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Vector2 rawMousePos;
	public Vector2 worldMousePos;

	public Vector3 pos;

	public float playerShipSpeed;

	public GameObject playerBulletPrefab;
	GameObject playerBulletPrefabClone;

	public float topBound;
	public float bottomBound;
	public float leftBound;
	public float rightBound;

	public enum Type
	{
		PLAYERSHIP_01 = 0,
		PLAYERSHIP_02,
		PLAYERSHIP_03,
		TOTAL
	}

	// 0 = keyboard + mouse, 1 = mouse, 2 = joystick
	public int ControlScheme = 0;

	public Type type = Type.PLAYERSHIP_01;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		BorderCheck ();
		Movement ();
		Shoot ();

		if (Input.GetButtonDown ("Fire2")) {
			ControlScheme++;
			ControlScheme %= 3;
		}
	}

	void FixedUpdate ()
	{

	}

	void Movement ()
	{
		if (ControlScheme == 0) { // keyboard + mouse
			
			Cursor.visible = true;
			if (Input.GetButton ("Vertical")) {
				if (Input.GetAxis ("Vertical") > 0f) {
					transform.Translate (Vector2.up * playerShipSpeed * Time.deltaTime);
				} else if (Input.GetAxis ("Vertical") < 0f) {
					transform.Translate (Vector2.down * playerShipSpeed * Time.deltaTime);
				}
			}
			if (Input.GetButton ("Horizontal")) {
				if (Input.GetAxis ("Horizontal") > 0f) {
					transform.Translate (Vector2.right * playerShipSpeed * Time.deltaTime);
				} else if (Input.GetAxis ("Horizontal") < 0f) {
					transform.Translate (Vector2.left * playerShipSpeed * Time.deltaTime);
				}
			}
		} else if (ControlScheme == 1) { // mouse

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
			rawMousePos = Input.mousePosition;
			worldMousePos = Camera.main.ScreenToWorldPoint (rawMousePos);
			this.transform.position = worldMousePos;
		} else if (ControlScheme == 2) { // joystick

			Cursor.visible = true;
			if (Input.GetAxis ("JoyLeftVertical") > 0f) {
				transform.Translate (Vector2.up * playerShipSpeed * Time.deltaTime);
			} else if (Input.GetAxis ("JoyLeftVertical") < 0f) {
				transform.Translate (Vector2.down * playerShipSpeed * Time.deltaTime);
			}
			if (Input.GetAxis ("JoyLeftHorizontal") > 0f) {
				transform.Translate (Vector2.right * playerShipSpeed * Time.deltaTime);
			} else if (Input.GetAxis ("JoyLeftHorizontal") < 0f) {
				transform.Translate (Vector2.left * playerShipSpeed * Time.deltaTime);
			}

		}
	}

	public float shootDuration;
	float shootDurationTimer;
	bool canShoot;

	void Shoot ()
	{
		if (Input.GetButton ("Fire1") && ControlScheme != 2) {
			if (!canShoot) {
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_ATTACK);
				Shoot (PlayerBulletScript.Type.PLAYERBULLET_01);
				canShoot = true;
			}
		}
		if (Input.GetButton ("JoyFire1") && ControlScheme == 2) {
			if (!canShoot) {
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_ATTACK);
				Shoot (PlayerBulletScript.Type.PLAYERBULLET_01);
				canShoot = true;
			}
		}
		if (canShoot) {
			if (shootDurationTimer <= shootDuration) {
				shootDurationTimer += Time.deltaTime * 1000f;
			} else {
				shootDurationTimer = 0f;
				canShoot = false;
			}
		}
	}

	GameObject Shoot (PlayerBulletScript.Type bulletType)
	{
		GameObject newBullet = null;

		if (bulletType == PlayerBulletScript.Type.PLAYERBULLET_01) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newBullet = ObjectPoolManagerScript.Instance.GetObject ("PlayerBullet");
		} else {
			Debug.Log ("UNKNOWN TYPE SPAWN");
		}
		newBullet.transform.position = transform.position;
		SpawnManagerScript.Instance.playerBulletList.Add (newBullet.GetComponent<PlayerBulletScript> ());
		return newBullet;
	}

	void BorderCheck ()
	{
		pos = Camera.main.WorldToViewportPoint (transform.position);

		/*
		if (worldMousePos.y < -Camera.main.orthographicSize + 0.5f) {
			worldMousePos = new Vector2 (worldMousePos.x, -Camera.main.orthographicSize + 0.5f);
		}
		*/

		if (transform.position.y < -Camera.main.orthographicSize + 0.5f) {
			transform.position = new Vector2 (transform.position.x, -Camera.main.orthographicSize + 0.5f);
			Debug.Log ("BORDER");
		}
		if (transform.position.y > Camera.main.orthographicSize - 0.5f) {
			transform.position = new Vector2 (transform.position.x, Camera.main.orthographicSize - 0.5f);
			Debug.Log ("BORDER");
		}
		if (transform.position.x < -Camera.main.orthographicSize - 3.5f) {
			transform.position = new Vector2 (-Camera.main.orthographicSize - 3.5f, transform.position.y);
			Debug.Log ("BORDER");
		}
		if (transform.position.x > Camera.main.orthographicSize + 3.5f) {
			transform.position = new Vector2 (Camera.main.orthographicSize + 3.5f, transform.position.y);
			Debug.Log ("BORDER");
		}
	}
}
