using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRadar : MonoBehaviour {
	GameObject goTarget;

	int radarMode; //1-find target 2-find freezer circle  
    Component cRCol;
    bool pingIsRun;


    //Работа радар: после вызова
    //Коллайдер радара работает на разные цели:
    //1.поиск цели, 2.определение препятствий, при определении цели он радиусом до 1/2 ширины экрана, при определении препятствий для прыжка
    //радиус коллайдера меньше и смещён в сторону вектора скорости Бота(родительского объекта)


    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SphereCollider>().radius = scrGlobal.radarRadius;
        cRCol = gameObject.GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        if (radarMode == 2) //сместим коллайдер по ходу движения - прыгать надо чуть раньше - чем быстрее едет тем раньше прыгать
        {
            ((SphereCollider)cRCol).center = gameObject.GetComponentInParent<Rigidbody>().velocity;
        }
    }

	void OnTriggerEnter(Collider col){
		
        switch (radarMode)
        {
            case 1:
                if (col.CompareTag("bot") || col.CompareTag("player"))
                {
                    //Debug.Log("trigger tag");
                    //if (col.gameObject.transform.position.y <=1.1f){ //не используем - ловим цель и в т.ч. в прыжке
                    if (!col.gameObject.GetComponent<scrBall>().isFreeze())
                    { //если не в заморозке
                        goTarget = col.gameObject;
                        gameObject.GetComponentInParent<scrBot>().SetTarget(col.gameObject); //установим цель
                        SetOnRadar(false); //цель нашли - выключить радар
                    }
                }
                break;
            case 2:
                if (col.CompareTag("freezer"))
                {
                    gameObject.GetComponentInParent<scrBall>().jump();
                }
                break;
        }
	}

    /// <summary>
    /// Устанавливает режим работы. Обычно вызывается при создании объекта
    /// </summary>
    /// <param name="_mode">режим работы радара 1-поиск цели 2-надо ли прыгать</param>
    public void SetMode(int _mode)
    {
        radarMode = _mode;
    }

    public void SetRadius(float _radius)
    {
        ((SphereCollider)cRCol).radius = _radius;
    }

    public void SetOnRadar(bool _on) {
        ((SphereCollider)cRCol).enabled = _on;
    }

  
}
