using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileEngineScript : MonoBehaviour
{

	//y = rows, x = columns
	public int mapWidth = 1;
	public int mapHeight = 1;
	public Sprite[] tileSprites;
	GameObject[,] tileRefs = new GameObject[0, 0];
	int[,] tileMap = new int[0, 0];
	Vector3 offset;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
			RandomizeTiles ();
		if (Input.GetKeyDown (KeyCode.Alpha2))
			HardcodeTiles ();
		if (Input.GetKeyDown (KeyCode.Alpha3))
			LoadTilesFromText ();
	}

	void ResetTileMap ()
	{
		tileMap = new int[0, 0];

		for (int row = 0; row < tileRefs.GetLength (1); row++) {
			for (int col = 0; col < tileRefs.GetLength (0); col++) {
				Destroy (tileRefs [col, row]);
			}
		}
	}

	void RecalculateOffsets ()
	{
		offset.y = (float)(tileRefs.GetLength (1) - 1) / 2f;
		offset.x = -(float)(tileRefs.GetLength (0) - 1) / 2f;
	}

	[ContextMenu ("Randomize Tiles")]
	void RandomizeTiles ()
	{
		// reset tile map and destroy all tile reference generated from previous action
		ResetTileMap ();

		// define size
		tileMap = new int[mapHeight, mapWidth];

		// assign random sprite index for each cell
		for (int row = 0; row < tileMap.GetLength (0); row++) {
			for (int col = 0; col < tileMap.GetLength (1); col++) {
				tileMap [row, col] = Random.Range (0, tileSprites.Length);
			}
		}

		mapHeight = tileMap.GetLength (0);
		mapWidth = tileMap.GetLength (1);

		RenderTiles ();

		Debug.Log ("Randomize Tiles");
	}

	[ContextMenu ("Hardcode Tiles")]
	void HardcodeTiles ()
	{
		ResetTileMap ();

		tileMap = new int[3, 3] {
			{ 0, 1, 2 },
			{ 11, 12, 13 },
			{ 22, 23, 24 },
		};

		mapHeight = tileMap.GetLength (0);
		mapWidth = tileMap.GetLength (1);

		RenderTiles ();
	}

	[ContextMenu ("Load Tiles From Text")]
	void LoadTilesFromText ()
	{
		ResetTileMap ();

		// read from text
		TextAsset textFile = Resources.Load ("TEXTS/TileMap") as TextAsset;
		string data = textFile.text;

		data = data.Substring (data.IndexOf ("width=") + 6);
		mapWidth = int.Parse (data.Substring (0, data.IndexOf ('\n')));
		//mapWidth = int.Parse (FindData (data, "width"));

		data = data.Substring (data.IndexOf ("height=") + 7);
		mapHeight = int.Parse (data.Substring (0, data.IndexOf ('\n')));
		//mapHeight = int.Parse (FindData (data, "height"));

		tileMap = new int[mapHeight, mapWidth];

		string[] mapDataRow = data.Substring (data.IndexOf ("data=") + 7).Split ('\n');
		for (int row = 0; row < mapHeight; row++) {
			//Debug.Log (mapDataRow [row]);
			string[] mapDataCol = mapDataRow [row].Split (',');

			for (int col = 0; col < mapWidth; col++) {
				//Debug.Log (mapDataCol [col]);
				tileMap [row, col] = int.Parse (mapDataCol [col]) - 1;
			}
		}

		//Debug.Log (data);

		mapHeight = tileMap.GetLength (0);
		mapWidth = tileMap.GetLength (1);

		RenderTiles ();
	}

	/*
	string FindData (string data, string findingData)
	{
		data = data.Substring (data.IndexOf (findingData) + findingData.Length + 1);
		data = data.Substring (0, data.IndexOf ("\n"));

		return data;
	}
	*/

	void RenderTiles ()
	{
		// determine map size
		tileRefs = new GameObject[tileMap.GetLength (1), tileMap.GetLength (0)];

		RecalculateOffsets ();

		GameObject go = Resources.Load ("RENDERERS/TileRender") as GameObject;

		// load tiles
		for (int row = 0; row < tileRefs.GetLength (1); row++) {
			for (int col = 0; col < tileRefs.GetLength (0); col++) {
				GameObject tile = Instantiate (go);
				tile.GetComponent<TileRenderScript> ().spriteRenderer.sprite = tileSprites [tileMap [row, col]];
				tileRefs [col, row] = tile;
				tile.transform.position = new Vector3 (col, -row, 0f) + offset;
			}
		}
	}
}
