using UnityEngine;
using System.Collections;

public class screenTracker : MonoBehaviour {

	public GameObject trackedScreen;
	public GameObject posText;

	public bool videoPlaying;

	private float servo_x_pos = 90f;
	private float servo_y_pos = 90f;

	private int servo_y_min = 0;
	private int servo_y_max = 179;
	
	private int servo_x_min = 0;
	private int servo_x_max = 179;


	//testing screen
	private float zpos = 1f;

	public Transform head;

	public FriendManager friendManager;

	private bool sending = false;
	public bool positionMode = false;
	
	// Use this for initialization
	void Start () {
//		friendManager = GameObject.Find ("FriendManager").GetComponent<FriendManager> ();
		//StartCoroutine (testZ ());
	}
	
	// Update is called once per frame
	void Update () {
		if (positionMode) {
			//moveRobot ();
			transform.localPosition = new Vector3(trackedScreen.transform.position.x, trackedScreen.transform.position.y, transform.position.z);
			transform.localRotation= trackedScreen.transform.rotation;
			//movingBox.transform.localRotation = trackedScreen.transform.rotation;
			//StartCoroutine (sendPosition ());
		}
	}

	public IEnumerator testZ() {
		yield return new WaitForSeconds (2f);

		zpos += 0.1f;

		GameObject.Find ("Screen").transform.localPosition = new Vector3 (1f, 3f, zpos);

		GameObject.Find ("zpos").GetComponent<TextMesh> ().text = "" + zpos;

		if (zpos >= 10f)	zpos = 0f;

		StartCoroutine (testZ ());
	}


	public void pushPos() {
		positionMode = !positionMode;
		if (positionMode) posText.SetActive (true);
		if (!positionMode) posText.SetActive (false);
		//StartCoroutine (sendPosition ());
	}

	public void hideScreen() {
		if (!videoPlaying) return;
		videoPlaying = false;
		MediaPlayerCtrl video = GameObject.Find ("VideoManager").GetComponent<MediaPlayerCtrl> ();
		video.UnLoad ();
		gameObject.GetComponent<Animator> ().Play ("hideScreen");
		StartCoroutine (removeScreen ());
	}
	public IEnumerator removeScreen() {
		yield return new WaitForSeconds (0.5f);
		gameObject.transform.parent.gameObject.SetActive (false);
	}

	public void selectVideo() {
		transform.localPosition = new Vector3 (1f, 3f, zpos);

		videoPlaying = true;
		MediaPlayerCtrl video = GameObject.Find ("VideoManager").GetComponent<MediaPlayerCtrl> ();
		video.UnLoad ();
		video.Load ("" + friendManager.friendMedia[friendManager.currentSelection]);
		video.Play ();
	}

	public void moveRobot() {
		Vector3 finalRotation = new Vector3 (head.eulerAngles.y, head.eulerAngles.x, head.eulerAngles.z);
		
		float posY = finalRotation.y + 90f;
		if (posY > 360) posY -= 360;
		if (posY > servo_y_max && posY < 270) posY = servo_y_max;
		if (posY >= 270 && posY <= 360) posY = servo_y_min;
		if(posY != 90f) servo_y_pos = posY;
		
		float posX = finalRotation.x + 90f;
		if (posX > 360) posX -= 360;
		if (posX > servo_x_max && posX < 270) posX = servo_x_max;
		if ((posX >= 270 && posX <= 360) || (posX >= 0 && posX <= servo_x_min)) posX = servo_x_min;
		posX = Mathf.Abs (posX - 180);
		if(posX != 90f) servo_x_pos = posX;

		posText.GetComponent<TextMesh> ().text = "X: " + Mathf.RoundToInt (servo_x_pos) + " Y: " + Mathf.RoundToInt (servo_y_pos);

	}

	public IEnumerator sendPosition() {
		if (sending) yield break;
		sending = true;
		string positions = "position?posX=" + Mathf.RoundToInt(servo_x_pos) + "&posY=" + Mathf.RoundToInt(servo_y_pos);
		WWW positionRequest = new WWW ("http://10.0.1.55:3001/"+positions);
		yield return positionRequest;
		yield return new WaitForSeconds(0.05f);
		sending = false;
	}
}
