using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFreeze : MonoBehaviour {
	bool FreezIsProcess;
	//bool FreezStage2;
	float freezeRadius;
	float freezeHeight = 0.05f;
	float freezeTime = 0.7f; //время распространения поля заморозки
	float freezeTime2;
	float fTmp;
	Vector3 tV3;

	// Use this for initialization
	void Start () {
		//FreezIsProcess = false;
		//FreezStage2 = false;
		freezeRadius = scrGlobal.freezeRadius;
		freezeTime = scrGlobal.freezeTime;
		freezeTime2 = freezeTime + freezeTime;
		fTmp = 0;
		tV3 = new Vector3(0.1f,freezeHeight,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Debug.Log("freez fupd");
		if (FreezIsProcess){
			Debug.Log("freez 1 1");
			if (fTmp < freezeTime){
				Debug.Log("freez 2");
				fTmp += Time.fixedDeltaTime;
				tV3.x = freezeRadius * fTmp / freezeTime;
				if (tV3.x <= 0) tV3.x = 0.05f;
				tV3.z = tV3.x;
				gameObject.transform.localScale = tV3;
			} else {
				Debug.Log("freez 3");
				if (fTmp < (freezeTime2)){
					Debug.Log("freez 4");
					fTmp += Time.fixedDeltaTime;
					tV3.y = freezeHeight * (freezeTime2 - fTmp);
					if (tV3.y <= 0) tV3.y = 0.05f;
					gameObject.transform.localScale = tV3;
				} else {
					FreezIsProcess = false;
					tV3.x = 0.1f;
					tV3.x = freezeHeight;
					tV3.z = 0.1f;
					gameObject.transform.localScale = tV3;
					Destroy(gameObject,0.5f);
				}
			}
		}
	}

	public void freeze(float _freezeRadius){
		freezeRadius = _freezeRadius;
		if (!FreezIsProcess){
			Debug.Log("freez 1");
			FreezIsProcess = true;
			fTmp = 0;
		}
	}


}
