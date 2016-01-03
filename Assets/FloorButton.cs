using UnityEngine;
using System.Collections;
using BestHTTP;
using SimpleJSON;
using System.Text;
using System;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour {

	public static GameObject selectedObject = null;

	public Transform floorButtonContainer;

	public enum FLOOR_BUTTON_TYPE {

		NONE = 0,
		YES,
		NO,
		DOOR,
		LIGHT,
		PLUG
	};

	public FLOOR_BUTTON_TYPE buttonType;
	public int dlSwitch = 0;
	public bool selected = false;

	// Use this for initialization
	void Start () {
	
		//if (buttonType == FLOOR_BUTTON_TYPE.YES) {
		//	sendAction();
		//}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {

		selectObject();

	}

	void OnTriggerExit(Collider coll) {

		FloorButton.selectedObject = null;
		selected = false;
		bindUI();
	}

	public void selectObject() {

		foreach(Transform button in floorButtonContainer) {
			button.GetComponent<FloorButton>().selected = false;
			button.GetComponent<FloorButton>().bindUI();
		}

		FloorButton.selectedObject = gameObject;

		selected = true;

		bindUI();
	}

	public void sendAction() {

		GetComponent<Animator>().Play("Click");

		if (buttonType == FLOOR_BUTTON_TYPE.YES || buttonType == FLOOR_BUTTON_TYPE.NO) {

			GameObject.Find("Debug Text").GetComponent<Text>().text += "Setting Position \n";

			HTTPRequest request = new HTTPRequest(new Uri("http://run-west.att.io/d9576f027ee8f/6e9234f387c9/9c7023eee2b3b23/in/flow/action"), HTTPMethods.Put, actionSentCallback);

			JSONClass data = new JSONClass();

			data["value"] = ((int)buttonType).ToString();

			request.AddHeader("Content-Type", "application/json");
			//request.AddHeader("X-M2X-KEY", "9fc7996ea7f03fccc6ef3978f2a4d012");
			request.RawData = Encoding.UTF8.GetBytes(data.ToString());
			request.Send();
		} else {

			HTTPRequest request = new HTTPRequest(new Uri("http://run-west.att.io/d9576f027ee8f/6e9234f387c9/9c7023eee2b3b23/in/flow/dlswitch"), HTTPMethods.Put, actionSentCallback);

			JSONClass data = new JSONClass();

			data["value"] = (dlSwitch).ToString();

			request.AddHeader("Content-Type", "application/json");
			//request.AddHeader("X-M2X-KEY", "9fc7996ea7f03fccc6ef3978f2a4d012");
			request.RawData = Encoding.UTF8.GetBytes(data.ToString());
			request.Send();
		}



	}

	public void actionSentCallback(HTTPRequest resq, HTTPResponse resp) {

	}

	public void bindUI() {

		if (selected) {
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/SelectedMaterial");
		} else {
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/UnselectedMaterial");
		}
	}
}
