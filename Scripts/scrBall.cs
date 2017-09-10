using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBall : MonoBehaviour {
	public bool isPlayer = false;
	public GameObject pfFreeze;
	public GameObject pfRadar;
	GameObject objFreeze;


	float freezeTime;//время до окончания заморозки
	float motionForce = 5f;
	float ultraForceTime;
	float ultraFreezeRadiusTime=0;

	// Use this for initialization
	void Awake(){
		if (isPlayer){
			gameObject.GetComponent<scrBot>().enabled = false;
			gameObject.GetComponent<scrController>().enabled = true;
			gameObject.GetComponent<scrPlayer>().enabled = true;
			gameObject.GetComponent<Renderer>().material.color = new Color32(177,253,184,255);
		}
		else{
			gameObject.GetComponent<scrBot>().enabled = true;
			gameObject.GetComponent<scrController>().enabled = false;
			gameObject.GetComponent<scrPlayer>().enabled = false;
			gameObject.GetComponent<Renderer>().material.color = new Color32(248,212,147,255);
			GameObject pfR = Instantiate(pfRadar, gameObject.transform.position, Quaternion.identity);
			pfR.transform.parent = gameObject.transform;
			pfR.name = "radar";
		}
	}

	void Start () {
		//objFreeze = gameObject.transform.Find("wave").gameObject;
		if (pfFreeze == null) Debug.LogError("pfFreeze is null");
		objFreeze = null;
		motionForce = scrGlobal.motionForce; //initial force is same for all


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (freezeTime>0){
			freezeTime -= Time.fixedDeltaTime;
		}
		if (ultraForceTime>0){
			ultraForceTime -= Time.fixedDeltaTime;
			if (ultraForceTime<=0) motionForce = scrGlobal.motionForce;
		}
		if (ultraFreezeRadiusTime>0){
			ultraFreezeRadiusTime -= Time.fixedDeltaTime;
		}

		if (gameObject.transform.position.y < -30f){
			doDeath();
		}
	}

	public void jump(){
		if (gameObject.transform.position.y < 1.1f){
			gameObject.transform.GetComponent<Rigidbody>().AddForce(0,50,0);
		}
	}

	/// <summary>
	/// Move ball in the direction.
	/// Work for player controller and for bot too.
	/// </summary>
	/// <param name="direction">Direction.</param>
	public void go(Vector3 direction){
		if (transform.position.y <= 1f && gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude < scrGlobal.maxSpeedSqr){ //если не в полёте и скорость не максимальная
			Vector3 tV3 = new Vector3(direction.x,0,direction.z);
			tV3 = tV3.normalized * motionForce;
			gameObject.transform.GetComponent<Rigidbody>().AddForce(tV3);
		}
	}

	public void freeze(){
		if (transform.position.y <= 1f && objFreeze == null){
			objFreeze = GameObject.Instantiate(pfFreeze,new Vector3(transform.position.x,0.05f,transform.position.z), Quaternion.identity);
			if (objFreeze != null){
				if (ultraFreezeRadiusTime<=0){
					objFreeze.GetComponent<scrFreeze>().freeze(scrGlobal.freezeRadius, gameObject);
				} else objFreeze.GetComponent<scrFreeze>().freeze(scrGlobal.ultraFreezeRadius, gameObject);
			} else Debug.Log("objFreeze is null");
		}
	}

	public bool isFreeze(){
		if (freezeTime > 0){
			return true;
		} else return false;
	}

	public void SetUltraForce(){
		ultraForceTime += scrGlobal.ultraForceTime;
		motionForce = scrGlobal.ultraMotionForce;
	}

	public void setUltraFreezeRadius(){
		ultraFreezeRadiusTime = scrGlobal.ultraFreezeRadiusTime;
	}

	public void doDeath(){
		gameObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.transform.GetComponent<Rigidbody>().ResetInertiaTensor();
		gameObject.transform.position = new Vector3(0,10,0);

	}
}
