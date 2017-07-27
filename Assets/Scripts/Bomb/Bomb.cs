using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    
    public static Vector3 dest;

    private Transform player;
    private Collider col;

    private bool isD;
    private Vector3[] controlPoint = new Vector3[4];
    private float speed = 0.7f;
    private float curTime = 0;
    Quaternion fa = Quaternion.Euler(0, 0, 0);
	
	void Awake () {
        col = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        controlPoint[3] = dest;
        controlPoint[0] = player.position;

        controlPoint[1] = controlPoint[0] + (controlPoint[3] - controlPoint[0]) / 3.0f;
        controlPoint[2] = controlPoint[0] + (controlPoint[3] - controlPoint[0]) * 2.0f / 3.0f;

        controlPoint[2].y = controlPoint[1].y = Vector3.Distance(controlPoint[3],controlPoint[1])/2;

	}
    Vector3 Bezier(float t){
        return controlPoint[3] * t*t*t+3*controlPoint[2] * (1 - t)*t*t+controlPoint[0] * (1 - t) * (1 - t) * (1 - t)+3*controlPoint[1]*t*(1-t)*(1-t);
    }

	void Update () {
        if (isD) return;
        curTime += Time.deltaTime;
        float p = curTime / speed;
        if(p>=1){
            isD = true;
            col.enabled = true;
            Destroy(gameObject, 0.5f);
        }else transform.position = Bezier(p);
	}
}
