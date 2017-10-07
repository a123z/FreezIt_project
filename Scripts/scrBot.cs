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


		//фигня получилась - этот коллайдер реагирует на пересечение с коллайдером заморозки и шар замораживается - надо с коллайдером радара делать....
		/*SphereCollider SColl = gameObject.AddComponent<SphereCollider>();
		SColl.isTrigger = true;
		SColl.center = Vector3.zero;
		SColl.radius = 3f;*/
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
        goRadar.GetComponent<scrRadar>().Check4Freezer();
    }

	void OnTriggerEnter(Collider col){
		Debug.Log("bot coll 1");
		if (col.CompareTag("freezer")){
			Debug.Log("freezer coll 1");
			if (Random.Range(1,4) < 3) {   //1,4 = 1.2.3
				gameObject.GetComponent<scrBall>().jump();
			}
		}
	}

	void go2target(){
		if (goTarget != null){
			//gameObject.GetComponent<scrBall>().go(goTarget.transform.position  - gameObject.transform.position);
			gameObject.GetComponent<scrBall>().go(goTarget.transform.position + goTarget.GetComponent<Rigidbody>().velocity  - gameObject.transform.position);
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
