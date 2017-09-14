using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLevel : MonoBehaviour {
	public GameObject pfTxtScore;

	GameObject goCanvas;

	// Use this for initialization
	void Start () {
		goCanvas = GameObject.Find("Canvas");
		if (goCanvas == null) {
			Debug.LogError("Canvas GO not found!!!");
		}
		setNumberBalls();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
	}

	void setNumberBalls(){
		int n = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("player")) {
			n++;
			go.GetComponent<scrScore>().num = n;
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("bot")) {
			n++;
			go.GetComponent<scrScore>().num = n;
		}
		GameObject txtGO;
		for (int i = 0; i < n; i++){
			txtGO = GameObject.Instantiate(pfTxtScore);
			txtGO.transform.parent = goCanvas.transform;
			txtGO.name = scrGlobal.txtScoreGONamePrefix + i.ToString();
			txtGO.transform.position = new Vector3(0,-20*i,0);
			txtGO.GetComponent<UnityEngine.UI.Text>().text = "Ball"+i.ToString()+" - 0" ;
		}

	}

	public void needRedrawScore(){
		
	}
}
