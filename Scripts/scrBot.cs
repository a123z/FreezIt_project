using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBot : MonoBehaviour {
	

	GameObject goRadar;
	GameObject goTarget;
        
	float radarTime = scrGlobal.radarTimeRepeat;
	float radarRadius = scrGlobal.radarRadius;
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
				FindTarget();
				radarTime = scrGlobal.radarTimeRepeat;
				if (goTarget == null) justGo();
			} else radarTime -= Time.fixedDeltaTime;
		} else {
				go2target();
				if ((goTarget.transform.position - gameObject.transform.position).sqrMagnitude < scrGlobal.freezerRadiusSqr){
				gameObject.GetComponent<scrBall>().freezerRun();
				}
			}
	}

	void go2target(){
		if (goTarget != null){
			gameObject.GetComponent<scrBall>().go(goTarget.transform.position - gameObject.transform.position);
		}
	}

	void justGo(){
	        
		if (transform.position.x > scrGlobal.arenaHalfSizeX) {
			
		} else if (transform.position.x < -scrGlobal.arenaHalfSizeX) {
		} else if (transform.position.z > scrGlobal.arenaHalfSizeZ) {
		} else if (transform.position.z < -scrGlobal.arenaHalfSizeZ) {
		}

		gameObject.GetComponent<scrBall>().go(justDirection);
	}

	void FindTarget(){
		goTarget = goRadar.GetComponent<scrRadar>().FindTarget(radarRadius);
	}

	public void SetTarget(GameObject target){ //вызывается из скрипта радара при коллизии коллайдера радара с целью
		Debug.Log("Set target (scrBot)");
		goTarget = target;
	}

	public void setJustDirection(Vector3 _direction){
		justDirection =_direction;
	}

	public void ClearTarget(GameObject _target){
		if (_target.Equals(goTarget)){
			goTarget = null;
		}
	}

}
