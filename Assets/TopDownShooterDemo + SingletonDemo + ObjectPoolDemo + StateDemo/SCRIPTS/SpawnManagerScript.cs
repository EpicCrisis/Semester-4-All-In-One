using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{

	public GameObject[] enemyPrefabList;
	public List<EnemyBaseScript> enemyList = new List<EnemyBaseScript> ();

	public GameObject[] playerPrefabList;
	public List<PlayerScript> playerList = new List<PlayerScript> ();

	public GameObject[] bulletPrefabList;
	public List<PlayerBulletScript> playerBulletList = new List<PlayerBulletScript> ();

	float spawnDurationCC;
	public float spawnDuration;

	PlayerScript.Type playerType = PlayerScript.Type.PLAYERSHIP_01;
	GameObject newPlayer = null;

	private static SpawnManagerScript mInstance;

	public static SpawnManagerScript Instance {
		get {
			if (mInstance == null) {
				GameObject tempObject = GameObject.FindWithTag ("SpawnManager");
				if (tempObject == null) {
					tempObject = Instantiate (PrefabManagerScript.Instance.gameManagerPrefab, Vector3.zero, Quaternion.identity);
					//GameObject go = new GameObject ("GameManager");
					//mInstance = go.AddComponent<GameManagerScript> ();
					//go.tag = "GameManager";
				}
				mInstance = tempObject.GetComponent<SpawnManagerScript> ();
			}
			return mInstance;
		}
	}

	void Start ()
	{
//		newPlayer = Instantiate (playerPrefabList [(int)playerType], new Vector2 (0f, 0f), Quaternion.identity);
	}

	void Update ()
	{
		
//		spawnDurationCC += Time.deltaTime;
//
//		if (spawnDurationCC > spawnDuration) {
//			
//			spawnDurationCC = 0f;
//			EnemyBaseScript.Type enemyType;
//
//			if (enemyList.Count < 5) {
//				enemyType = EnemyBaseScript.Type.ENEMY_01;
//			} else {
//				enemyType = (EnemyBaseScript.Type)Random.Range(0, (int)EnemyBaseScript.Type.TOTAL);
//			}
//
//			Spawn (enemyType);
//			/*
//			GameObject newEnemy = Spawn (type);
//
//			if (newEnemy.GetComponent<EnemyBaseScript> ().type == EnemyBaseScript.Type.ENEMY_03) {
//				Spawn (EnemyBaseScript.Type.ENEMY_01);
//				Spawn (EnemyBaseScript.Type.ENEMY_02);
//			}
//			*/
//		}
	}

	//factory method
	GameObject Spawn (EnemyBaseScript.Type enemyType)
	{
		GameObject newEnemy = null;

		if (enemyType == EnemyBaseScript.Type.ENEMY_01) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_01");
			newEnemy.transform.position = new Vector2 (0f, 6.6f);
		} else if (enemyType == EnemyBaseScript.Type.ENEMY_02) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_02");
			newEnemy.transform.position = new Vector2 (7.0f, 6.6f);
		} else if (enemyType == EnemyBaseScript.Type.ENEMY_03) {
			//newEnemy = Instantiate (enemyPrefabList [(int)enemyType], new Vector2 (0f, 6.6f), Quaternion.identity);
			newEnemy = ObjectPoolManagerScript.Instance.GetObject ("Enemy_03");
			newEnemy.transform.position = new Vector2 (-7.0f, 6.6f);
		} else {
			Debug.Log ("UNKNOWN TYPE SPAWN");
		}

		enemyList.Add (newEnemy.GetComponent<EnemyBaseScript> ());
		return newEnemy;
	}
}
