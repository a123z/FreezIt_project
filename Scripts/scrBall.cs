using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBall : MonoBehaviour {
	public GameObject pfFreeze;
	GameObject objFreeze;


	float freezeTime;//время до окончания заморозки
	float motionForce = 5f;
	float ultraForceTime;
	float ultraFreezeRadiusTime=0;

	// Use this for initialization
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
	}

	public void jump(){
		if (gameObject.transform.position.y < 1.1f){
			gameObject.transform.GetComponent<Rigidbody>().AddForce(0,50,0);
		}
	}

	///need comment that 
	public void go(Vector3 direction){
		Vector3 tV3 = new Vector3(direction.x,0,direction.z);
		tV3 = tV3.normalized * motionForce;
		gameObject.transform.GetComponent<Rigidbody>().AddForce(tV3);
	}

	public void freeze(){
		if (transform.position.y <= 1f && objFreeze == null){
			objFreeze = GameObject.Instantiate(pfFreeze,new Vector3(transform.position.x,0.05f,transform.position.z), Quaternion.identity);
			if (objFreeze != null){
				if (ultraFreezeRadiusTime<=0){
					objFreeze.GetComponent<scrFreeze>().freeze(scrGlobal.freezeRadius);
				} else objFreeze.GetComponent<scrFreeze>().freeze(scrGlobal.ultraFreezeRadius);
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
}
