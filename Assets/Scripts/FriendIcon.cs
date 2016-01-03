using UnityEngine;
using System.Collections;

public class FriendIcon : MonoBehaviour {

	public bool selected;

	public GameObject playArrow;

	public string friendName;
	public int fid;
	public GameObject avatar;

	public GameObject frame_left;
	public GameObject frame_right;

	public Coroutine iconTimer;

	public GameObject nameHolder;

	public FriendManager friendManager;


	// Use this for initialization
	void Start () {
		friendManager = GameObject.Find ("FriendManager").GetComponent<FriendManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initFriend(int id) {
		avatar.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Friends/" + id) as Sprite;

		frame_left.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Friends/" + id + "_fl") as Sprite;
		frame_right.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Friends/" + id + "_fr") as Sprite;

		nameHolder.GetComponent<TextMesh> ().text = friendName;
	}

	public IEnumerator SelectIcon() {
		yield return new WaitForSeconds (1.5f);
		playArrow.SetActive (true);
	}

	public void SetGazedAt(bool setSelection) {
		if (friendManager.loading)	return;
		selected = setSelection;
		if (selected) {
		//	GetComponent<Animator>().Play("iconSelected");
			iconTimer = StartCoroutine (SelectIcon ());
			iconSelect ();
		} else {
			playArrow.SetActive (false);
		//	GetComponent<Animator>().Play("iconDeselected");
			iconDeselect ();
			StopCoroutine (iconTimer);
		}
	}

	public void iconSelect() {
		friendManager.currentSelection = fid;
	}
	public void iconDeselect() {
	}

	void OnTriggerEnter(Collider coll) {
		GameObject.Find ("VideoManager").GetComponent<testplay>().friendTouching = gameObject.name;
		GameObject.Find ("VideoManager").GetComponent<testplay> ().videoToLoad = fid;
		GetComponent<Animator> ().Play ("SelectFriend");
	}

	void OnTriggerExit(Collider coll) {
		GameObject.Find ("VideoManager").GetComponent<testplay>().friendTouching = "";
		GetComponent<Animator> ().Play ("DeselectFriend");
	}

}
