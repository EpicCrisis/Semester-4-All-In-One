using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType // drag and drop must adhere to order
{
	SAND = 0,
	BRICK,
	WOOD,
	STONE,
	GRASS,
	DUNE,
	DIRT,
	CLAY,
	NONE,
}

public class TileScript : MonoBehaviour
{
	public TileModel tileModelScript;
	public TileType myType;

	void Start ()
	{
		GetComponent<SpriteRenderer> ().sprite = tileModelScript.tileSpriteList [(int)myType];
	}

	void Update ()
	{
		
	}
}
