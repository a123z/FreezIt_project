using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFreezer : MonoBehaviour {
	bool FreezerIsProcess;
	//bool FreezStage2;
	float freezerRadius;
	float freezerHeight = 0.05f;
	float freezerTime = 0.7f; //время распространения поля заморозки
	float freezerTime2;
	float fTmp;
	GameObject parentGameObject;
	Vector3 tV3;

	// Use this for initialization
	void Start () {
		//FreezIsProcess = false;
		//FreezStage2 = false;
		freezerRadius = scrGlobal.freezerRadius;
		freezerTime = scrGlobal.freezerTime;
		freezerTime2 = freezerTime + freezerTime;
		fTmp = 0;
		tV3 = new Vector3(0.1f,freezerHeight,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider otherBall){
		if (otherBall.gameObject != parentGameObject ){
			if (otherBall.CompareTag("player") || otherBall.CompareTag("bot")){
				otherBall.gameObject.GetComponent<scrBall>().doFreeze();
				if (parentGameObject.CompareTag("bot")){
					parentGameObject.GetComponent<scrBot>().ClearTarget(otherBall.gameObject);
				}
			}
		}
	}

	void FixedUpdate(){

		if (FreezerIsProcess){			//если надо рисовать заморозку
			if (fTmp < freezerTime){ 	//заморозка ещё расширяется
				fTmp += Time.fixedDeltaTime;
				tV3.x = freezerRadius * fTmp / freezerTime;
				if (tV3.x <= 0) tV3.x = 0.05f;
				tV3.z = tV3.x;
				gameObject.transform.localScale = tV3;
			} else { 						//заморозка уже закончила расширяться
				if (fTmp < (freezerTime2)){	//заморозка должна снижаться
					fTmp += Time.fixedDeltaTime;
					tV3.y = freezerHeight * (freezerTime2 - fTmp);
					if (tV3.y <= 0) tV3.y = 0.05f;
					gameObject.transform.localScale = tV3;
				} else {					//замаразка закончилась - отключаем всё
					FreezerIsProcess = false;
					tV3.x = 0.1f;
					tV3.x = freezerHeight;
					tV3.z = 0.1f;
					gameObject.transform.localScale = tV3;
					Destroy(gameObject,0.1f);
				}
			}
		}
	}

	public void freezerRun(float _freezerRadius, GameObject parentGO){
		freezerRadius = _freezerRadius;
		if (!FreezerIsProcess){
			Debug.Log("freez 1");
			FreezerIsProcess = true;
			fTmp = 0;
			parentGameObject = parentGO;
		}
	}


}
