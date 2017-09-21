using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimatorScript : MonoBehaviour
{
	[Header ("Sprite Setup")]
	public int rowSize;
	public int colSize;
	public int pixelsPerUnit;

	[Header ("Animation Controls")]
	public int currentRow;
	public int currentCol;
	public float timer;
	public float framesPerSec;
	public bool playReverse = false;
	public bool playDown = false;

	MeshRenderer meshrend;

	void Start ()
	{
		meshrend = GetComponent<MeshRenderer> ();
		Reslice ();
	}

	[ContextMenu ("Reslice")]
	public void Reslice ()
	{
		if (Application.isPlaying) {
			Vector2 tiling = new Vector2 (1f / colSize, 1f / rowSize); //slicing texture
			GetComponent<Renderer> ().material.SetTextureScale ("_MainTex", tiling); //apply tiling

			//scaling sprite while keeping aspect ratio
			float sizeX = GetComponent<Renderer> ().material.mainTexture.width / (float)colSize;
			float sizeY = GetComponent<Renderer> ().material.mainTexture.height / (float)rowSize;
			this.transform.localScale = new Vector3 (sizeX / (float)pixelsPerUnit, sizeY / (float)pixelsPerUnit, 1f);
		}
	}

	void Update ()
	{
		PressButton ();
		LoopAnimation ();
		ReverseLoop ();
		OneShot ();
		PingPong ();
		MultipleRow ();
	}

	public int animationType = 0;

	void PressButton ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			currentCol = 0;
			currentRow = 1;
			animationType = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentCol = 5;
			currentRow = 1;
			animationType = 2;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			currentCol = 0;
			currentRow = 1;
			animationType = 3;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			currentCol = 0;
			currentRow = 1;
			animationType = 4;
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			currentCol = 0;
			currentRow = 1;
			animationType = 5;
		}
	}

	void LoopAnimation () //play normal loop
	{
		if (animationType == 1) {
			Vector2 offset = Vector2.zero;
			offset.y = (1f / rowSize) * (rowSize - currentRow); //set row offset to show the current row

			if (timer < 1f) {
				timer += Time.deltaTime * framesPerSec;	//iterate through every column of the row per second
			} else {
				timer = 0f;
				currentCol++;
				currentCol %= colSize;
			}

			offset.x = (1f / colSize) * currentCol;

			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset); 
		}
	}

	void ReverseLoop () //play in reverse
	{
		if (animationType == 2) {
			Vector2 offset = Vector2.zero;
			offset.y = (1f / rowSize) * (rowSize - currentRow);

			if (timer < 1f) {
				timer += Time.deltaTime * framesPerSec;
			} else {
				timer = 0f;
				currentCol--;
			}
			if (currentCol < 0) {
				currentCol = colSize - 1;
			}

			offset.x = (1f / colSize) * currentCol;

			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset);
		}
	}

	void OneShot () //play once and stop at last frame
	{
		if (animationType == 3) {
			Vector2 offset = Vector2.zero;
			offset.y = (1f / rowSize) * (rowSize - currentRow);

			//iterate through every column of the row per second
			if (timer < 1f) {
				timer += Time.deltaTime * framesPerSec;
			} else {
				timer = 0f;
				currentCol++;
			}
			if (currentCol >= colSize - 1) {
				timer = 0f;
				currentCol = colSize - 1;
			}

			offset.x = (1f / colSize) * currentCol;

			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset); 
		}
	}

	void PingPong () //play to last frame then play in reverse
	{
		if (animationType == 4) {
			Vector2 offset = Vector2.zero;
			offset.y = (1f / rowSize) * (rowSize - currentRow); //set row offset to show the current row

			if (currentCol > colSize - 2) {
				playReverse = true;
			}
			if (currentCol < 1) {
				playReverse = false;
			}

			if (!playReverse) {
				if (timer < 1f) {
					timer += Time.deltaTime * framesPerSec;	//iterate through every column of the row per second
				} else {
					timer = 0f;
					currentCol++;
				}
			}
			if (playReverse) {
				if (timer < 1f) {
					timer += Time.deltaTime * framesPerSec;
				} else {
					timer = 0f;
					currentCol--;
				}
			}

			offset.x = (1f / colSize) * currentCol;

			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset); 
		}
	}

	void MultipleRow () //play until end of image sequence then loop
	{
		if (animationType == 5) {
			Vector2 offset = Vector2.zero;
			offset.y = (1f / rowSize) * (rowSize - currentRow); //set row offset to show the current row

			if (timer < 1f) {
				timer += Time.deltaTime * framesPerSec;	//iterate through every column of the row per second
			} else {
				timer = 0f;
				currentCol++;
				if (currentCol > 1 && currentRow == rowSize) { //special condition as current sprite is unique
					playDown = true;
				} else if (currentCol > colSize - 1) {
					playDown = true;
				}

				if (currentRow >= rowSize && playDown) { //special condition as current sprite is unique
					currentRow = 1; //return to beginning
					currentCol = 0;
					playDown = false;
				} else if (playDown) {
					currentRow++;
					currentCol = 0;
					playDown = false;
				}
			}

			offset.x = (1f / colSize) * currentCol;

			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset); 
		}
	}
}
