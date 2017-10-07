using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBot : MonoBehaviour {
	

	GameObject goRadar1;
    GameObject goRadar2;
    GameObject goTarget;
        
	float radarTime = scrGlobal.radarTimeRepeat;
	float radarRadius = scrGlobal.radarRadius;
	Vector3 justDirection;
	Vector3 tV3;


	// Use this for initialization
	void Start () {
		goRadar1 = transform.Find("radar1").gameObject; //find child gaomeObject
		if (goRadar1 == null) Debug.LogError("goRadar1 not found!");
        goRadar2 = transform.Find("radar2").gameObject; //find child gaomeObject
        if (goRadar2 == null) Debug.LogError("goRadar2 not found!");

        goRadar1.GetComponent<scrRadar>().SetRadius(radarRadius);
        goRadar2.GetComponent<scrRadar>().SetRadius(5f);

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
                goRadar1.GetComponent<scrRadar>().SetOnRadar(true);
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

	void OnTriggerEnter(Collider col){
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
