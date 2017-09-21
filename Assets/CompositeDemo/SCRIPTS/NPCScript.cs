using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour, IBaseNPC
{

	SpriteRenderer sp;

	public GameObject selectionRing;

	Vector2 moveDirection;
	Vector2 moveTarget;

	public float moveSpeed = 1f;

	bool isMove = false;

	void Start ()
	{
		selectionRing.SetActive (false);

		sp = GetComponent<SpriteRenderer> ();
		sp.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
	}

	public void Move (Vector2 moveDestination)
	{
		isMove = true;
		moveTarget = moveDestination;
		Vector2 tempPos = transform.position;
		moveDirection = moveDestination - tempPos;
	}

	public void Selected (bool t)
	{
		selectionRing.SetActive (t);
	}

	void Update ()
	{
		if (isMove) {
			transform.Translate (moveDirection * moveSpeed * Time.deltaTime);
			if (Vector2.Distance (transform.position, moveTarget) <= 0.1f) {
				isMove = false;
			}
		}
	}
}
