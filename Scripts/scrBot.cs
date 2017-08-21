using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBot : MonoBehaviour {
	public float radarRadius = 10f;

	GameObject goRadar;
	GameObject goTarget;
        
	float radarTime = 0;
	Vector3 justDirection;
	Vector3 tV3;


	// Use this for initialization
	void Start () {
		goRadar = transform.Find("radar").gameObject; //find child gaomeObject
		if (goRadar == null) Debug.LogError("goRadar not found!");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (goTarget == null){
			if (radarTime<=0){
				Debug.Log("Run radar");
				//FindTarget();
				if (goTarget == null) justGo();
			} else radarTime -= Time.fixedDeltaTime;
		} else {
			go2target();
			}
	}

	void go2target(){
		if (goTarget != null){
		        gameObject.GetComponent<scrBall>().go(gameObject.transform.position - goTarget.transform.position);
		}
	}

	void justGo(){
	        gameObject.GetComponent<scrBall>().go(justDirection);
	}

	void FindTarget(){
		goTarget = goRadar.GetComponent<scrRadar>().FindTarget(radarRadius);
	}

	public void SetTarget(GameObject target){ //вызывается из скрипта радара при коллизии коллайдера радара с целью
		goTarget = target;
	}

	public void setJustDirection(Vector3 _direction){
		justDirection =_direction;
	}

}
