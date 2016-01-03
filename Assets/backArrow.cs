using UnityEngine;
using System.Collections;

public class backArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		GameObject.Find ("VideoManager").GetComponent<testplay>().friendTouching = gameObject.name;
	}

	void OnTriggerExit(Collider coll) {
		GameObject.Find ("VideoManager").GetComponent<testplay>().friendTouching = "";
	}
}
