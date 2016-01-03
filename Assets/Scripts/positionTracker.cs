using UnityEngine;
using System.Collections;

public class positionTracker : MonoBehaviour {

	public GameObject trackTarget;
	
	public GameObject upArrow;

	public FriendManager friendManager;
	public screenTracker screen;

	public GameObject head;

	private float curRotation;
	private float lastRotation;
	private float curHoriz;

	private float distance;
	private Vector3 target;
	private float newX;

	private float speedDampner = 4f;



	// Use this for initialization
	void Start () {
		lastRotation = head.transform.localEulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		curHoriz = head.transform.eulerAngles.x;
		checkArrows ();

		/*
		curRotation = head.transform.localEulerAngles.y / speedDampner;
		curHoriz = head.transform.localEulerAngles.x;

		if (curRotation > 0) {
			distance = curRotation - lastRotation;
			if(Mathf.Abs (distance) > 30) distance = 0f;

			newX = Mathf.Clamp (transform.localPosition.x - distance, -10f, 10f);
		} else {
			distance = lastRotation - curRotation;
			if(Mathf.Abs (distance) > 30) distance = 0f;

			newX = Mathf.Clamp(transform.localPosition.x + distance, -10f, 10f);
		}

		checkArrows ();

		target = new Vector3 (newX, transform.localPosition.y, transform.localPosition.z);

		if (head.transform.localEulerAngles.y != 0) {
			//transform.localPosition = target;
			//lastRotation = curRotation;
		}
		*/
	}

	public void checkArrows() {
		/*
		if (newX > 4.5f) {
			float arrowAlpha = Mathf.Min ((newX - 4.5f) / 5, 1f);
			if(friendManager.currentPage <= 0) arrowAlpha = Mathf.Min ((newX - 4.5f) / 5, 0.2f);
			leftArrow.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, arrowAlpha);
		} else {
			leftArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
		}
	
		if (newX < -4.5f) {
			float arrowAlpha = Mathf.Max ((newX + 4.5f) / 5, -1f);
			if(friendManager.currentPage >= friendManager.totalPages) arrowAlpha = Mathf.Max ((newX + 4.5f) / 5, -0.2f);
			rightArrow.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, -arrowAlpha);
		} else {
			rightArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);		
		}
		*/

		if (curHoriz < 340f && curHoriz > 180f) {
			Debug.Log (curHoriz);
			float arrowAlpha = (340f - curHoriz) / 20f;
			upArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,arrowAlpha);
		} else {
			upArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
		}
		//if (curHoriz < 325f && curHoriz > 180f && screen.videoPlaying) {
		//	screen.GetComponent<screenTracker>().hideScreen ();
		//}

		/*
		if (curHoriz > 20f && curHoriz < 180f && !screen.videoPlaying) {
			float arrowAlpha = (curHoriz - 20f) / 20f;
			downArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,arrowAlpha);
		} else {
			downArrow.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
		}
		if (curHoriz > 35f && curHoriz < 180f && !screen.videoPlaying) {
			screen.GetComponent<screenTracker>().showScreen ();
		}
		*/
	}
}

