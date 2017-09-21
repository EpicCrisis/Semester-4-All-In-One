using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

	public float bulletSpeed;

	public enum Type
	{
		PLAYERBULLET_01 = 0,
		PLAYERBULLET_02,
		PLAYERBULLET_03,
		TOTAL
	}

	public Type type = Type.PLAYERBULLET_01;
	public bool isToBeDestroyed = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Movement ();

		if (transform.position.y > 6.6f) {
			isToBeDestroyed = true;
		}
		if (isToBeDestroyed) {
			gameObject.SetActive (false);
			isToBeDestroyed = false;
		}
	}

	void Movement ()
	{
		transform.Translate (Vector2.up * bulletSpeed * Time.deltaTime);
	}

	void OnTriggerStay2D (Collider2D target)
	{
		if (gameObject.tag == ("PlayerBullet") && target.tag == ("Enemy")) {
			GameManagerScript.Instance.score += 10;
			SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_ENEMY_HURT);
			isToBeDestroyed = true;
		}
	}
}
