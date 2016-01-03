using UnityEngine;
using System.Collections;
using BestHTTP;
using System;
using System.Text;
using SimpleJSON;
using UnityEngine.UI;

public class testplay : MonoBehaviour {

	public string friendTouching;
	public int videoToLoad;

	public string url;

	public MainScreen mainScreen;

	public MeshRenderer screen;

	// Use this for initialization
	void Start () {

		StartCoroutine( startVideoFeed() );

		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += HandleTouchHandler;
		/* // on start
		Debug.Log("Sending position");

		HTTPRequest request = new HTTPRequest(new Uri("http://api-m2x.att.com/v2/devices/8e8402aaa6c97bc7e2f9d5bd6a454fa2/streams/position/value"), HTTPMethods.Put, callback);

		JSONClass data = new JSONClass();

		data["value"] = ;

		request.AddHeader("X-M2X-KEY", "9fc7996ea7f03fccc6ef3978f2a4d012");
		request.AddHeader("Content-Type", "application/json");

		request.RawData = Encoding.UTF8.GetBytes(data.ToString());
		request.Send();

		Debug.Log("Sent");*/

	}

	void callback(HTTPRequest resq, HTTPResponse resp) {
		Debug.Log(resp.IsSuccess.ToString() + ": "  + resp.Message);
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject.Find ("Arrow").transform.position = GameObject.Find ("OVRPlayerController").transform.position;
		//GameObject.Find ("Arrow").transform.rotation = GameObject.Find ("OVRPlayerController").transform.rotation;
	}

	public IEnumerator startVideoFeed() {

		while (true) {
			
			yield return new WaitForSeconds(.1f);
			requestImage();
		
		}

	}
	public void test() {

		if (LayoutButton.currentlySelectedObject != null) {

			LayoutButton.currentlySelectedObject.GetComponent<LayoutButton>().setPosition();
		}
	}

	public void requestImage() {

		HTTPRequest request = new HTTPRequest(new Uri(url), HTTPMethods.Get, imageCallback);

		request.Send();
	}

	public void imageCallback(HTTPRequest req, HTTPResponse resp) {

		screen.material.mainTexture = resp.DataAsTexture2D;

	}

	void HandleTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
		{
			
			if (LayoutButton.currentlySelectedObject != null && mainScreen.buttons.activeSelf) {
				
				LayoutButton.currentlySelectedObject.GetComponent<LayoutButton>().setPosition();
			}

			if (FloorButton.selectedObject != null) {

				FloorButton.selectedObject.GetComponent<FloorButton>().sendAction();
			}

			if (!mainScreen.buttons.activeSelf) {
				mainScreen.buttons.SetActive(true);
			} else {
				mainScreen.buttons.SetActive(false);
				LayoutButton.currentlySelectedObject = null;
			}

			//TODO: Insert code here to handle a single tap.  Note that there are other TouchTypes you can check for like directional swipes, but double tap is not currently implemented I believe.
			/*
			if(friendTouching != "") {

				MediaPlayerCtrl video = GameObject.Find ("VideoManager").GetComponent<MediaPlayerCtrl> ();

				if (friendTouching == "Up Arrow") {
					video = GameObject.Find ("VideoManager").GetComponent<MediaPlayerCtrl> ();
					video.Pause();
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().screenHolder.SetActive(false);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().world.SetActive(true);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().room.SetActive(false);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().friendsContainer.SetActive(true);
				} else {
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().world.SetActive(false);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().screenHolder.SetActive(true);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().room.SetActive(true);
					GameObject.Find ("FriendManager").GetComponent<FriendManager>().friendsContainer.SetActive(false);
					video = GameObject.Find ("VideoManager").GetComponent<MediaPlayerCtrl> ();
					video.UnLoad ();
					video.Load (GameObject.Find ("FriendManager").GetComponent<FriendManager>().friendMedia[videoToLoad]);
					video.Play ();
				}
			}
*/



			    
		}
	}
}
