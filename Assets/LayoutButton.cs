using UnityEngine;
using System.Collections;
using BestHTTP;
using UnityEngine.UI;
using SimpleJSON;
using System;
using System.Text;

public class LayoutButton : MonoBehaviour {

	public enum POSITION_TYPE {

		NONE = 0,
		TOP,
		MIDDLE,
		BOTTOM
	}

	public static GameObject currentlySelectedObject;

	public MainScreen mainScreen;

	public bool selected = false;

	public POSITION_TYPE yPos;

	public int position;
	public float rotateScreenTo;

	public Transform buttonsContainer;

	public Transform screen;

	// Use this for initialization
	void Start () {

		//if (position == 2) {
		//	setPosition();
		//}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectObject() {

		// Unselect the other buttons if they are selected
		foreach(Transform button in buttonsContainer) {
			button.gameObject.GetComponent<LayoutButton>().selected = false;
			button.GetComponent<LayoutButton>().bindUI();

		}

		// Select this one
		if (!selected) {
			selected = true;
		}

		Debug.Log(gameObject.name + " is selected!");

		LayoutButton.currentlySelectedObject = gameObject;

		bindUI();
	}

	public void OnTriggerEnter(Collider coll) {

		selectObject();
	}

	public void OnTriggerExit(Collider coll) {

		selected = false;
		LayoutButton.currentlySelectedObject = null;
		bindUI();
	}

	public void moveScreenTo() {

		mainScreen.GetComponent<MainScreen>().rotateTo = rotateScreenTo;

		switch(yPos) {

			case POSITION_TYPE.TOP:
			mainScreen.GetComponent<MainScreen>().rotateYTo = 4f;
				break;

			case POSITION_TYPE.MIDDLE:
			mainScreen.GetComponent<MainScreen>().rotateYTo = 0f;
				break;

			case POSITION_TYPE.BOTTOM:
			mainScreen.GetComponent<MainScreen>().rotateYTo = -4f;
				break;
		}

	}
		
	public void bindUI() {

		if (selected) {
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/SelectedMaterial");
		} else {
			GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/UnSelectedMaterial");
		}

	}

	public void setPosition() {

		GameObject.Find("Debug Text").GetComponent<Text>().text += "Setting Position \n";

		HTTPRequest request = new HTTPRequest(new Uri("http://run-west.att.io/d9576f027ee8f/6e9234f387c9/9c7023eee2b3b23/in/flow/position"), HTTPMethods.Put, setPositionFinished);

		JSONClass data = new JSONClass();

		data["value"] = position.ToString();

		request.AddHeader("Content-Type", "application/json");
		//request.AddHeader("X-M2X-KEY", "9fc7996ea7f03fccc6ef3978f2a4d012");
		request.RawData = Encoding.UTF8.GetBytes(data.ToString());
		request.Send();

		moveScreenTo();

	}

	public void setPositionFinished(HTTPRequest req, HTTPResponse resp) {
	
		if (!resp.IsSuccess) {
			GameObject.Find("Debug Text").GetComponent<Text>().text += "ERROR: " + "resp.Message" + "\n";
		} else {
			GameObject.Find("Debug Text").GetComponent<Text>().text += "SUCCESS: " + "resp.Message" + "\n";
		}
	}
}
