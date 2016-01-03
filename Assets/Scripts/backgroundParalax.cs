using UnityEngine;
using System.Collections;

public class backgroundParalax : MonoBehaviour {

	public GameObject grass;
	public GameObject mountains;
	public GameObject bg;

	public GameObject head;

	private float curRotation;
	private float lastRotation;
	
	private float distance;

	private Vector3 target_grass;
	private Vector3 target_mountains;
	private Vector3 target_bg;

	private float newX;

	private float speedDampner = 4f;

	// Use this for initialization
	void Start () {
		lastRotation = head.transform.localEulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		curRotation = head.transform.localEulerAngles.y / speedDampner;
		
		if (curRotation > 0) {
			distance = curRotation - lastRotation;
			if(Mathf.Abs (distance) > 30) distance = 0f;
			
			newX = Mathf.Clamp (grass.transform.localPosition.x - distance, -10f, 10f);
		} else {
			distance = lastRotation - curRotation;
			if(Mathf.Abs (distance) > 30) distance = 0f;
			
			newX = Mathf.Clamp(grass.transform.localPosition.x + distance, -10f, 10f);
		}
		
		target_grass = new Vector3 (newX, transform.localPosition.y, transform.localPosition.z);
		target_mountains = new Vector3 (newX/2, transform.localPosition.y, transform.localPosition.z);
		target_bg = new Vector3 (newX/4, transform.localPosition.y, transform.localPosition.z);
		
		if (head.transform.localEulerAngles.y != 0) {
			grass.transform.localPosition = target_grass;
			mountains.transform.localPosition = target_mountains;
			bg.transform.localPosition = target_bg;
			lastRotation = curRotation;
		}
		*/
	}

}
