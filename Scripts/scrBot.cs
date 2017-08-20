using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBot : MonoBehaviour {
	public float radarRadius = 10f;

	GameObject goRadar;
	GameObject goTarget;

	// Use this for initialization
	void Start () {
		goRadar = transform.Find("radar").gameObject;
		if (goRadar == null) Debug.LogError("goRadar not found!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (goTarget == null){
			FindTarget();
		}
	}

	void go2target(){
		if (goTarget != null){
			
		}
	}

	public void FindTarget(){
		goTarget = goRadar.GetComponent<scrRadar>().FindTarget(radarRadius);
	}

	public void SetTarget(GameObject target){ //вызывается из скрипта радара при коллизии коллайдера радара с целью
		goTarget = target;
	}


}
