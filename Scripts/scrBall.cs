using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBall : MonoBehaviour {
	public GameObject pfFreeze;
	GameObject objFreeze;


	float freezeTime;//время до окончания заморозки

	// Use this for initialization
	void Start () {
		//objFreeze = gameObject.transform.Find("wave").gameObject;
		if (pfFreeze == null) Debug.LogError("pfFreeze is null");
		objFreeze = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (freezeTime>0){
			freezeTime -= Time.fixedDeltaTime;
		}
	}

	public void jump(){
		if (gameObject.transform.position.y < 1.1f){
			gameObject.transform.GetComponent<Rigidbody>().AddForce(0,20,0);
		}
	}

	public void go(Vector3 force){
		Vector3 tV3 = new Vector3(force.x,0,force.z);
		gameObject.transform.GetComponent<Rigidbody>().AddForce(tV3);
	}

	public void freeze(){
		if (transform.position.y <= 1f && objFreeze == null){
			objFreeze = GameObject.Instantiate(pfFreeze,new Vector3(transform.position.x,0.05f,transform.position.z), Quaternion.identity);
			if (objFreeze != null){
				Debug.Log("beg freez");
				objFreeze.GetComponent<scrFreeze>().freeze();
				Debug.Log("end freez");
			} else Debug.Log("objFreeze is null");
		}
	}

	public bool isFreeze(){
		if (freezeTime > 0){
			return true;
		} else return false;
	}

	public void youFreeze(float _freezeTime = 7f){
		if (!isFreeze()){ //надо ли не замораживать если уже заморожен???
			freezeTime = _freezeTime;
		}
	}
}
