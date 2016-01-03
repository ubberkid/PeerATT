using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	public GameObject buttons;

	public static MainScreen instance;

	public bool clicked = false;
	public bool selected = false;

	public float rotateTo = 0;
	public float rotateYTo = 0;

	public Vector3 v3Current;

	void Start() {

		MainScreen.instance = this;
	}

	void Update() {

		if (GameObject.Find("ScreenHolder").transform.rotation.eulerAngles.y != (int)rotateTo) {
			v3Current = Vector3.Lerp(v3Current, new Vector3(0, rotateTo, 0), Time.deltaTime);
			GameObject.Find("ScreenHolder").transform.eulerAngles = v3Current;
		}

		if (GameObject.Find("ScreenHolder").transform.localPosition.y != (int)rotateYTo) {
			GameObject.Find("ScreenHolder").transform.localPosition = Vector3.Lerp(GameObject.Find("ScreenHolder").transform.localPosition, new Vector3(0f,rotateYTo,0f), Time.deltaTime * 2f);
		}

	}

	public void toggleButtons(bool show) {

		buttons.SetActive(show);
	}

	public void OnTriggerEnter(Collider coll) {

	//	MainScreen.instance.selected = true;
	}

	public void OnTriggerExit(Collider coll) {

		//MainScreen.instance.selected = false;
	}
}
