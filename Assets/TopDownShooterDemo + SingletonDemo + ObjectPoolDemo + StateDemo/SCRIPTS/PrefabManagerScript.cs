using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManagerScript : MonoBehaviour
{
	private static PrefabManagerScript mInstance = null;

	public static PrefabManagerScript Instance {
		get { 
			// singleton implementation for object that must be created manually in the scene
			if (mInstance == null) {
				GameObject tempObject = GameObject.FindWithTag ("PrefabManager");
				if (tempObject == null) {
					Debug.LogError ("PrefabManagerScript Does Not Exist");
				}
				mInstance = tempObject.GetComponent<PrefabManagerScript> ();
			}
			return mInstance;
		}
	}
	public GameObject gameManagerPrefab;
	public GameObject soundManagerPrefab;
	public GameObject spawnManagerPrefab;
}