using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayer : MonoBehaviour {


	GameObject goCamera;
	Vector3 tV3;

	// Use this for initialization
	void Start () {
		goCamera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		tV3 = gameObject.transform.position;
		tV3.y = 10f;
		tV3.z -= 20f;
		goCamera.gameObject.transform.position = tV3;
	}
}
