using UnityEngine;
using System.Collections;

public class FriendManager : MonoBehaviour {

	public GameObject friend1;
	public GameObject friend2;
	public GameObject friend3;

	public GameObject friend;

	public GameObject leftArrow;
	public GameObject rightArrow;

	public GameObject screenHolder;
	public GameObject screen;
	public GameObject friendsContainer;
	public GameObject world;
	public GameObject room;

	public string[] friends = {"Nick Scheri", "Zach BMX", "Emily Anzell", "Sumi Bear", "Test Content"};
	public string[] friendMedia = {"rtsp://10.0.1.50:1935/live/peer","bike.mp4","emily.mp4", "emily.mp4", "emily.mp4"};

	public int currentPage = 0;
	public int friendsPerPage = 3;

	public int currentSelection;

	public int totalPages;

	public Coroutine arrowTimer;

	public bool loading;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		totalPages = Mathf.CeilToInt (friends.Length / friendsPerPage);

		deselectArrows ();
		renderFriends ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playVideo() {
		foreach(GameObject arrow in GameObject.FindGameObjectsWithTag("playarrow")) {
			arrow.SetActive(false);
		}

		world.SetActive (false);
		friendsContainer.SetActive (false);

		room.SetActive (true);


		screenHolder.SetActive (true);
		screen.GetComponent<screenTracker> ().selectVideo ();

	}

	public void lobbyReturn() { 
		screen.GetComponent<screenTracker> ().hideScreen ();
		world.SetActive (true);
		friendsContainer.SetActive (true);
		room.SetActive (false);
	}

	public void renderFriends() {
		loading = true;
		int count = 0;

		for (int i=currentPage*friendsPerPage; i<(currentPage*friendsPerPage + friendsPerPage); i++) {

			if(count == 0) friend = friend1;
			if(count == 1) friend = friend2;
			if(count == 2) friend = friend3;

			//GameObject friend = GameObject.Find("friend" + (count+1));

			friend.SetActive(true);

			if(i > friends.Length-1) {
				friend.SetActive(false);
			} else {
				//friend.GetComponent<RectTransform>().SetParent(friendContainer.GetComponent<RectTransform>());
				//friend.transform.localScale = Vector3.one;
				//friend.transform.localRotation = new Quaternion(0f,0f,0f,0f);

				friend.GetComponent<FriendIcon>().friendName = friends[i];
				friend.GetComponent<FriendIcon>().fid = i;
				friend.GetComponent<FriendIcon>().initFriend(i);
			}
			count++;
		}

		StartCoroutine (loadFriends ());
	}

	public IEnumerator nextPage() {
		yield return new WaitForSeconds (1f);
		Debug.Log ("NextPage Called");
		if (currentPage < totalPages) {
			//rightArrow.GetComponent<Animator> ().Play ("arrowClick");
			Debug.Log ("NextPage Advance");
			currentPage++;

			StartCoroutine (unloadFriends ());
		}
	}

	public IEnumerator previousPage() {
		yield return new WaitForSeconds (1f);
		Debug.Log ("Previous Page Called");
		if (currentPage > 0) {
			leftArrow.GetComponent<Animator> ().Play ("arrowClick");
			Debug.Log ("PreviousPage Advance");
			currentPage--;

			StartCoroutine (unloadFriends ());
		}
	}

	public void leftSelect() {
		if (currentPage <= 0) return;
	//	leftArrow.GetComponent<Animator> ().Play ("arrowSelect");
		arrowTimer = StartCoroutine (previousPage ());
	}
	public void leftDeselect() {
		if (currentPage <= 0) return;
	//	leftArrow.GetComponent<Animator> ().Play ("arrowDeselect");
		StopCoroutine (arrowTimer);
	}
	public void rightSelect() {
		if (currentPage >= totalPages) return;
	//	rightArrow.GetComponent<Animator> ().Play ("arrowSelect");
		arrowTimer = StartCoroutine (nextPage ());
	}
	public void rightDeselect() {
		if (currentPage >= totalPages) return;
	//	rightArrow.GetComponent<Animator> ().Play ("arrowDeselect");
		StopCoroutine (arrowTimer);
	}
	public void deselectArrows() {
		if (currentPage <= 0) {
//			leftArrow.GetComponent<Animator> ().Play ("arrowInactive");
		} else {
		//	leftArrow.GetComponent<Animator> ().Play ("arrowDeselect");
		}

		if (currentPage >= totalPages) {
	//		rightArrow.GetComponent<Animator> ().Play ("arrowInactive");
		} else {
	//		rightArrow.GetComponent<Animator> ().Play ("arrowDeselect");
		}

	}

	public IEnumerator loadFriends() {

		foreach(GameObject friend in GameObject.FindGameObjectsWithTag("friend")) {
			//friend.GetComponent <Animator>().Play("friendLoad");
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds (0.4f);
		loading = false;

		yield return new WaitForSeconds(0.01f);
	}

	public IEnumerator unloadFriends() {
		loading = true;
		deselectArrows ();
		foreach(GameObject friend in GameObject.FindGameObjectsWithTag("friend")) {
		//	friend.GetComponent <Animator>().Play("friendUnload");
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(0.5f);
		renderFriends ();
	}

}
