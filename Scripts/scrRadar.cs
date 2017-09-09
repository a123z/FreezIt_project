using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRadar : MonoBehaviour {
	GameObject goTarget;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.tag == "bot" || col.gameObject.tag == "player"){
			//Debug.Log("trigger tag");
			//if (col.gameObject.transform.position.y <=1.1f){ //не используем - ловим цель и в т.ч. в прыжке
			if ( !col.gameObject.GetComponent<scrBall>().isFreeze()){ //если не в заморозке
				goTarget = col.gameObject;
				//gameObject.GetComponentInParent<scrBot>().SetTarget(col.gameObject); //установим цель
			}
		}
	}

	public GameObject FindTarget(float raRadius){ 
		

		//if () //обнулять надо только 1 раз при запуске корутины потом просто пропускать пока не найдется цель
			goTarget = null;
		StartCoroutine(radarPing(raRadius));
		//while (rr < raRadius && goTarget == null){
		//	((SphereCollider)cRCol).radius += 0.5f;
		//} 
		
		return goTarget;
	}

	IEnumerator radarPing(float _radius){
		float rr = 0;
		Component cRCol = gameObject.GetComponent<SphereCollider>();
		while (rr < _radius && goTarget == null){
			((SphereCollider)cRCol).radius += 0.5f;
			yield return new WaitForFixedUpdate();
		} 
		((SphereCollider)cRCol).radius = 0;
	}
}
