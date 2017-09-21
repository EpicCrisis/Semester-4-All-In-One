using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

	private static GameManagerScript mInstance;

	public static GameManagerScript Instance {
		get {
			if (mInstance == null) {
				GameObject tempObject = GameObject.FindWithTag ("GameManager");
				if (tempObject == null) {
					tempObject = Instantiate (PrefabManagerScript.Instance.gameManagerPrefab, Vector3.zero, Quaternion.identity);
					//GameObject go = new GameObject ("GameManager");
					//mInstance = go.AddComponent<GameManagerScript> ();
					//go.tag = "GameManager";
				}
				mInstance = tempObject.GetComponent<GameManagerScript> ();
			}
			return mInstance;
		}
	}

	public Text scoreText;
	public int score;

	void Start ()
	{
		SoundManagerScript.Instance.PlayBGM (AudioClipID.BGM_GAMEPLAY);

		ObjectPoolManagerScript.Instance.CreatePool (SpawnManagerScript.Instance.enemyPrefabList [0], 10, 50);
		ObjectPoolManagerScript.Instance.CreatePool (SpawnManagerScript.Instance.enemyPrefabList [1], 10, 50);
		ObjectPoolManagerScript.Instance.CreatePool (SpawnManagerScript.Instance.enemyPrefabList [2], 10, 50);

		ObjectPoolManagerScript.Instance.CreatePool (SpawnManagerScript.Instance.bulletPrefabList [0], 10, 50);
	}

	void Update ()
	{
		scoreText.text = "Score : " + score;
	}
}
