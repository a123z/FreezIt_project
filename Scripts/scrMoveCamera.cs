using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMoveCamera : MonoBehaviour {

    GameObject goCamera;
    Vector3 tV3;
    Rigidbody cache_RB;

    // Use this for initialization
    void Start()
    {
        goCamera = Camera.main.gameObject;
        cache_RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        tV3 = scrGlobal.CameraOffset + new Vector3(gameObject.transform.position.x,0, gameObject.transform.position.z);
        //tV3.y = 10f;
        //tV3.z -= 20f;
        goCamera.gameObject.transform.position = tV3;

        /*//сдвиг камеры вслед за шариком игрока с задержкой:
        float a = (gameObject.transform.position.x - goCamera.transform.position.x) / 10;// dx
        if (a < -1) a = -1;
        else
        if (a > 1) a = 1;
        else
        if (a > 0) a = a * a;
        else
        if (a < 0) a = -a * a;
        float x2 = gameObject.transform.position.x + a * 10; //dx
        */

        /*
        //просто переделать поворот камеры не получится - надо ещё менять управление на вращение вектора прилагаемой силы...много заморочек
        tV3 = cache_RB.velocity.normalized;
        goCamera.gameObject.transform.position = gameObject.transform.position - tV3 * 20f + new Vector3(0,5,0);
        goCamera.transform.rotation = Quaternion.LookRotation(tV3 + new Vector3(0,-0.5f,0));
        */

    }
}
