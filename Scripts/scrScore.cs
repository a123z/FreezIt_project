using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrScore : MonoBehaviour {
	public int num;
	public int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ScorePlus(int _score){
		score += _score;
		drawScore();
	}

	public int getScore(){
		return score;
	}

	public void setScore(int _score){
		score = _score;
		drawScore();
	}

	void drawScore(){
		GameObject.Find(scrGlobal.txtScoreGONamePrefix + num.ToString()).GetComponent<UnityEngine.UI.Text>().text = gameObject.tag + num.ToString() + " - " + score.ToString();
	}
}
