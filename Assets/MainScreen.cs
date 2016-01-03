using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	public GameObject buttons;

	public static MainScreen instance;

	public bool clicked = false;
	public bool selected = false;

	void Start() {

		MainScreen.instance = this;
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
