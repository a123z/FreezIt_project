using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.inputString);
		if (Input.GetAxis("Horizontal") != 0){
			gameObject.GetComponent<scrBall>().go(new Vector3(Input.GetAxis("Horizontal"),0,0));
		}
		if (Input.GetAxis("Vertical") != 0){
			gameObject.GetComponent<scrBall>().go(new Vector3(0,0,Input.GetAxis("Vertical")));
		}

		if (Input.GetButton("Jump")){
			gameObject.GetComponent<scrBall>().jump();
		}

		if (Input.GetButton("Fire1")){
			gameObject.GetComponent<scrBall>().freezerRun();
		}
	}


}
