﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{

	public enum Type
	{
		ENEMY_01 = 0,
		ENEMY_02,
		ENEMY_03,
		TOTAL
	}

	public Type type = Type.ENEMY_01;
	public bool isToBeDestroyed = false;
	public bool isToBeKilled = false;

	// does not get called as it is overwritten in child class
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (transform.position.y < -6.6f) {
			isToBeDestroyed = true;
		}
		if (isToBeDestroyed) {
			//Destroy (gameObject);
			gameObject.SetActive (false);
			isToBeDestroyed = false;
		}
		if (isToBeKilled) {
			GameManagerScript.Instance.score += 100;
			//Destroy (gameObject);
			gameObject.SetActive (false);
			isToBeKilled = false;
		}
	}

	void OnTriggerStay2D (Collider2D target)
	{
		if (gameObject.tag == ("Enemy") && target.tag == ("PlayerBullet")) {
			isToBeKilled = true;
			SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_EXPLODE);
		}
	}
}
