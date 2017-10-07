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
    /// Поиск цели вокруг
    /// </summary>
    /// <param name="raRadius">радиус внутри которого ищется цель, преременная т.к. может быть больше при бонусе</param>
    /// <returns></returns>
	public GameObject FindTarget(float raRadius){ 
		//if () //обнулять надо только 1 раз при запуске корутины потом просто пропускать пока не найдется цель
		goTarget = null;
        if (!pingIsRun) //ищем цель даже если сейчас работает контроль прыжка перед заморозкой
        {
            StartCoroutine(Ping(raRadius, 1));
        }
		return goTarget;
	}


    public void Check4Freezer()
    {
        if (!pingIsRun)
        {
            StartCoroutine(Ping(5f, 2));
        }
    }

    IEnumerator Ping(float _radius, int _mode)
    {
        pingIsRun = true;
        radarMode = _mode;
        ((SphereCollider)cRCol).radius = _radius;
        if (_mode == 1)
        {
            int pingCount = 5;
            while (pingCount > 0 && goTarget == null)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        else 
        if (_mode == 2)
        {
            int pingCount = 2;
            while (pingCount > 0)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        ((SphereCollider)cRCol).radius = 0;
        pingIsRun = false;
    }

    IEnumerator radarPing(float _radius){
		//float rr = 0;
		Component cRCol = gameObject.GetComponent<SphereCollider>();
		while (((SphereCollider)cRCol).radius < _radius && goTarget == null){
			((SphereCollider)cRCol).radius += 0.5f;
			yield return new WaitForFixedUpdate();
		} 
		((SphereCollider)cRCol).radius = 0;
	}
}
