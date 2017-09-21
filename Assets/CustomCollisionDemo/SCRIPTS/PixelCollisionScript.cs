using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCollisionScript : MonoBehaviour
{

	SpriteRenderer spr;
	public PixelCollisionScript other;
	bool isColliding = false;
	public bool debugMode = false;

	void Start ()
	{
		this.spr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		DebugRender ();
		DetectCollision ();
	}

	void DetectCollision ()
	{
		if (this.spr.bounds.Intersects (other.spr.bounds)) {

			//coord R1
			BoundsScript r1 = new BoundsScript () {
				minX = this.spr.bounds.min.x,
				maxX = this.spr.bounds.max.x,
				minY = this.spr.bounds.min.y,
				maxY = this.spr.bounds.max.y,
			};

			//Debug.Log (string.Format ("{0},{1},{2},{3}", r1.minX, r1.maxX, r1.minY, r1.maxY));

			//coord R2
			BoundsScript r2 = new BoundsScript () {
				minX = other.spr.bounds.min.x,
				maxX = other.spr.bounds.max.x,
				minY = other.spr.bounds.min.y,
				maxY = other.spr.bounds.max.y,
			};

			//intersection
			float x1 = Mathf.Min (r1.maxX, r2.maxX);
			float x2 = Mathf.Max (r1.minX, r2.minX);
			float y1 = Mathf.Min (r1.maxY, r2.maxY);
			float y2 = Mathf.Max (r1.minY, r2.minY);

			//find minX and maxX coords for intersection
			Rect section = new Rect (Mathf.Min (x1, x2), Mathf.Min (y1, y2), Mathf.Abs (x1 - x2), Mathf.Abs (y1 - y2));

			//convert section to local texture space
			Rect r1Local = new Rect (section.x - r1.minX, section.y - r1.minY, section.width, section.height);

			Rect r2Local = new Rect (section.x - r2.minX, section.y - r2.minY, section.width, section.height);

			//Debug.Log (string.Format ("{0},{1}", r1Local.x, r1Local.y, r1Local.width, r1Local.height));

			//Debug.Log (string.Format ("{0},{1}", r2Local.x, r2Local.y, r2Local.width, r2Local.height));

			//get color information within local section
			Color[] r1Colors = this.spr.sprite.texture.GetPixels (
				                   (int)r1Local.min.x,
				                   (int)r1Local.min.y,
				                   (int)(r1Local.width * spr.sprite.pixelsPerUnit),
				                   (int)(r1Local.height * spr.sprite.pixelsPerUnit)
			                   );

			Color[] r2Colors = other.spr.sprite.texture.GetPixels (
				                   (int)r2Local.min.x,
				                   (int)r2Local.min.y,
				                   (int)(r2Local.width * other.spr.sprite.pixelsPerUnit),
				                   (int)(r2Local.height * other.spr.sprite.pixelsPerUnit)
			                   );

			//compare both sprite colour information
			for (int i = 0; i < r1Colors.Length; i++) {
				//return colliding if both pixels are not transparent (color.a == 1f)
				if (r1Colors [i].a > 0f && r2Colors [i].a > 0f) {
					isColliding = true;
					return;
				}
			}
		}
		isColliding = false;
	}

	//render sprite when collision is detected
	void DebugRender ()
	{
		if (debugMode) {
			if (isColliding) {
				this.spr.color = Color.red;
			} else {
				this.spr.color = Color.white;
			}
		}
	}
}
