using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour
{

	// create tiles
	// determine tile location
	// determine tile type
	public TileModel tileModel;
	public GameObject tilePrefab;

	int ROW_COUNT = 12;
	int COL_COUNT = 16;

	float tileSize = 0.64f;

	void Start ()
	{
		GenerateTileMap ();
	}

	void Update ()
	{
		
	}

	void GenerateTileMap ()
	{
		// goes up
		for (int i = 0; i < ROW_COUNT; i++) {
			//goes right
			for (int j = 0; j < COL_COUNT; j++) { // formula for creating prefabs
				GameObject obj = (GameObject)Instantiate (tilePrefab, new Vector3 (j * tileSize - COL_COUNT / 2f * tileSize + tileSize / 2f,
					                 i * tileSize - ROW_COUNT / 2f * tileSize + tileSize / 2f), Quaternion.identity);
				TileScript tileScript = obj.GetComponent<TileScript> ();
				tileScript.myType = (TileType)Random.Range (0, (int)TileType.NONE);
				tileScript.tileModelScript = tileModel;
			}
		}
	}
}
