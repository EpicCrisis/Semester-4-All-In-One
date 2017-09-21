using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

	public enum AttackState
	{
		NONE,
		RANGED_ATTACK1,
		RANGED_ATTACK2,
		MELEE_ATTACK1
	}

	public AttackState curState;
	public AttackState prevState;

	public GameObject target;

	public bool isMeleeDone = false;
	public float meleeCheckOffset = 1.5f;

	public float shootTimer = 0f;
	public float shootDelay = 1f;
	int shootAmount = 0;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		curState = AttackState.NONE;
	}

	void Update ()
	{
		if (curState == AttackState.RANGED_ATTACK1) {
			shootTimer += Time.deltaTime;
			if (shootTimer >= shootDelay) {
				Shoot (EnemyBaseScript.Type.ENEMY_01);
				shootAmount++;
				shootTimer = 0f;
			}
			if (shootAmount >= 10) {
				shootAmount = 0;
				prevState = curState;
				curState = AttackState.RANGED_ATTACK2;
			}
		} else if (curState == AttackState.RANGED_ATTACK2) {
			shootTimer += Time.deltaTime;
			if (shootTimer >= shootDelay) {
				Shoot (EnemyBaseScript.Type.ENEMY_02);
				Shoot (EnemyBaseScript.Type.ENEMY_03);
				shootAmount++;
				shootTimer = 0f;
			}
			if (shootAmount >= 4) {
				shootAmount = 0;
				prevState = curState;
				curState = AttackState.NONE;
			}
		} else if (curState == AttackState.MELEE_ATTACK1) {
			if (!isMeleeDone) {
				transform.Translate (Vector2.down * 10f * Time.deltaTime);
				if (transform.position.y < -3.5f) {
					isMeleeDone = true;
				}
			} else {
				transform.Translate (Vector2.up * 5f * Time.deltaTime);
				if (transform.position.y >= 3f) {
					prevState = curState;
					curState = AttackState.NONE;
				}
			}
		} else if (curState == AttackState.NONE) {
			isMeleeDone = false;
			if (target.transform.position.x < transform.position.x + meleeCheckOffset
			    && target.transform.position.x > transform.position.x - meleeCheckOffset) {
				if (prevState != AttackState.MELEE_ATTACK1) {
					curState = AttackState.MELEE_ATTACK1;
				} else {
					curState = AttackState.RANGED_ATTACK1;
				}
			} else {
				curState = AttackState.RANGED_ATTACK2;
			}
		}
	}

	GameObject Shoot (EnemyBaseScript.Type enemyType)
	{
		GameObject newEnemy = null;

		if (enemyType == EnemyBaseScript.Type.ENEMY_01) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_01");
		} else if (enemyType == EnemyBaseScript.Type.ENEMY_02) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_02");
		} else if (enemyType == EnemyBaseScript.Type.ENEMY_03) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_03");
		} else {
			Debug.Log ("UNKNOWN TYPE SPAWN");
		}
		newEnemy.transform.position = transform.position;
		SpawnManagerScript.Instance.enemyList.Add (newEnemy.GetComponent<EnemyBaseScript> ());
		return newEnemy;
	}
}
