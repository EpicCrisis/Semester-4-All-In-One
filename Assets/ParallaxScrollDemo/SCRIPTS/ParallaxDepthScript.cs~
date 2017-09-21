using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDepthScript : MonoBehaviour
{

	Vector3 velocity = Vector3.zero;
	public Camera mgCam;
	public Camera bgCam;
	public Camera fgCam;

	void Start ()
	{
		
	}

	void Update ()
	{
		velocity.x = Input.GetAxis ("Horizontal");
		velocity.y = Input.GetAxis ("Vertical");

		this.transform.Translate (velocity * Time.deltaTime * 3f);

		// zoom in by changing camera size
		if (Input.GetKey (KeyCode.Space)) {
			mgCam.orthographicSize = Mathf.MoveTowards (mgCam.orthographicSize, 3f, Time.deltaTime * 2f);
		} else {
			mgCam.orthographicSize = Mathf.MoveTowards (mgCam.orthographicSize, 5f, Time.deltaTime * 2f);
		}
		fgCam.fieldOfView = Mathf.Atan2 (mgCam.orthographicSize, 10) * Mathf.Rad2Deg * 2f;
		bgCam.fieldOfView = fgCam.fieldOfView;
	}
}
